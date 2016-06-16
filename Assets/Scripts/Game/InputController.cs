using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
  public   bool doInputChecking = true;
    public GameObject pillow;
    int eggsForAchievment = 0;
	// Use this for initialization
	void Start () {
        HiddePillow();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckInput(Input.GetTouch(0).position);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckInput(Input.mousePosition);
            }
        }
        
	}

    void CheckInput(Vector3 pos)
    {
        if (doInputChecking)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            Collider2D otherCol = Physics2D.OverlapPoint(touchPos);
            if (otherCol)
            {

                eggsForAchievment++;
                GameObject.Find("GameController").GetComponent<Achievments>().EggsAllTime(GPS.achievement_save_10000_eggs);
                if (eggsForAchievment == 5)
                {
                    GameObject.Find("GameController").GetComponent<Achievments>().EggsAllTime(GPS.achievement_save_50000_eggs);
                    eggsForAchievment = 0;
                }
                if (otherCol.gameObject.CompareTag("Egg"))
                {
                    Kill(otherCol);
                }
                if (otherCol.gameObject.CompareTag("Eggs"))
                {
                    Kill(otherCol);
                  
                    
                }
                if (otherCol.gameObject.CompareTag("Coin"))
                    Kill(otherCol);
                if (otherCol.gameObject.CompareTag("Slug"))
                {
                    Invoke("NextSlug",10);
                    Invoke("NextRabbit", 15);
                    Kill(otherCol);
                 
                }
                if (otherCol.gameObject.CompareTag("Rabbit"))
                {
                    Kill(otherCol);
                    Invoke("NextSlug", 10);
                    Invoke("NextRabbit", 15);
                    
                }
                if (otherCol.gameObject.CompareTag("Pillow"))
                {
                    GameObject.Find("GameController").GetComponent<Achievments>().CheckPillows();
                    Kill(otherCol);
                    ShowPillow();
                    Invoke("HiddePillow", 12);
                    Invoke("NextPillow", 30);
                }

            }
        }
    }
    void Kill(Collider2D col)
    {
        
        col.GetComponent<Killing>().KillMe();
       // col.transform.gameObject.SendMessage("KillMe", 0, SendMessageOptions.DontRequireReceiver);
    }

  void StopInputChecking()
    {
        doInputChecking = false;
    }

    void NextSlug()
    {
        GameObject.Find("GameController").GetComponent<Spawner>().nextSlug = true;
    }
    void NextRabbit()
    {
        GameObject.Find("GameController").GetComponent<Spawner>().nextRabbit = true;
    }
    void NextPillow()
    {
        GameObject.Find("GameController").GetComponent<Spawner>().nextPillow = true;
    }
  
    void ShowPillow()
    {
        pillow.SetActive(true);
    }
    void HiddePillow()
    {
        pillow.SetActive(false);
    }
}
