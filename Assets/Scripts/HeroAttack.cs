using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
 {
 
     public float heroCooldown = 10.0f;
     public int damage = 1;
 
     public bool enemyInRange = false;
     private bool doorInRange = false;
     public bool DoorInRange => doorInRange;
     private bool canAttack = true;
     private Animator animator;
     private Rigidbody2D rigidbody2D;

    private string tagToAttack = "Enemy";

    private void Start() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
     }
 
    private void Update()
     {
            Attack();
            animator.SetFloat("velocityMag",rigidbody2D.velocity.magnitude);
     }


     void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == tagToAttack)
          {
             enemyInRange = true;
             
         } 
     }

       void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.tag == tagToAttack)
          {
             enemyInRange = false;
         }

     }


     IEnumerator AttackCooldown()
     {
         canAttack = false;
         yield return new WaitForSeconds(heroCooldown);
         canAttack = true;
     }

     void Attack() {
       var heroHP = GetComponent<HeroAI>().heroHP;
        
        if (enemyInRange && canAttack && heroHP > 0)
         {
            animator.SetTrigger("attack");
            GameObject.FindWithTag(tagToAttack).GetComponent<EnemyAI>().EnemyTakeDamage(damage);
            StartCoroutine(AttackCooldown());
         }

     }

 /*     void CheckheroIsAlive(){
         GameObject.FindWithTag("Enemy").GetComponent<EnemyController>().isDead
     } */

 }