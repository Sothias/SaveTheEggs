using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float speed;
	bool stop = false;
    public float points = 0;
    public bool startEggs = false;
     
	void Start(){
       // int level = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spawner>().levelCounter;
        int min = 2;
        int max =  GameObject.Find("GameController").GetComponent<Spawner>().levelCounter+2;
        if (max >= 7)
            max = 7;
        speed = (int)Random.Range(min, max);
        if (GameObject.Find("GameController").GetComponent<Spawner>().speedUp)
            speed += gameObject.GetComponent<Killing>().speedMulti;
        if (GameObject.Find("GameController").GetComponent<Spawner>().speedDown)
            speed -= gameObject.GetComponent<Killing>().speedMulti;
        if (GameObject.Find("GameController").GetComponent<GUIController>().starting)
            speed += 15;




     //   if (gameObject.CompareTag("Egg") || gameObject.CompareTag("LastEgg"))
      //  {
            Color color = new Color();
            color.b = Random.Range(0.0f, 1.0f);
            color.r = Random.Range(0.0f, 1.0f);
            color.g = Random.Range(0.0f, 1.0f);
            color.a = 1;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        //}

        if (gameObject.CompareTag("Eggs"))
        {
            GameObject[] childEggs = GameObject.FindGameObjectsWithTag("ChildEggs");
            foreach(GameObject child in childEggs)
            {
                Color c = new Color();
                c.a = 1;
                c.b = Random.Range(0.0f, 1.0f);
                c.g = Random.Range(0.0f, 1.0f);
                c.r = Random.Range(0.0f, 1.0f);
                child.GetComponent<SpriteRenderer>().color = c;

            }

        }

       


    }
	void Update () {
        if(!stop)
         points += (speed/10);


        if (!stop)
		{
			Vector3 vel = new Vector3();

			vel = Vector3.down * speed * Time.deltaTime;
			transform.Translate(vel);
		}
	}

	public void SlowDown(float f){
		if (speed > f) {
			speed -= f;
		}

	}
	void StopMoving()
	{
		stop = true;
	}
}
