using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameObject hitGameObject = collision.gameObject;


        if (hitGameObject.tag == "Enemy")
        {
            var damage = GameObject.Find("Player").GetComponent<PlayerMain>().attackDamage;
            hitGameObject.GetComponent<EnemyAI>().EnemyTakeDamage(damage);
            hitGameObject.GetComponent<Animator>().SetTrigger("tookHit"); //tookHit
        
            CheckEnemyHp(hitGameObject);
        }
        
    }

    private void CheckEnemyHp(GameObject hitGameObject){
        if (hitGameObject.GetComponent<EnemyAI>().enemyHP <= 0) {
            hitGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
             hitGameObject.GetComponent<Animator>().SetTrigger("die");
        }
    }
}
