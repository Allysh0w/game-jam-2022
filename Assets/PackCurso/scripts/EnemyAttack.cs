using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
 {
 
     public float enemyCooldown = 10.0f;
     public int damage = 1;
 
     public bool playerInRange = false;
    public bool heroInRange = false;
    private bool doorInRange = false;
     public bool DoorInRange => doorInRange;
     private bool canAttack = true;
     private Animator animator;
     private Rigidbody2D rigidbody2D;

    private string tagToAttack = "Player";
     
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
             playerInRange = true;
             
         } else if (collision.gameObject.tag == "Door") {
            doorInRange = true;
         }
        else if (collision.gameObject.tag == "Hero")
        {
            heroInRange = true;
        }
    }

       void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.tag == tagToAttack)
          {
             playerInRange = false;
         }

        if (collision.gameObject.tag == "Hero")
        {
            heroInRange = false;
        }

        if (collision.gameObject.tag == "Door")
          {
             animator.SetBool("velocityMag", true);
             doorInRange = false;
         }
     }


     IEnumerator AttackCooldown()
     {
         canAttack = false;
         yield return new WaitForSeconds(enemyCooldown);
         canAttack = true;
     }

     void Attack() {
       var enemyHP = GetComponent<EnemyAI>().enemyHP;
        
        if (playerInRange && canAttack && enemyHP > 0)
         {
            animator.SetTrigger("attack");
            GameObject.FindWithTag(tagToAttack).GetComponent<PlayerMain>().PlayerTakeDamage(damage);
            StartCoroutine(AttackCooldown());
         }

         if (doorInRange && canAttack && enemyHP > 0){
            animator.SetTrigger("attack");
            GameObject.FindWithTag("Door").GetComponent<DoorManager>().DoorTakeDamage(damage);
            StartCoroutine(AttackCooldown());
         }

        if (heroInRange && canAttack && enemyHP > 0)
        {
            animator.SetTrigger("attack");
            GameObject.FindWithTag("Hero").GetComponent<HeroAI>().heroTakeDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }

 /*     void CheckEnemyIsAlive(){
         GameObject.FindWithTag("Player").GetComponent<PlayerController>().isDead
     } */

 }