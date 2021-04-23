using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject pizzaEnemy, bigPizzaEnemy, refrigerantEnemy, donutEnemy, boss;
    GameManager gm;

    bool spawnTime = true;
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        if(gm.gameState == GameManager.GameState.MENU) spawnTime = true;
        if(gm.gameState == GameManager.GameState.MENU && spawnTime) SpawnEnemies(); 
        
    }


    void SpawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in enemies){
            Destroy(obj);
        }

        Instantiate(pizzaEnemy, new Vector3(1.30f, -0.6f, 0), Quaternion.identity);
        Instantiate(pizzaEnemy, new Vector3(5.60f, -0.60f, 0), Quaternion.identity);
        Instantiate(bigPizzaEnemy, new Vector3(25.0f, 8.80f, 0), Quaternion.identity);
        Instantiate(refrigerantEnemy, new Vector3(45.0f, 4.3f, 0), Quaternion.identity);
        Instantiate(donutEnemy, new Vector3(85.0f, 0.3f, 0), Quaternion.identity);
        Instantiate(boss, new Vector3(107.0f, 0.5f, 0), Quaternion.identity);
        spawnTime = false;
    }
}
