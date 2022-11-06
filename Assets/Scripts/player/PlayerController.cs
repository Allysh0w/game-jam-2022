using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int speed = 2;

    private bool isOnGround;
    private bool isLookingToLeft;
    private bool isAttacking = false;
    private bool isWalking = false;
    private bool isDead = false;
    public bool isShieldUp = false;
    public bool canTakeDamage = true;
    private bool isDashing = false;
    private bool isDodging = false;
    public bool isTakingDamage = false;

    private int lightAttackIndex = 0;
    private float lightAttackCooldown = 1f;
    private float lightAttackTimedown = 0f;

    private float jumpForce = 200f;

    private float doubleHorizontalTimedown = 0f;
    private float doubleHorizontalCooldown = 0.5f;
    private int horizontalTaps = 0;

    private float dodgeForce = 75f;
    private float dodgeCooldown = 0.75f;
    private bool canDodge = true;

    private float dashForce = 100f;
    private float dashCooldown = 0.75f;
    private bool canDash = true;


    public GameObject[] heroes;
    public int currentHero = 1;
    private bool canSpawn = true;

    public Rigidbody2D playerRB;
    private GameObject hand;
    private GameObject heroSpawner;

    public Animator playerAnimator;
    private TrailRenderer trailRender;
    [SerializeField] private GameObject hitBox;

    private PlayerMain PlayerMain;
    private float originalGravity;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        hand = GameObject.Find("hand").gameObject;
        playerAnimator = GetComponent<Animator>();
        trailRender = GetComponent<TrailRenderer>();
        PlayerMain = GetComponent<PlayerMain>();
        heroSpawner = GameObject.Find("heroSpawner").gameObject;
        originalGravity = playerRB.gravityScale;
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float currentVelocityX = playerRB.velocity.x;
        float currentVelocityY = playerRB.velocity.y;

        /*
        Debug.Log("canDash -->" + canDash);
        Debug.Log("isDashing -->" + isDashing);
        Debug.Log("horizontalTaps -->" + horizontalTaps);
        Debug.Log("-----------------------------------");
        */
        Debug.Log("isTakingDamage --> " + isTakingDamage);
        Debug.Log("canTakeDamage --> " + canTakeDamage);

        // Cooldowns / Timedowns
        lightAttackTimedown = Mathf.Clamp(lightAttackTimedown - Time.deltaTime, 0f, lightAttackCooldown);
        doubleHorizontalTimedown = Mathf.Clamp(doubleHorizontalTimedown - Time.deltaTime, 0f, doubleHorizontalCooldown);

        if(doubleHorizontalTimedown == 0f)
        {
            horizontalTaps = 0;
        }

        // PLAYER DEATH MANAGER
        if (PlayerMain.playerHp <= 0)
        {
            isDead = true;
            playerAnimator.SetBool("isDead", isDead);
            return;
        }

        // PLAYER MOVEMENT HANDLER
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


        if(currentVelocityX == 0 && isOnGround)
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
        
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
        }


        // ----- PLAYER CONTROLLERS ----- //
        // Debug.Log("doubleHorizontalTimedown -->" + doubleHorizontalTimedown);

        // DASH HANDLER
        if (Input.GetButtonDown("Horizontal"))
        {
            DashHandler();
        };

        // DODGE 
        if (Input.GetButtonDown("Dodge") && isWalking && canDodge && !isShieldUp && !isDashing && isOnGround)
        {
            StartCoroutine(Dodge());
        };

        // DEFENSE
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
        if (Input.GetButtonDown("Fire1") && !isShieldUp && !isAttacking && !isDashing)
        {
            LightAttack();
        }

        // SPAWN HEROES
        if (Input.GetButtonDown("Summon") && canSpawn)
        {
            SpawnHero();
        }

         // ANIMATIONS

        playerAnimator.SetBool("isOnGround", isOnGround);
        playerAnimator.SetBool("isAttacking", isAttacking);
        playerAnimator.SetFloat("lightAttackIndex", (int)lightAttackIndex);
        playerAnimator.SetInteger("horizontalAxis", (int)horizontalAxis);
        playerAnimator.SetFloat("yVelocity", currentVelocityY);
        playerAnimator.SetBool("isShieldUp", isShieldUp);
        playerAnimator.SetBool("isDashing", isDashing);
        playerAnimator.SetBool("isDodging", isDodging);
        playerAnimator.SetBool("isTakingDamage", isTakingDamage);


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


    private void  DashHandler ()
    {

        if (doubleHorizontalTimedown > 0f && horizontalTaps != 0)
        {
            if (canDash && !isShieldUp)
            {
                horizontalTaps = 0;
                StartCoroutine(Dash());
            }
        }
        else
        {
            doubleHorizontalTimedown = doubleHorizontalCooldown;
            horizontalTaps += 1;
        }

    }
    public void CreateHitBox()
    {
        GameObject createdHitBox = Instantiate(hitBox, hand.transform.position, transform.localRotation);
        Destroy(createdHitBox, 0.25f);
    }

    public void StopTakingDamage()
    {
        isTakingDamage = false;
    }

    public void StopDashing ()
    {
        isDashing = false;
        trailRender.emitting = false;
    }

    public void StopDodge()
    {
        isDodging = false;
    }


    private IEnumerator Dash()
    {
        Debug.Log("!!!!! DASH !!!!!!");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        canDash = false;
        trailRender.emitting = true;
        isDashing = true;
        playerRB.gravityScale = 0;
         playerRB.velocity = new Vector2(dashForce * transform.localScale.x, 0f);
        playerRB.AddForce(new Vector2(dashForce * horizontalAxis, 0f));
        yield return new WaitForSeconds(0.25f);
        playerRB.gravityScale = originalGravity;
        StopDashing();

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Dodge()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        canDodge = false;
        isDodging = true;
        playerRB.velocity = new Vector2(dodgeForce * transform.localScale.x, 0f);
        playerRB.AddForce(new Vector2(dodgeForce * horizontalAxis, 0f));

        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }


    public void SetPlayerHero(int heroIndex)
    {
        currentHero = heroIndex;
    }


    private void SpawnHero ()
    {
        GameObject heroToSpawn = heroes[currentHero - 1];
        if (heroToSpawn)
        {
            Instantiate(heroToSpawn, heroSpawner.transform.position, transform.localRotation);
            canSpawn = false;
        }
    }
}
