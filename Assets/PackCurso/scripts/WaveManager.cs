using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemies;
    public bool canSpawn;
    public int checkLevel; 
    public int enemyCountBase;
    private bool countCorroutine;
    

    // Start is called before the first frame update
    void Start()
    {
        checkLevel = 1;
        countCorroutine = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(checkLevel) {
            case 1: 
                if(canSpawn){
                    Debug.Log("Level 1 => ");
                    Spawn();
                    canSpawn = false;
                }
                break;
                
            
            case 2: 
            if(canSpawn){
                    Spawn();
                }
                Debug.Log("level 2");
                break;

            default:
                Debug.Log("Default value => ");
                break;
        }
    }

     void Spawn()
     {
        StartCoroutine(AwaitAndRespawn());
        countCorroutine = false;
     }

     private IEnumerator AwaitAndRespawn()
    {
        var enemyCount = enemyCountBase * checkLevel;
        while (countCorroutine)
        {
             for (int i = 0; i < enemyCount; i++)
            {
                int enemyIndex = Random.Range(0, enemies.Length);
                Instantiate(enemies[enemyIndex], transform.position, transform.rotation); 
                yield return new WaitForSeconds(1.0f);
            }
        }
        
    }
}
