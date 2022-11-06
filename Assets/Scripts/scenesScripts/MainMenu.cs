using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    void Play()
    {
        if(Input.GetButtonDown("Keycode.Return") == true)
        {
            Debug.Log("Enter foi pressionado.");
            SceneManager.LoadScene(sceneName:"CenaNoite");
        }
    }
    public void Start1()
    {
        SceneManager.LoadScene(sceneName:"CenaNoite");
    }
    public void ReturnMenu(){

        SceneManager.LoadScene("Menu");
    }
    public void PlayAgain(){
        SceneManager.LoadScene("CenaNoite");
    }
}
