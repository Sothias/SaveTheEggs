using UnityEngine;
using System.Collections;

public class DeathCollision : MonoBehaviour {
    public GameObject gameController;
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Egg" ||coll.gameObject.tag == "Eggs" ||coll.gameObject.tag == "Slug" || coll.gameObject.tag == "Rabbit"   )
        {
            StopGame();
        }
    }

    void StopGame()
    {
        gameController.SendMessage("StopSpawning");
        gameController.SendMessage("ShowHighscore");
        gameController.SendMessage("StopInputChecking");
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
        GameObject.Find("Fence").GetComponent<FenceMoving>().DeathMoving();
        foreach(GameObject egg in eggs)
        {
            egg.SendMessage("StopMoving");
        }
        if(GameObject.FindGameObjectWithTag("Slug") != null)
            GameObject.FindGameObjectWithTag("Slug").SendMessage("StopMoving", SendMessageOptions.DontRequireReceiver);
        if (GameObject.FindGameObjectWithTag("Rabbit") != null)
            GameObject.FindGameObjectWithTag("Rabbit").SendMessage("StopMoving", SendMessageOptions.DontRequireReceiver);
        if (GameObject.FindGameObjectWithTag("Eggs") != null)
            GameObject.FindGameObjectWithTag("Eggs").SendMessage("StopMoving", SendMessageOptions.DontRequireReceiver);
        
        
    }
}
