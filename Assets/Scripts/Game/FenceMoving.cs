using UnityEngine;
using System.Collections;

public class FenceMoving : MonoBehaviour {
    bool stop = true;
    float speed = 0.2f;
    bool up = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!stop)
        {
            if (up)
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }

             
             
        }

	}
    void MoveDown()
    {
         
        Vector3 vel = new Vector3();

        vel = Vector3.down * speed * Time.deltaTime;
        transform.Translate(vel);
    }
    void MoveUp()
    {
        Vector3 vel = new Vector3();

        vel = Vector3.up * speed * Time.deltaTime;
        transform.Translate(vel);
    }
 public   void StartMoving()
    {

        stop = false;
        Invoke("StopMoving", Random.Range(5,15));
    }
    void StopMoving()
    {
        ToogleMoving();
        stop = true;
        Invoke("StartMoving", Random.Range(5,15));
    }
     
    void ToogleMoving()
    {
        up = !up;
    }
    public void DeathMoving()
    {
        CancelInvoke("StartMoving");
        CancelInvoke("StopMoving");
        stop = true;
    }
    
   
}
