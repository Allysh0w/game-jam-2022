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
    private GameObject Player;
    private Rigidbody2D rigidbody2D;
    public int enemyHP;

    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAttack = GetComponent<EnemyAttack>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        playerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0){
            Destroy(gameObject, 0.85f);
            return;
        }

        if(enemyAttack.playerInRange == false && enemyAttack.DoorInRange == false){
           ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        var playerIsToRight = enemyTransform.position.x < playerTransform.position.x;

        if(playerIsToRight == true)
        {   
            //enemyTransform.Translate(Vector3.right * Time.deltaTime * speed);
            var velocityY = rigidbody2D.velocity.y;
            rigidbody2D.velocity = new Vector2(1 * speed, velocityY);

            spriteRenderer.flipX = false;
        }else{
            //enemyTransform.Translate(Vector3.left * Time.deltaTime * speed);
            var velocityY = rigidbody2D.velocity.y;
            rigidbody2D.velocity = new Vector2(-1 * speed, velocityY);
            spriteRenderer.flipX = true;
        }
    }

    public void EnemyTakeDamage(int damage){
        enemyHP = enemyHP - damage;
    }

}
