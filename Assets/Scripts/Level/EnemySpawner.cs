using System.Collections.Generic;
using UnityEngine;

namespace Beehaw.Level
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnLocations;
        [SerializeField] private int enemyLimit;
        [SerializeField] private float spawnTime;
        [SerializeField] private bool usesTriggerVolume;
        private bool canSpawn = true;
        private float spawnTimer = 0f;
        private int spawnerIndex = 0;
        private int enemiesSpawned = 0;
        private List<GameObject> spawnedEnemies = new List<GameObject>();

        private void Start()
        {
            if (usesTriggerVolume)
            {
                canSpawn = false;
            }    
        }

        private void Update()
        {
            spawnedEnemies = UpdateSpawnedEnemies();
            enemiesSpawned = spawnedEnemies.Count;
            if(enemiesSpawned < enemyLimit && spawnTimer > spawnTime && canSpawn)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
            spawnTimer += Time.deltaTime;
        }

        private void SpawnEnemy()
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnLocations[spawnerIndex].position, Quaternion.identity);
            spawnedEnemies.Add(spawnedEnemy);
            if (spawnerIndex == spawnLocations.Length - 1)
            {
                spawnerIndex = 0;
            } 
            else
            {
                spawnerIndex++;
            }
        }

        private List<GameObject> UpdateSpawnedEnemies()
        {
            List<GameObject> updatedEnemyList = new List<GameObject>();
            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy != null)
                {
                    updatedEnemyList.Add(enemy);
                }
            }
            return updatedEnemyList;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(usesTriggerVolume && collision.CompareTag("Player"))
            {
                canSpawn = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (usesTriggerVolume && collision.CompareTag("Player"))
            {
                canSpawn = false;
            }
        }
    }

}