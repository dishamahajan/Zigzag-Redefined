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
				if (Advertisement.IsReady ("video2")) {
					Advertisement.Show ("video2");
				}
				PlayerPrefs.SetInt ("AdCount", PlayerPrefs.GetInt ("AdCount") + 1);
			}else if (PlayerPrefs.GetInt ("AdCount") == 6) {
				if (Advertisement.IsReady ("rewardedVideo")) {
					Advertisement.Show ("rewardedVideo");
				}
				PlayerPrefs.SetInt ("AdCount", PlayerPrefs.GetInt ("AdCount") + 1);
			}else if (PlayerPrefs.GetInt ("AdCount") == 10) {
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

	public void ShowRewardedVideoAd() {
			Advertisement.Show ("rewardedVedio1");
			if (PlayerPrefs.HasKey ("diamondScore1")) {
				PlayerPrefs.SetInt ("diamondScore1", PlayerPrefs.GetInt ("diamondScore1") + 20000);
			} else {
				PlayerPrefs.SetInt ("diamondScore1", 10000);
			}
	}
		
	public void ShowRewardedVideoAdContinueGame() {
		var options = new ShowOptions { resultCallback = HandleShowResult };
		Advertisement.Show("rewardedVedio1", options);
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			BallController.instance.ResumeAfterVedio ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}