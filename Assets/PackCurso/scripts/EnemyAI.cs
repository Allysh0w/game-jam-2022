using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform playerTransform; 
    private Transform enemyTransform; 
    public float speed = 3f;
    private SpriteRenderer spriteRenderer;
    private EnemyAttack enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(enemyAttack.playerInRange){

       // } else {
           // ChasePlayer();
      //  }
    }

    private void ChasePlayer()
    {
        var playerIsToRight = enemyTransform.position.x < playerTransform.position.x;

        if(playerIsToRight == true)
        {   
            enemyTransform.Translate(Vector3.right * Time.deltaTime * speed);
            spriteRenderer.flipX = false;
        }else{
            enemyTransform.Translate(Vector3.left * Time.deltaTime * speed);
            spriteRenderer.flipX = true;
        }
    }
}
