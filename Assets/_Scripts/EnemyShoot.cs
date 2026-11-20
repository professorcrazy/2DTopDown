using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform shotOrigin;
    public float bulletSpeed = 15f;
    float fireRate;
    public float shotsPerSecond = 1.5f;
    
    bool canShoot = true;

    void Start()
    {
        if (shotOrigin == null)
        {
            shotOrigin = transform;
        }
        fireRate = 1f / shotsPerSecond;
    }

    public void Shoot()
    {
        if (bulletPrefab == null)
        {
            return;
        }
        if(canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shotOrigin.position, shotOrigin.rotation);
            if (bullet.TryGetComponent<ABullet>(out ABullet bulletComponent))
            {
                bulletComponent.BulletSetup(this.transform.root.gameObject, bulletSpeed, 3f);
            }
            StartCoroutine(ShotDelay(fireRate));
        }
    }

    public void Shoot(float delay)
    {
        if (bulletPrefab == null)
        {
            return;
        }
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shotOrigin.position, shotOrigin.rotation);
            if (bullet.TryGetComponent<ABullet>(out ABullet bulletComponent))
            {
                bulletComponent.BulletSetup(this.transform.root.gameObject, bulletSpeed, 3f);
            }
            StartCoroutine(ShotDelay(delay));
        }
    }

    IEnumerator ShotDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
