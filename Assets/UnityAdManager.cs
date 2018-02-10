using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdManager : MonoBehaviour {

	public static UnityAdManager instance;

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		if (instance == null) {
			instance = this;
		} 
		else {
			Destroy (this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowAd() {

		if (PlayerPrefs.HasKey ("AdCount")) {

			if (PlayerPrefs.GetInt ("AdCount") == 3) {
				if (Advertisement.IsReady ("rewardedVideo")) {
					Advertisement.Show ("rewardedVideo");
				}
				PlayerPrefs.SetInt ("AdCount", PlayerPrefs.GetInt ("AdCount") + 1);
			}else if (PlayerPrefs.GetInt ("AdCount") == 6) {
				if (Advertisement.IsReady ("video")) {
					Advertisement.Show ("video");
				}
				PlayerPrefs.SetInt ("AdCount", 0);
			}else {
				PlayerPrefs.SetInt ("AdCount", PlayerPrefs.GetInt ("AdCount") + 1);
			}
		} else {
			PlayerPrefs.SetInt ("AdCount", 0);
		}	
	}
}
