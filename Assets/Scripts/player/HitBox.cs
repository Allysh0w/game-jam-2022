using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameObject hitGameObject = collision.gameObject;
        Debug.Log("DEAL DAMAGE!");
        Debug.Log(hitGameObject.tag);


        if (hitGameObject.tag == "Enemy")
        {
            Destroy(hitGameObject);
        }
        
    }

 
}
