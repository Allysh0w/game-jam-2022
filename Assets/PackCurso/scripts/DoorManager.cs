using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    private int doorHp = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorHp <= 0)
        {
            SceneManager.LoadScene(sceneName: "GameOver");
        }
    }

    public void DoorTakeDamage(int damage) {
        doorHp = doorHp - damage;
    }
}
