using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip mineExplosionSound;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem landingParticles;
    [SerializeField] private float crashDelay = 2f;

    AudioSource audioSource;
    bool isControllable = true;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision){
        if (!isControllable) return;

        switch (collision.gameObject.tag){
            case "LandingPad":
                DieOrSuccess(landingSuccessAudioClip, landingParticles);
                break;
            case "Explosion":
                DieOrSuccess(explosionAudioClip, explosionParticles);
                break;
            case "Enemy":
                EnemyHit(collision.gameObject);
                break;                        
        }
    }

    // For the mine explosion
    void OnTriggerEnter(Collider other){
        EnterMineZone(other.gameObject);
    }

    private void PlayAudio(AudioClip audioClip){
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Manages when the player enters the explosion range of a mine
    /// </summary>
    private void EnterMineZone(GameObject mineObj){
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(playerHealth.currentHealth);

        PlayAudio(mineExplosionSound);

        mineObj.GetComponent<EnemyHealth>().TakeDamage(mineObj.GetComponent<EnemyHealth>().currenthealth);
    }

    /// <summary>
    /// Manages the enemy collision
    /// </summary>
    /// <param name="obj">the gameobject to get the enemy damage</param>
    private void EnemyHit(GameObject obj){
        PlayAudio(enemyHitSound);

        GetComponent<PlayerHealth>().TakeDamage(
            obj.GetComponent<EnemyHealth>().enemyData.damage
        );
    }

    /// <summary>
    /// Manages the collision event, disabling player control, reproducing the audioClip.
    /// </summary>
    /// <param name="audioClip">the audio played when the collision occurs</param>
    private void DieOrSuccess(AudioClip audioClip, ParticleSystem particleSystem){
        isControllable = false;

        PlayAudio(audioClip);

        particleSystem.Play();

        GetComponent<PlayerMovement>().enabled = false;

        Invoke(nameof(ReloadScene), crashDelay);
    }

    void ReloadScene(){
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
