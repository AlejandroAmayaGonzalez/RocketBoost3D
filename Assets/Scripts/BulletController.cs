using System;
using UnityEngine;

public class BulletController : MonoBehaviour {
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 3f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private ParticleSystem hitParticles;

    [Header("Bullet Hit Sounds")]
    [SerializeField] private AudioClip hitSound;

    void OnEnable(){
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }

    private void DeactivateBullet(){ gameObject.SetActive(false); }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy")){
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        }

        // Play hit sound and particles
        AudioSource.PlayClipAtPoint(hitSound, transform.position, 5f);
        hitParticles.Play();
        DeactivateBullet();
    }
}
