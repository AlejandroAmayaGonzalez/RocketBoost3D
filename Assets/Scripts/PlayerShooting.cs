using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    [Header("Ammo Settings")]
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private bool infiniteAmmo = false;

    [Header("Shooting Settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletRate = 1f;
    [SerializeField] private float bulletDamage = 1;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip bulletSound;

    private Transform bulletSpawnPoint;
    private ObjectPull bulletPool;
    private float lastShotTime = 0f;

    void Start(){ 
        bulletPool = GetComponent<ObjectPull>(); 
        bulletSpawnPoint = transform.Find("ShootPoint");
    }

    /// <summary>
    /// Check if the player can shoot based on bullet rate and ammo
    /// </summary>
    /// <returns></returns>
    public bool CanShoot(){
        // Check if time has passed between shots and has enough ammo to shoot
        return (Time.time - lastShotTime >= bulletRate && (infiniteAmmo || currentAmmo > 0));
    }

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    public void Shoot(){
        if (CanShoot()){
            // Update last shot time
            lastShotTime = Time.time;
            
            // Create a temporary audio source to play the bullet sound at the bullet spawn point
            AudioSource.PlayClipAtPoint(bulletSound, bulletSpawnPoint.position, 5f);

            // Reduce ammo
            if (!infiniteAmmo){ currentAmmo--; }
            
            // Get a bullet
            GameObject bullet = bulletPool.GetObjectFromPool();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;

            // Apply speed and movement
            Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
            bulletRigidBody.linearVelocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }
}
