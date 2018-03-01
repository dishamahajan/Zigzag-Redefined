using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

	public static AchievementManager instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Login(){
		Social.localUser.Authenticate ((bool success)=>{});
	}

	public void ShowAchievements(){
		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI ();
		} else {
			Login ();
		}
	}
	public void CheckAchievements(){
		if (ScoreManagerScript.instance.score > 30) {
			Social.ReportProgress (Achievements.achievement_beginner, 100f, (bool success) => {
			});
		}
		if (ScoreManagerScript.instance.score > 100) {
			Social.ReportProgress (Achievements.achievement_intermediate, 100f, (bool success) => {
			});
		}
		if (ScoreManagerScript.instance.score > 300) {
			Social.ReportProgress (Achievements.achievement_awesome, 100f, (bool success) => {
			});
		}
		if (ScoreManagerScript.instance.score > 500) {
			Social.ReportProgress (Achievements.achievement_pro, 100f, (bool success) => {
			});
		}
		if (ScoreManagerScript.instance.score > 1000) {
			Social.ReportProgress (Achievements.achievement_expert, 100f, (bool success) => {
			});
		}
		if (PlayerPrefs.GetInt("Round") > 1000) {
			Social.ReportProgress (Achievements.achievement_1000_rounds, 100f, (bool success) => {
			});
		}
	}
}
