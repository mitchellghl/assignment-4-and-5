using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyClone;
    private GameObject lamp;

    float timer = 20;

    void Start()
    {
        Debug.Log("Hey, look ma, I made it");
        lamp = GameObject.FindGameObjectWithTag("Lamp");
        PleaseSpawnEnemy();
    }

    void PleaseSpawnEnemy()
    {
        enemyClone = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        enemyClone.gameObject.GetComponent<TestEnemyMove>().home = transform;
    }

    void Update()
    {
        timer = timer - Time.deltaTime;

        if(timer <= 0)
        {
            timer = 20;
            PleaseSpawnEnemy();
        }
    }

}
