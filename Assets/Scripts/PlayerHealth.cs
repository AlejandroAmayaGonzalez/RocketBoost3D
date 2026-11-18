using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    
    [Header("Health settings")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private ParticleSystem deadParticles;
    [SerializeField] private AudioClip deadSound;
    
    [Header("Blink Settings")]
    [SerializeField] private float blinkDuration = 0.3f;
    [SerializeField] private Color blinkColor = Color.red;

    private bool isVulnerable = true;
    public int currentHealth;
    private AudioSource audioSource;
    
    // Starship's Sprites
    private MeshRenderer[] meshes;

    void Start(){
        currentHealth = maxHealth;
        // Get all spriteRenderer
        meshes = GetComponentsInChildren<MeshRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Deal damage to the player
    /// </summary>
    /// <param name="enemyDamage">The amount the enemy damage</param>
    public void TakeDamage(int enemyDamage){
        if (isVulnerable){
            currentHealth -= enemyDamage;

            if (currentHealth <= 0){
                StartCoroutine(Die());
            }else{
                StartCoroutine(Blink(3));    
            }
        }
    }

    /// <summary>
    /// Blinks and gives invulnerability to the player when is hit
    /// </summary>
    private IEnumerator Blink(int numberBlink){
        isVulnerable = false;

        do{
            //Change hit color
            for (int i = 0; i < meshes.Length; i++){
                meshes[i].material.color = blinkColor;
            }
            yield return new WaitForSeconds(blinkDuration);

            // Restore the original
            for (int i = 0; i < meshes.Length; i++){
                meshes[i].material.color = Color.white;
            }
            yield return new WaitForSeconds(blinkDuration);
            
            numberBlink--;
        }while(numberBlink > 0);

        isVulnerable = true;
    }

    private IEnumerator Die(){
        for (int i = 0; i < meshes.Length; i++){
            meshes[i].enabled = false;
        }

        // Play the particles and sound
        audioSource.Stop();
        audioSource.PlayOneShot(deadSound);
        deadParticles.Play();  

        yield return new WaitForSeconds(1f);  // Wait until the particles finish

        gameObject.SetActive(false); // Deactivate the enemy for now

        Invoke(nameof(ReloadScene), 0.5f);
    }

    void ReloadScene(){
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
