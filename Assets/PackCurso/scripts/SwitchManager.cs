using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchManager : MonoBehaviour
{
    public Animator animator;
    public GameObject gameobject;
    public WaveManager waveManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            StartCoroutine(LevelFinished());
        }
    }

    IEnumerator LevelFinished() {
        
        animator.SetTrigger("altarBlink");
        yield return new WaitForSeconds(5.0f);
        gameobject.SetActive(true);
        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }

        SceneManager.LoadScene(sceneName:"CenaDia");
    
    }


}
