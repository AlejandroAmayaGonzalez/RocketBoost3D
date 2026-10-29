using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [Header("Input actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private InputAction shoot;

    [Header("Movement settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 2.5f;

    [Header("Particle systems")]
    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrustParticleSystem;
    [SerializeField] private ParticleSystem rightThrustParticleSystem;

    [Header("Audio settings")]
    [SerializeField] private AudioClip thrustAudioClip;

    Rigidbody rb;
    AudioSource audioSource;
    PlayerShooting playerShooting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    void FixedUpdate(){
        ProcessThrust();
        ProcessRotation();
        ProcessShooting();
    }

    void OnEnable(){
        thrust.Enable();
        rotation.Enable();
        shoot.Enable();
    }

    /// <summary>
    /// Process the rotation and applies a rotation to the player
    /// </summary>
    private void ProcessRotation(){
        rb.freezeRotation = true; // Freezing rotation so we can control it manually

        float rotationValue = rotation.ReadValue<float>();

        if (rotationValue < 0){
            RotateRight();
        }else if (rotationValue > 0){
            RotateLeft();
        }else{ StopRotation(); }
    }

    /// <summary>
    /// Rotation control
    /// </summary>
    private void StopRotation(){
        leftThrustParticleSystem.Stop();
        rightThrustParticleSystem.Stop();
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);

        if (!leftThrustParticleSystem.isPlaying){
            rightThrustParticleSystem.Stop();
            leftThrustParticleSystem.Play();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);

        if (!rightThrustParticleSystem.isPlaying){
            leftThrustParticleSystem.Stop();
            rightThrustParticleSystem.Play();
        }
    }

    /// <summary>
    /// Thrust control
    /// </summary>
    private void ProcessThrust(){
        if (thrust.IsPressed()){
            StartThrusting();
        }else{
            StopThrusting();
        }
    }

    /// <summary>
    /// Process the shooting input action 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ProcessShooting(){
        if (shoot.IsPressed()) playerShooting.Shoot();
    }

    /// <summary>
    /// Stop the thrusting action by stopping the particles and sound
    /// </summary>
    private void StopThrusting(){ 
        thrustParticleSystem.Stop(); 
        audioSource.Stop();
    }

    /// <summary>
    /// Start adding a rotation force, particles and sound
    /// </summary>
    private void StartThrusting(){
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thrustParticleSystem.isPlaying){ thrustParticleSystem.Play(); }

        if (!audioSource.isPlaying){ audioSource.PlayOneShot(thrustAudioClip); }
    }
}
