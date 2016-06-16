using UnityEngine;
using System.Collections;

public class Pillow : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Egg" || coll.gameObject.tag == "Rabbit" || coll.gameObject.tag == "Eggs" || coll.gameObject.tag == "Slug" ||coll.gameObject.tag == "LastEgg")
        {
         

            // otherCol.transform.gameObject.SendMessage("KillMe", 0, SendMessageOptions.DontRequireReceiver);
            coll.transform.gameObject.SendMessage("KillMe", 0, SendMessageOptions.DontRequireReceiver);
            if (coll.gameObject.tag == "LastEgg")
            {
                GameObject.Find("GameController").GetComponent<Spawner>().levelText.text = "Let's go";
                GameObject.Find("GameController").gameObject.GetComponent<Spawner>().lastStartEgg = true;
                GameObject.Find("Pillow").SetActive(false);
                GameObject.Find("GameController").GetComponent<GUIController>().starting = false;
                Invoke("StartGame",2);
            }

        }
    }
    public void HideEggPad()
    {
        gameObject.SetActive(false);

    }
    void StartGame()
    {
        GameObject.Find("GameController").gameObject.GetComponent<Spawner>().StartSpawnInterval();
    }
    // Update is called once per frame
    void Update () {
	
	}
}
