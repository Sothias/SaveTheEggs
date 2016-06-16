using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sound : MonoBehaviour {
	public bool isMute;
	// Use this for initialization
	void Start () {
		PlayerPrefs.GetFloat ("music");
		if (PlayerPrefs.GetFloat ("music") == 0) {
			GameObject sound = GameObject.Find ("Sound");
			sound.GetComponent<Toggle> ().isOn = false;
		}
	}

	public void MuteToggle(){
	
		isMute = !isMute;
		if (isMute) {
			AudioListener.volume = 0;
			PlayerPrefs.SetFloat ("music", 0);
		} else {

			AudioListener.volume = 1;
			PlayerPrefs.SetFloat ("music", 1); 
		
		}
	


	}

}
