using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private AudioClip enemyHitSound;

    private int currenthealth;
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private ParticleSystem explosionParticles;

    void OnEnable(){
        currenthealth = enemyData.health;
        explosionParticles = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        
        // Get the sprite renderer and set it as true
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }
    
    public void TakeDamage(int damage){
        currenthealth -= damage;
        if (currenthealth <= 0){ StartCoroutine(Die()); }
    }

    /// <summary>
    /// Plays particles and deactivate itself
    /// </summary>
    private IEnumerator Die(){
        meshRenderer.enabled = false;   // Deactivate the mesh
        explosionParticles.Play();  // Play the particles
        yield return new WaitForSeconds(0.5f);  // Wait until the particles finish
        gameObject.SetActive(false); // Deactivate the enemy for now
    }

    // Deal damge to player
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            audioSource.Stop();
            audioSource.PlayOneShot(enemyHitSound);

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyData.damage);
        }
    }
}
