using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera cam;
    public Transform playerTransform;
    public Transform leftCameraLimit, rightCameraLimit, topCameraLimit, bottomCameraLimit;
    public float speedCam;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Player = GameObject.FindWithTag("Player");
        playerTransform = Player.transform;
        speedCam = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;
        CamController();

    }

    void CamController()
    {
        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;
        if (cam.transform.position.x < leftCameraLimit.position.x && playerTransform.position.x + 3 > leftCameraLimit.position.x)
        {
            posCamX = leftCameraLimit.position.x;
        }
        else if (cam.transform.position.x < rightCameraLimit.position.x && playerTransform.position.x + 2 > rightCameraLimit.position.x)
        {
            posCamX = rightCameraLimit.position.x;
        }

        if (cam.transform.position.y < bottomCameraLimit.position.y && playerTransform.position.y - 0.6 < bottomCameraLimit.position.y)
        {
            posCamY = bottomCameraLimit.position.y;
        }
        else if (cam.transform.position.y > topCameraLimit.position.y && playerTransform.position.y > topCameraLimit.position.y)
        {
            posCamY = topCameraLimit.position.y;
        }

        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }
}
