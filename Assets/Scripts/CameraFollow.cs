using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (playerTransform.position.x + 1, 2.5f, -10);
    }
}
