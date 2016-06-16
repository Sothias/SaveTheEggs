using UnityEngine;
using System.Collections;


public class Killing : MonoBehaviour {

    bool up;
   public float speedMulti = 1.5f;

    public AudioClip clip;
    
    GUIController guiController;
     bool isDead = false;
	// Use this for initialization
	void Start () {
         
        guiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIController>();
	}


    public void KillMe() {
        if (!isDead)
        {
           

            SendMessage("StopMoving");
            AudioSource.PlayClipAtPoint(clip, transform.position);
            Destroy(gameObject, 0.4F);
            Vector3 pos = transform.position;
            pos.z= 5;
            transform.position = pos;
            isDead = true;
            guiController.points += gameObject.GetComponent<Moving>().points;
            if(!gameObject.CompareTag("Coin"))
            GameObject.Find("GameController").GetComponent<Achievments>().CheckPoints((int)guiController.points);
            if (gameObject.CompareTag("Coin"))
                GameObject.Find("GameController").GetComponent<Spawner>().coins++;
            
            if (gameObject.CompareTag("Slug"))
            {
                //     Time.timeScale = 0.6F;
                   
                GameObject.Find("GameController").GetComponent<Achievments>().CheckSlugs();
                up = true;
                GameObject.Find("GameController").GetComponent<Spawner>().speedDown = true;
                GameObject.Find("GameController").GetComponent<Spawner>().SetSpeed();
                GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
                foreach (GameObject egg in eggs)
                {
                    egg.GetComponent<Moving>().speed -= speedMulti;
                }
                Invoke("DefaultTimeScale", 2);
            }
            if(gameObject.CompareTag("Rabbit"))
            {
               
                // Time.timeScale = 1.3F;
               
              
                GameObject.Find("GameController").GetComponent<Spawner>().speedUp = true;
                GameObject.Find("GameController").GetComponent<Spawner>().SetSpeed();
                up = false;
                GameObject.Find("GameController").GetComponent<Achievments>().CheckRabbits();

                GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
                foreach (GameObject egg in eggs)
                {
                    egg.GetComponent<Moving>().speed += speedMulti;
                }
                Invoke("DefaultTimeScale", 2);
            }
            if(gameObject.CompareTag("Eggs"))
            {
                GameObject.Find("GameController").GetComponent<Achievments>().CheckColoredEggs();
                GameObject.Find("GameController").GetComponent<Spawner>().startMassSpawn = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
         
	}
   
   void DefaultTimeScale()
    {
   GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
        foreach (GameObject egg in eggs)
        {
            if (up)
            {
                egg.GetComponent<Moving>().speed += speedMulti;
            }
            else
            {
                egg.GetComponent<Moving>().speed += speedMulti;
            }
        }
    }
    GameObject[] GetAllClones()
    {
        GameObject[] egg = GameObject.FindGameObjectsWithTag("Egg");


        return egg;
    }
}
