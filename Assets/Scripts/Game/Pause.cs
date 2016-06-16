using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

   
    public bool paused;
    public GameObject pausePanel;


    // Use this for initialization
    void Start () {
        paused = false;
        pausePanel.SetActive(false); 
	}
	
	// Update is called once per frame
	void Update () {
	

        

	}
    public void SetPause()
    {
        paused = !paused;
      
            if (paused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            GameObject.Find("GameController").GetComponent<InputController>().doInputChecking = false;


        } else if (!paused)
        {
            GameObject.Find("GameController").GetComponent<InputController>().doInputChecking = true;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

    }

     public void Restart()
    {


        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0F;

        
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainScene");
    }

}
