using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float impactForceMultiplier = 1f;
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private float damage;
    [SerializeField] private float lifeTimer;
    [SerializeField] private ParticleSystem thrusterParticleSystem;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip travelingSound;
    [SerializeField] private AudioClip explosionSoundEffect;
    [SerializeField] private Transform model;

    private Rigidbody2D rb2d;
    private bool hasDetonated;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.queriesHitTriggers = false;
    }

    private IEnumerator Start()
    {
        //play traveling sound
        //audioSource.clip = travelingSound;
        //audioSource.Play();
        yield return new WaitForSeconds(lifeTimer);
        Detonate();
    }

    private void Update()
    {
        rb2d.AddForce(transform.up * speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Detonate();
    }

    public void Detonate()
    {
        if(hasDetonated)
            return;
        
        audioSource.Stop();
        audioSource.PlayOneShot(explosionSoundEffect);
        
        explosionParticleSystem.Play();
        explosionParticleSystem.gameObject.transform.SetParent(null);
        Destroy(explosionParticleSystem.gameObject, 3f);

        var hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, 0f, affectedLayers);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                var closestPt = hit.collider.ClosestPoint(transform.position);
                var between = closestPt - (Vector2)transform.position;
                var ratio = (explosionRadius - between.magnitude)/explosionRadius;
                damageable.TakeDamage(damage * ratio);

                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(between.normalized * impactForceMultiplier * ratio, ForceMode2D.Impulse);
            }
        }

        rb2d.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        model.gameObject.SetActive(false);
        thrusterParticleSystem.gameObject.SetActive(false);
        GetComponent<Light2D>().enabled = false;
        hasDetonated = true;
        Destroy(gameObject, 3f);
    }
}