using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(TransitionToNight()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TransitionToNight()
     {
         yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(sceneName:"CenaNoite");
        
     }
}
