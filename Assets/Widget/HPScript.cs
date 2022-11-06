using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class HPScript : MonoBehaviour
{
    private PlayerMain PlayerMain;
    TextMeshProUGUI TextMesh; 
    // Start is called before the first frame update
    void Start()
    {
        PlayerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
        TextMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerHP = PlayerMain.playerHp;

        Debug.Log("playerHP --> " + playerHP);

        TextMesh.SetText("" + playerHP);
    }
}
