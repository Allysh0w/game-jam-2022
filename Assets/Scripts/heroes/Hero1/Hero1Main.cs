using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1Main : MonoBehaviour
{

    public int hp = 300;
    public int damage = 15;
    public bool isTakingDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
       
        isTakingDamage = true;
        hp = hp - damage;
    }


}
