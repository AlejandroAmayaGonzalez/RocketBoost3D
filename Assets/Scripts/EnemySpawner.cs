using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    public class SpawnGroup{
        public ObjectPull enemyPool;
        public Transform spawnPoint;
        public float startX;
        public int enemyCount;
        public float spawnInterval = 2f;

        [HideInInspector] public bool hasStarted;
    }

    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<SpawnGroup> SpawnGroups;

    void Start(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        foreach (var group in SpawnGroups){
            if (!group.hasStarted && playerTransform.position.x >= group.startX){
                group.hasStarted = true;
                StartCoroutine(SpawnEnemies(group));
            }
            
        }
    }

    private IEnumerator SpawnEnemies(SpawnGroup group){
        for (int i = 0; i < group.enemyCount; i++){
            group.enemyPool.GetObjectFromPool(group.spawnPoint.position);
            yield return new WaitForSeconds(group.spawnInterval);
        }
    }
}
