using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class WaveScript : MonoBehaviour
{
    WaveManager WaveManager;
    TextMeshProUGUI TextMesh;
    // Start is called before the first frame update
    void Start()
    {
        WaveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        TextMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int waveCount = WaveManager.checkLevel;

        TextMesh.SetText("Wave " + waveCount);
    }
}
