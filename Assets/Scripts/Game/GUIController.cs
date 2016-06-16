using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Unity5
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GUIController : MonoBehaviour {

	public GameObject uiPanel;
	//Unity5
	public Text text1;
	public Text text2;
    public GameObject pillow;
	/*
	public Texture2D gameOverPicture;
	public Texture2D bgPicture;
	*/
	//Unity5
	public AudioClip highscoreClip;
	public AudioClip gameOverClip;
    public int startEggs = 0;
    
	[HideInInspector]
	public float points = 0;
    //bool showHs = false;
    //Unity5
    //Rect pointsRect;
    //Rect endRect;
    //Rect scoreRect;
    //Unity5
    public bool starting = false;

    int beginningHs;
    int countScore = 0;
	//string scoreText1;
	//string scoreText2;

	void Start()
	{

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        /*
		pointsRect = new Rect(10,10,100,40);
		endRect = new Rect(Screen.width /2 - 100, Screen.height/2 - 75, 200, 250);
		scoreRect = new Rect(Screen.width /2 - 95, Screen.height/2 , 200, 150);
*/
        GameObject.FindGameObjectWithTag("100Eggs").GetComponent<Toggle>().interactable = false;
        GameObject.FindGameObjectWithTag("500Eggs").GetComponent<Toggle>().interactable = false;
        GameObject.FindGameObjectWithTag("1000Eggs").GetComponent<Toggle>().interactable = false;
        GameObject.FindGameObjectWithTag("2000Eggs").GetComponent<Toggle>().interactable = false;
        int coins = PlayerPrefs.GetInt("Coins");
        GameObject.Find("StartCoins").GetComponent<Text>().text = "Coins: " + coins.ToString();
        if(coins >= 40)
        {
            GameObject.FindGameObjectWithTag("100Eggs").GetComponent<Toggle>().interactable = true;
        }
        if(coins >= 50)
            GameObject.FindGameObjectWithTag("500Eggs").GetComponent<Toggle>().interactable = true;
        if(coins >= 100)
            GameObject.FindGameObjectWithTag("1000Eggs").GetComponent<Toggle>().interactable = true;
        if(coins >= 200)
            GameObject.FindGameObjectWithTag("2000Eggs").GetComponent<Toggle>().interactable = true;

        GooglePlayGames.PlayGamesPlatform.Activate();
       
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {

                if (success)
                {

                    string token = GooglePlayGames.PlayGamesPlatform.Instance.GetToken();
                    Debug.Log(token);
                }
                else {

                }
            });
        }
        beginningHs = 0;
        uiPanel.SetActive(false);
        if (PlayerPrefs.HasKey("Highscore"))
        {
            beginningHs = PlayerPrefs.GetInt("Highscore");
        }

    }
   public void BackToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void StartGame()
    {
        GameObject.Find("Start").SetActive(false);
        if (startEggs != 0)
        {
            pillow.SetActive(true);
            gameObject.GetComponent<Spawner>().PayCoins(startEggs);
        }
       
       gameObject.GetComponent<Spawner>().SpawnStartEggs(startEggs);
    }
    public void SetOptions(int eggs)
    {
        startEggs = eggs;
       
    }
    void ShowHighscore()
    {
        uiPanel.SetActive(true);
        points = Mathf.Round(points);
        PlayerPrefs.SetInt("Coins", GameObject.Find("GameController").GetComponent<Spawner>().coins);
        if(points >0)
            InvokeRepeating("CountScore", 0.5F, 0.0001F);
        //gameObject.GetComponent<Achievments>().CheckPoints((int)points);
        
        if (points > beginningHs)
        {
             
            PlayerPrefs.Save();
            //AudioSource.PlayClipAtPoint(highscoreClip,transform.position);

       //     text1.text = "New Highscore!";
            text2.text = "New Highscore";
            
            
            PlayerPrefs.SetInt("Highscore", (int)points);
            PlayerPrefs.Save();
            if (Social.localUser.authenticated)
            {
                Social.ReportScore((int)(Mathf.Round(points)), GPS.leaderboard_points, (bool success) =>

                {
                    if (success)
                    {
                        text1.text = "Update Score Success";

                    }
                    else {
                        text1.text = "Update Score Fail";
                    }

                }
                );


            }
        }

        else {


            //AudioSource.PlayClipAtPoint(gameOverClip,transform.position);

            text2.text = "Highscore: " +beginningHs.ToString()  ;
            text2.fontSize = 77;


               
                
            
        }


        PlayerPrefs.Save();
      
    }
    void CountScore()
    {
        countScore++;

        text1.text =  countScore.ToString();

        if (countScore == points)
            CancelInvoke();
    }
   
void Update()
    {

        GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + GameObject.Find("GameController").GetComponent<Spawner>().coins.ToString();
        GameObject.Find("Score").GetComponent<Text>().text = "Score: " + Mathf.Round(points).ToString();
    }

	/*
	void OnGUI()
	{	
	
		if (showHs)
		{
			GUILayout.BeginArea(endRect);
			GUILayout.Label(gameOverPicture);	
			GUILayout.Label(bgPicture);		

			GUILayout.BeginHorizontal();
			if (GUILayout.Button ("Menü"))
				Application.LoadLevel(0);
						
			if (GUILayout.Button ("Retry"))							
				Application.LoadLevel(1);
			GUILayout.EndHorizontal ();

			GUILayout.EndArea ();

			GUILayout.BeginArea(scoreRect);
			GUILayout.Label(scoreText1);
			GUILayout.Label(scoreText2);
			GUILayout.EndArea ();
		}
	}
*/
	public void LoadLevel(int index)
	{
		SceneManager.LoadScene(index);
	}
}
