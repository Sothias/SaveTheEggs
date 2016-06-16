using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
    public Text levelText;
    public GameObject eggPref;
    public GameObject eggsPref;
    public GameObject slug;
    public GameObject rabbit;
    public GameObject pillow;
    public GameObject coin;
    public GameObject lastEgg;
    public float intervallLength = 15.0F;
    public int coins = 0;
    int coinsInLevel = 0;
    float currentSpawnDelay = 1.0F;
    float width;
    float height;
    public int levelCounter = 0;
    public bool speedUp = false;
    public bool speedDown = false;
   
    public bool nextEggs = true;
    public bool startMassSpawn = false;
    public bool nextSlug = true;
    public bool nextRabbit = true;
    public bool nextPillow = true;
     bool fenceActive = false;
   public bool lastStartEgg = false;
    bool start = false;
    int countStartEggs= 0;
    Color32 eggColor;
	// Use this for initialization
	void Start () {
        width = Screen.width;
        height = Screen.height;
   //     StartSpawnInterval();
        
            coins = PlayerPrefs.GetInt("Coins");
     
	}
	void Spawn()
    {
        levelText.text = "";
        SpawnEgg();
         // Spawn Egg
        if (Random.Range(1,100) <= 25 && nextEggs && levelCounter >= 1)
        {
            nextEggs = false;
            Instantiate(eggsPref, PrefPosition(), Quaternion.identity);//Spawn Eggs
           

        }
        if(Random.Range(1,100) <= 2 && coinsInLevel <= 5)
        {
            coinsInLevel++;
            Instantiate(coin, PrefPosition(), Quaternion.identity);
        }
        if(Random.Range(1,100) <= 50 && levelCounter >= 17 && !fenceActive)
        {
            Invoke("StartFence", 5);
            fenceActive = false;
            Invoke("NextFence", Random.Range(20, 50));
        }
        if(Random.Range(1,100) <= 20 && nextSlug && levelCounter >= 9 && (!speedDown && !speedUp))
        {
            nextSlug = false;
            nextRabbit = false;
            Instantiate(slug, PrefPosition(), Quaternion.identity);
            
        }
        if(Random.Range(1,100) <= 20 && nextRabbit && levelCounter >= 11 && (!speedDown && !speedUp))
        {
            nextRabbit = false;
            nextSlug = false;
            Instantiate(rabbit, PrefPosition(), Quaternion.identity);
           
        }
        if(Random.Range(1,100) <= 15 && nextPillow && levelCounter >= 15)
        {
            nextPillow = false;
            Instantiate(pillow, PrefPosition(), Quaternion.identity);
         
        }
    }
    
    Vector3 PrefPosition()
    {
        Vector3 pos = new Vector3();
        pos.x = Random.Range(50, width - 50);
        pos.y = height + 70;
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        wp.z = 0;
        return wp; 
    }
    void SpawnEgg()
    {
         
     


        if (countStartEggs != gameObject.GetComponent<GUIController>().startEggs-1 && gameObject.GetComponent<GUIController>().startEggs != 0)
        {
            GameObject.Find("GameController").GetComponent<GUIController>().starting = true;
            countStartEggs++;
            GameObject.Find("Level").GetComponent<Text>().text = countStartEggs.ToString();
            Instantiate(eggPref, PrefPosition(), Quaternion.identity);
        }
        else if(!lastStartEgg && gameObject.GetComponent<GUIController>().startEggs != 0)
        {
            CancelInvoke("SpawnEgg");
            GameObject.Find("Level").GetComponent<Text>().text = (countStartEggs + 1).ToString();
            Instantiate(lastEgg, PrefPosition(), Quaternion.identity);
            levelText.text = "Let's go";

        }
        else
        {
            Instantiate(eggPref, PrefPosition(), Quaternion.identity);
        }
        
    }
     
    void NextFence()
    {
        fenceActive = false;
    }
    public void SpawnStartEggs(int eggs)
    {
         

        if (eggs > 0)
        {
            InvokeRepeating("SpawnEgg", 1, 0.09f);

            levelText.text = "Eggs coming";

        }
        else
        {
            Invoke("StartSpawnInterval",1);
            
        }
       
       
    }
    public void StartSpawnInterval()
    {
        coinsInLevel = 0;
        levelCounter++;
        speedUp = false;
        speedDown = false;
        levelText.text = "Level " + levelCounter.ToString();
        GameObject.Find("GameController").GetComponent<Achievments>().CheckLevel(levelCounter);
        Social.ReportScore((int)(Mathf.Round(levelCounter)), GPS.leaderboard_level, (bool success) =>

        {
            if (success)
            {
                Debug.Log("ok");

            }
            else {
                Debug.Log("error");
            }

        }
             );
        GameObject.Find("Level").GetComponent<Text>().text = "Level: " + levelCounter.ToString();
        InvokeRepeating("Spawn", 3, currentSpawnDelay);
        intervallLength  =  10* Mathf.Pow(levelCounter, 0.33F);
        Invoke("IntervalBreak", intervallLength );
       

    }
    void StartFence()
    {
        GameObject.Find("Fence").GetComponent<FenceMoving>().StartMoving();
    }
    void IntervalBreak()
    {
        CancelInvoke("Spawn");
        Invoke("StartSpawnInterval", 3);
        if (currentSpawnDelay > 0.2F)
            currentSpawnDelay -= 0.05F;


    }
    void StopSpawning()
    {
        CancelInvoke();
    }
    void MassSpawnEnemy()
    {
        if (startMassSpawn)
        {
            int countMax = (int)Random.Range(2, 6);
            for (int counter = 0; counter <= countMax; counter++)
            {
                Instantiate(eggPref, PrefPosition(), Quaternion.identity);
 
            }
         
            //    InvokeRepeating("Spawn", 0, 0.1F);
            startMassSpawn = false;
            Invoke("NextEggs", 10);
        }
    }
    void Update()
    {
        GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + coins.ToString();
        
        
        MassSpawnEnemy();
    }
   public void SetSpeed()
    {

        Invoke("DefaultTimeScale", 5);

    }
    void NextEggs()
    {
        nextEggs = true;
    }
    void DefaultTimeScale()
    {
       
        if (speedUp)
        {
           speedUp = false;
            
        }
         
        if (speedDown)
            speedDown = false;
        
    }
    public void PayCoins(int c)
    {

        switch (GameObject.Find("GameController").GetComponent<GUIController>().startEggs)
        {
            case 50:
                coins -= 40;
                break;
            case 100:
                coins -= 50;
                break;
            case 200:
                coins -= 100;
                break;
            case 400:
                coins -= 200;
                break;


        }
         
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }


}
