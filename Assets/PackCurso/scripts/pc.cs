using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pc : MonoBehaviour {
private Rigidbody2D playerrb ;
public float speed;
public float jumpforce;
public bool IsLookLeft;
    // Start is called before the first frame update
    void Start()
    {
         playerrb = GetComponent <Rigidbody2D>();      


    }

    // Update is called once per frame
    void Update(){
     float h = Input.GetAxisRaw("Horizontal");
     if (h>0 && IsLookLeft == true )
     {
        Flip ();
     }
     else if (h<0 && IsLookLeft == false)
     {
        Flip();
     }
     float speedy = playerrb.velocity.y;
     if (Input.GetButtonDown("Jump"))
     {
        playerrb.AddForce(new Vector2 (0,jumpforce)); 
     }
     playerrb.velocity =  new Vector2 (h * speed,speedy);


    }
    void Flip ()
    {
       IsLookLeft = !IsLookLeft;
        float x = transform.localScale.x * -1; // INVERTE O SINAL DO SCALE x
         transform. localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

    }
}
