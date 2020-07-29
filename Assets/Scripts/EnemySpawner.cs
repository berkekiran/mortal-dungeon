using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;
    public List<GameObject> enemyIns;
    public Transform[] enemySpawnPoints;
    public int spawnIndex;

    void Start()
    {
        EnemySpawn();
    }

    void Update() {
        
    }

    void EnemySpawn(){
        for(spawnIndex=0; spawnIndex < enemySpawnPoints.Length; spawnIndex++){
            if(enemySpawnPoints [spawnIndex].CompareTag("EnemyType1"))
                enemyIns.Add(Instantiate (enemyType1, new Vector3(enemySpawnPoints [spawnIndex].position.x, enemySpawnPoints [spawnIndex].position.y, 0f), Quaternion.identity, GameObject.Find("Enemies").transform) as GameObject);
            else if(enemySpawnPoints [spawnIndex].CompareTag("EnemyType2"))
                enemyIns.Add(Instantiate (enemyType2, new Vector3(enemySpawnPoints [spawnIndex].position.x, enemySpawnPoints [spawnIndex].position.y, 0f), Quaternion.identity, GameObject.Find("Enemies").transform) as GameObject);
            else if(enemySpawnPoints [spawnIndex].CompareTag("EnemyType3"))
                enemyIns.Add(Instantiate (enemyType3, new Vector3(enemySpawnPoints [spawnIndex].position.x, enemySpawnPoints [spawnIndex].position.y, 0f), Quaternion.identity, GameObject.Find("Enemies").transform) as GameObject);
        }
    }

}