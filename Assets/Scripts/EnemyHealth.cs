using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private EnemyData enemyData;

    private int currenthealth;

    private void OnEnable(){
        currenthealth = enemyData.health;
    }
    
    public void TakeDamage(int damage){
        currenthealth -= damage;
        if (currenthealth <= 0){ Die(); }
    }

    private void Die(){
        gameObject.SetActive(false); // Deactivate the enemy for now
    }
}
