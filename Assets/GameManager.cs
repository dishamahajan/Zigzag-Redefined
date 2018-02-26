using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool gameOver;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			AchievementManager.instance.CheckAchievements ();
		}
	}

	public void StartGame(){
		
		UIManager.instance.GameStart ();
		ScoreManagerScript.instance.startScore ();
		GameObject.Find ("PlaformSpawner").GetComponent<PlaformSpawner> ().StartSpawningPlatform ();
	}

	public void GameOver(){
		gameOver = true;
		UIManager.instance.GameOver ();
		LeaderBoardManager.instance.AddScoreToLeaderBoard ();
		UnityAdManager.instance.ShowAd ();
	}
		
}
