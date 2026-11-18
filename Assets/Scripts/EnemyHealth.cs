using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] public EnemyData enemyData;

    public int currenthealth;
    private MeshRenderer meshRenderer;
    private ParticleSystem explosionParticles;

    void OnEnable(){
        currenthealth = enemyData.health;
        explosionParticles = GetComponentInChildren<ParticleSystem>();
        
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
}
