using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;

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
            case "LaunchPad":
                Debug.Log("LaunchPad collision");
                break;
            case "LandingPad":
                CustomCollision(landingSuccessAudioClip, landingParticles);
                break;
            case "Explosion":
                CustomCollision(explosionAudioClip, explosionParticles);
                break;
            default:
                Debug.Log("No collision");
                break;            
        }
    }

    /// <summary>
    /// Manages the collision event, disabling player control, reproducing the audioClip.
    /// </summary>
    /// <param name="audioClip">the audio played when the collision occurs</param>
    private void CustomCollision(AudioClip audioClip, ParticleSystem particleSystem){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        particleSystem.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(ReloadScene), crashDelay);
    }

    void ReloadScene(){
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
