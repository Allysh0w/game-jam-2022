using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerCOPY : MonoBehaviour
{
    public GameObject[] enemies;
    public bool canSpawn;
    public int waveLevel; 
    private bool countCoroutine;

    private int enemyCountBase = 7;
    private int enemiesLeft = 7;

    private float spawnCooldown = 2f;

    private float nextWaveCooldown = 14f;

    // Start is called before the first frame update
    void Start()
    {
        waveLevel = 1;
        countCoroutine = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (enemiesLeft > 0)
        {
            Spawn();

        }

    }

     void Spawn()
     {
        StartCoroutine(AwaitAndRespawn());
        countCoroutine = false;
     }

     private IEnumerator AwaitAndRespawn()
    {
        while (countCoroutine)
        {
             for (int i = 0; i < enemyCountBase; i++)
            {
                int enemyIndex = Random.Range(0, enemies.Length);
                Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
                enemiesLeft -= 1;

                if(enemiesLeft <= 0)
                {
                    yield return new WaitForSeconds(nextWaveCooldown);
                    PassWaveLevel();
                }
                yield return new WaitForSeconds(spawnCooldown);
            }
        }
        
    }


    private void PassWaveLevel ()
    {
        waveLevel = waveLevel + 1;
        enemyCountBase = 10 + (Mathf.FloorToInt(enemyCountBase * 1.5f));
        enemiesLeft = enemyCountBase;
        spawnCooldown = Mathf.Clamp(spawnCooldown - 0.25f, 0.5f, 10f);
        countCoroutine = true;
    }
}
