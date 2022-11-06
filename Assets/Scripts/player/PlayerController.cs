using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maximumHp = 100;
    public int hp;
    public int playerLevel = 1;
    public int playerExperience = 0;
    public int attackDamage = 10;
    private int speed = 2;
    public int playerKills = 0;
    public int nextLevelExperience = 1000;

    private bool isOnGround;
    private bool isLookingToLeft;
    private bool isAttacking = false;
    private bool isTakingDamage = false;
    private bool isWalking = false;
    private bool isShieldUp = false;
    private bool isDead = false;
    private int lightAttackIndex = 0;
    private float lightAttackCooldown = 1f;
    private float lightAttackTimedown = 0f;

    private float dashForce = 2500f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;
    // private bool isDashing = false;
    private bool canDash = true;
    



    public Rigidbody2D playerRB;
    private GameObject hand;
    private Animator playerAnimator;
    private TrailRenderer trailRender;
    [SerializeField] private GameObject hitBox;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        hand = GameObject.Find("hand").gameObject;
        playerAnimator = GetComponent<Animator>();
        hp = maximumHp;
        trailRender = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float currentVelocityX = playerRB.velocity.x;
        float currentVelocityY = playerRB.velocity.y;

        lightAttackTimedown = Mathf.Clamp(lightAttackTimedown - Time.deltaTime, 0f, lightAttackCooldown);

        /*
        Debug.Log(isAttacking);
        Debug.Log(lightAttackIndex);
        Debug.Log(lightAttackTimedown);
        Debug.Log("-----------------");
        */

        if (hp <= 0)
        {
            isDead = true;
            playerAnimator.SetBool("isDead", isDead);
            return;
        }

        // PLAYER MOVEMENT
        if (isTakingDamage || isShieldUp)
        {
            horizontalAxis = 0;
        }

        if (isAttacking)
        {
            // Slow player movement when attacking
            horizontalAxis = horizontalAxis / 3;
        }

        if (currentVelocityY == 0)
        {
            isOnGround = true;
        } else
        {
            isOnGround = false;
        }


        if(currentVelocityX == 0)
        {
            isWalking = false;
        } else
        {
            isWalking = true;
        }

        playerRB.velocity = new Vector2(speed * horizontalAxis, currentVelocityY);


        // PLAYER FLIP
        if (horizontalAxis > 0 && isLookingToLeft && !isAttacking)
        {
            Flip();
        }else if(horizontalAxis < 0 && !isLookingToLeft && !isAttacking)
        {
            Flip();
        }


        // PLAYER JUMP
        /*
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
        }
        */

        // ----- PLAYER CONTROLLERS ----- //
        if(Input.GetButtonDown("Dash") && isWalking && canDash)
        {
            StartCoroutine(Dash());
        };

        if (Input.GetButton("Defense"))
        {
            isShieldUp = true;
        }
        else
        {
            isShieldUp = false;
        }

        // PLAYER ATTACK

        // LIGHT ATTACK
        if (Input.GetButtonDown("Fire1") && !isShieldUp && !isAttacking)
        {
            LightAttack();
        }

        // ANIMATIONS

        playerAnimator.SetBool("isOnGround", isOnGround);
        playerAnimator.SetBool("isAttacking", isAttacking);
        playerAnimator.SetFloat("lightAttackIndex", (int)lightAttackIndex);
        playerAnimator.SetInteger("horizontalAxis", (int)horizontalAxis);
        playerAnimator.SetFloat("yVelocity", currentVelocityY);
        playerAnimator.SetBool("isTakingDamage", isTakingDamage);
        playerAnimator.SetBool("isShieldUp", isShieldUp);

        if (playerExperience >= nextLevelExperience)
        {
            LevelUp();
        }

    }

    private void Flip()
    {
        isLookingToLeft = !isLookingToLeft;
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        playerRB.transform.localScale = new Vector2(scaleX * -1, scaleY);
    }

    public void StopPlayerAttack ()
    {
        Debug.Log("STOP PLAYER ATTACK");
        isAttacking = false;

        if (lightAttackTimedown == 0)
        {
            lightAttackIndex = 0;
        }
        
    }

    private void LightAttack()
    {
        //lightAttackIndex = Mathf.Clamp(lightAttackIndex + 1, 0, 2);
        if (lightAttackIndex == 1)
        {
            lightAttackIndex = 2;
        }
        else
        {
            lightAttackIndex = 1;
        }

        isAttacking = true;

        lightAttackTimedown = lightAttackCooldown; // RESET TIMER

        
    }

    public void CreateHitBox()
    {
        GameObject createdHitBox = Instantiate(hitBox, hand.transform.position, transform.localRotation);
        Destroy(createdHitBox, 0.25f);
    }

    public void PlayerTakeDamage(int damage)
    {
        if (isShieldUp)
        {
            damage = damage / 2;
        }
        isTakingDamage = true;
        hp = hp - damage;
    }



    public void StopTakingDamage()
    {
        isTakingDamage = false;
    }

    public void PlayerKilledEnemy(int experience)
    {
        playerKills = playerKills + 1;
        playerExperience = playerExperience + experience;
    }

    public void LevelUp()
    {
        nextLevelExperience = Mathf.RoundToInt(nextLevelExperience * 1.5f);
        attackDamage = attackDamage + 1;
        maximumHp = maximumHp + 10;
        hp = maximumHp;
    }

    private IEnumerator Dash()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        canDash = false;
        trailRender.emitting = true;
        // isDashing = true;
        float originalGravity = playerRB.gravityScale;
        playerRB.gravityScale = 0;
        // playerRB.velocity = new Vector2(dashForce * transform.localScale.x, 0f);
        playerRB.AddForce(new Vector2(dashForce * horizontalAxis, 0f));

        yield return new WaitForSeconds(dashTime);
        // isDashing = false;
        trailRender.emitting = false;


        playerRB.gravityScale = originalGravity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
