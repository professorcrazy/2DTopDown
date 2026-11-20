using System;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public abstract class ABullet : MonoBehaviour
{
    GameObject owner;
    Rigidbody2D rb;

    float lifeTime = 5f;
    [SerializeField] private int bouncesLeft = 0;
    [SerializeField] private float bounceSpeedReduction = 0.8f;

    [SerializeField] private float damage = 20f;
    [SerializeField] private LayerMask hitLayers;
    CinemachineImpulseSource impulseSource;

    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactEffectDuration = 1f;

    protected virtual void LoadComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    virtual public void BulletSetup(GameObject owner, float speed)
    {
        LoadComponents();
        this.owner = owner;
        rb.linearVelocity = transform.up * speed;
    }
    virtual public void BulletSetup(GameObject owner, float speed, float lifeTime)
    {
        LoadComponents();
        this.owner = owner;
        rb.linearVelocity = transform.up * speed;
        this.lifeTime = lifeTime;
        if (lifeTime > 0) { 
            Destroy(this.gameObject, lifeTime);
        }
    }

    virtual public void BulletSetup(GameObject owner, float speed, int bounces)
    {
        LoadComponents();
        this.owner = owner;
        rb.linearVelocity = transform.up * speed;
        this.bouncesLeft = bounces;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == owner)
        {
            return;
        }
        if (GameSettings.Instance.UseScreenShake)
        {
            impulseSource.GenerateImpulse();
        }
        if (impactEffect != null)
        {
            GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(impact, impactEffectDuration);
        }
        other.gameObject.GetComponent<IHealth>()?.TakeDamage(damage);
        
        bouncesLeft--;
        if (bouncesLeft < 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //hit speed
            float hitSpeedReduction = rb.linearVelocity.magnitude * bounceSpeedReduction;
            rb.linearVelocity *= bounceSpeedReduction;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.linearVelocity);
        }
    }
}
