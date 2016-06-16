using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class Achievments : MonoBehaviour {
    public GameObject gameController;

 


    int rabbitCount = 0;
    int slugCount = 0;
    int pillowCount = 0;
    int coloredEggsCount = 0;
    // Use this for initialization

    void Start () {
        rabbitCount = 0;
         
	}
	
	// Update is called once per frame
    public void CheckLevel(int level)

    {
        switch (level)
        {
            case 10:
                Report(GPS.achievement_level_10);
                break;
            case 20:
                Report(GPS.achievement_level_20);
                break;
            case 30:
                Report(GPS.achievement_level_30);
                break;
            case 50:
                Report(GPS.achievement_level_50);
                break;

        }
       
         
    }
    public void CheckRabbits()
    {
        rabbitCount++;
        IncrementAchievments(GPS.achievement_1000_rabbits_every_time);
        IncrementAchievments(GPS.achievement_100_rabbits_every_time);
        if (rabbitCount == 1)
            Report(GPS.achievement_use_a_rabbit);
        if (rabbitCount == 5)
            Report(GPS.achievement_5_rabbits);
        if (rabbitCount == 15)
            Report(GPS.achievement_15_rabbits);
    }
    public void CheckSlugs()
    {
        slugCount++;
        IncrementAchievments(GPS.achievement_1000_slugs_every_time);
        IncrementAchievments(GPS.achievement_100_slugs_every_time);
        if (slugCount == 1)
            Report(GPS.achievement_use_a_slug);
        if (slugCount == 5)
            Report(GPS.achievement_5_slugs);
        if (slugCount == 15)
            Report(GPS.achievement_15_slugs);
    }
    public void CheckPillows()
    {
        pillowCount++;
        IncrementAchievments(GPS.achievement_100_pillows_every_time);
        IncrementAchievments(GPS.achievement_1000_pillows_every_time);
        if (pillowCount == 1)
            Report(GPS.achievement_use_a_pillow);
        if (pillowCount == 5)
            Report(GPS.achievement_5_pillows);
        if (pillowCount == 15)
            Report(GPS.achievement_15_pillows);
    }
    public void CheckColoredEggs()
    {
        coloredEggsCount++;
        IncrementAchievments(GPS.achievement_1000_colored_eggs_every_time);
        IncrementAchievments(GPS.achievement_100_colored_eggs_every_time);
        if (coloredEggsCount == 1)
            Report(GPS.achievement_use_colored_eggs);
        if (coloredEggsCount == 5)
            Report(GPS.achievement_5_colored_eggs);
        if (coloredEggsCount == 15)
            Report(GPS.achievement_15_colored_eggs);

    }
    
     
    

      public  void CheckPoints(int points)

    {

        if (points >= 100)
        {
            Debug.Log("100 Points");
            Report(GPS.achievement_100_points);
        }
        if (points >= 1000f)
        {
            Report(GPS.achievement_1000_points);
        }
        if (points >= 2000)
        {

            Report(GPS.achievement_2000_points);
        }

        if (points >= 5000)
        {
            Report(GPS.achievement_5000_points);
        }
        if (points >= 7500)
        {
            Report(GPS.achievement_7500_points);
        }
        if (points >= 10000)
        {
            Report(GPS.achievement_10000_points);
        }
        if (points >= 30000)
            Report(GPS.achievement_30000_points);
        if (points >= 60000)
            Report(GPS.achievement_60000_points);
        if (points >= 120000)
            Report(GPS.achievement_120000_points);

    
      
    
       
        



    }
    public void EggsAllTime(string a)
    {
        IncrementAchievments(a);
    }
    void IncrementAchievments(string a )
    {
        PlayGamesPlatform.Instance.IncrementAchievement(a, 1, (bool success) => {
            if (success)
            {
                Debug.Log("Ok");
            }
            else
            {
                int tttt = 1;
            }
        });
    }
    void Report(string a)
    {
        Social.ReportProgress(a, 100.0f, (bool success) => {
            if (success)
            {
                Debug.Log("Ok");
            }
            
        });

    }

	void Update () {
	
	}
}
