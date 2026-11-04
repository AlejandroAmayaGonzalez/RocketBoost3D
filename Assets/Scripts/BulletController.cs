using System;
using UnityEngine;

public class BulletController : MonoBehaviour {
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 3f;
    [SerializeField] private ParticleSystem hitParticles;

    [Header("Bullet Hit Sounds")]
    [SerializeField] private AudioClip hitSound;

    void OnEnable(){
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }

    private void DeactivateBullet(){ gameObject.SetActive(false); }

    void OnTriggerEnter(Collider other){
        // Play hit sound and particles
        AudioSource.PlayClipAtPoint(hitSound, transform.position, 5f);
        hitParticles.Play();
        DeactivateBullet();

        //TODO particles
        //TODO if bullet hits obstacle, destroy it
        //TODO if bullet hits player, reduce health
        //TODO if bullet hits enemy, reduce enemy health
    }
}
