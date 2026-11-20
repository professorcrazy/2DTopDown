using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineCollisionImpulseSource))]
[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform shotOrigin;
    float bulletSpeed = 10f;
    float fireRate;
    public float shotsPerSecond = 2f;
    bool canShoot = true;
    bool isShooting = false;

    AudioSource gunSound;

    [SerializeField] private float bulletLifeTime = 5f;

    //[Header("Special bullets")]
    //[SerializeField] private GameObject originalBulletPrefab;
    //[SerializeField] private GameObject specialBulletPrefab;
    //float specialBulletTimer = 5f;

    private CinemachineImpulseSource impulseSource;
    public void Shoot(InputAction.CallbackContext context)
    {
        if (bulletPrefab == null)
        {
            return;
        }

        if (context.performed)
        {
            isShooting = true;
        }
        if (context.canceled)
        {
            isShooting = false;
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        gunSound = GetComponent<AudioSource>();
        if (shotOrigin == null)
        {
            shotOrigin = transform;
        }
        fireRate = 1f / shotsPerSecond;
    }

    private void FixedUpdate()
    {
        if (isShooting && canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shotOrigin.position, shotOrigin.rotation);
            bullet.GetComponent<ABullet>()?.BulletSetup(this.gameObject, bulletSpeed, bulletLifeTime);
            if (GameSettings.Instance.UseScreenShake)
            {
                impulseSource.GenerateImpulse();
            }
            if (gunSound != null)
            {
                gunSound.Play();
            }
            StartCoroutine(ShotDelay(fireRate));
        }
    }
    IEnumerator ShotDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
