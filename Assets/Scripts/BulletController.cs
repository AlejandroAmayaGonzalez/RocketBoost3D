using System;
using UnityEngine;

public class BulletController : MonoBehaviour {
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 3f;

    void OnEnable(){
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }

    private void DeactivateBullet(){ gameObject.SetActive(false); }

    void OggerEnter(Collider other){
        DeactivateBullet();

        //TODO particles
        //TODO if bullet hits obstacle, destroy it
        //TODO if bullet hits player, reduce health
        //TODO if bullet hits enemy, reduce enemy health
    }
}
