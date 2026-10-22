using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 2.5f;

    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrustParticleSystem;
    [SerializeField] private ParticleSystem rightThrustParticleSystem;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        ProcessThrust();
        ProcessRotation();
    }

    void OnEnable(){
        thrust.Enable();
        rotation.Enable();
    }

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

    private void StopThrusting(){ thrustParticleSystem.Stop(); }

    private void StartThrusting(){
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thrustParticleSystem.isPlaying){ thrustParticleSystem.Play(); }

        // Start particles and sound
    }
}
