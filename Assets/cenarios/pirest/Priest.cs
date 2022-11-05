using UnityEngine;
using System.Collections;

public class Priest : MonoBehaviour
{
    public float MovementSpeed = 1;

    public float IdleTime = 1;
    public float WalkingTime = 2;
    
    private Animator animator;
    
    private int movementDirection = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WalkCycle());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        movementDirection *= -1;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator WalkCycle()
    {
        animator.SetBool("IsWalking", false);
        
        yield return new WaitForSeconds(IdleTime);

        animator.SetBool("IsWalking", true);
        var timer = WalkingTime;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Translate(Vector3.right * Time.deltaTime * MovementSpeed * movementDirection);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(WalkCycle());
    }
}
