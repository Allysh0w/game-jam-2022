using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : MonoBehaviour
{
    private Transform enemyTransform; 
    private Transform heroTransform; 
    public float speed = 3f;
    private SpriteRenderer spriteRenderer;
    private HeroAttack heroAttack;
    private GameObject Enemy;
    private Rigidbody2D rigidbody2D;
    public int heroHP;

    private string tagToAttack = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        //enemyTransform = GameObject.Find("Enemy").GetComponent<Transform>();
        heroTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        heroAttack = GetComponent<HeroAttack>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Enemy = GameObject.FindWithTag(tagToAttack);
        enemyTransform = Enemy.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (heroHP <= 0){
            Destroy(gameObject, 0.85f);
            return;
        }

        if(heroAttack.enemyInRange == false){
          // ChaseEnemy();
        }
    }

    private void ChaseEnemy()
    {
        var enemyIsToRight = heroTransform.position.x < enemyTransform.position.x;

        if(enemyIsToRight == true)
        {   
            //heroTransform.Translate(Vector3.right * Time.deltaTime * speed);
            var velocityY = rigidbody2D.velocity.y;
            rigidbody2D.velocity = new Vector2(1 * speed, velocityY);

            spriteRenderer.flipX = false;
        }else{
            //heroTransform.Translate(Vector3.left * Time.deltaTime * speed);
            var velocityY = rigidbody2D.velocity.y;
            rigidbody2D.velocity = new Vector2(-1 * speed, velocityY);
            spriteRenderer.flipX = true;
        }
    }

    public void heroTakeDamage(int damage){
        heroHP = heroHP - damage;
    }

}
