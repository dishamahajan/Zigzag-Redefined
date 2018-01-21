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
		
	}

	public void StartGame(){
		
		UIManager.instance.GameStart ();
		ScoreManagerScript.instance.startScore ();
		GameObject.Find ("PlaformSpawner").GetComponent<PlaformSpawner> ().StartSpawningPlatform ();
	}

	public void GameOver(){
		
		UIManager.instance.GameOver ();
		ScoreManagerScript.instance.stopScore ();
		LeaderBoardManager.instance.AddScoreToLeaderBoard ();
		gameOver = true;
	}
		
}
