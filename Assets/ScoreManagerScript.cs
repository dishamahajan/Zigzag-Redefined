﻿	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour {

	public static ScoreManagerScript instance;
	public int score;
	public int diamondScore;
	public BallController ballController;
	void Awake(){
		if (instance == null) {
			instance = this;
		}
		diamondScore = 0;
	}

	// Use this for initialization
	void Start () {
		score = 0;
		PlayerPrefs.SetInt ("score", score);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void incrementScore(){
		score += 1;
	}

	public void startScore(){
		InvokeRepeating ("incrementScore", 0.1f, 0.5f);
	}

	public void stopScore(){
		CancelInvoke ("incrementScore");
		PlayerPrefs.SetInt ("score", score);

		if (PlayerPrefs.HasKey ("highScore")) {
			if (score > PlayerPrefs.GetInt ("highScore")) {
				PlayerPrefs.SetInt ("highScore", score);
			}
		} else {
			PlayerPrefs.SetInt ("highScore", score);
		}
		/*int diamondCount = PlayerPrefs.GetInt ("diamondScore");
		if (PlayerPrefs.HasKey ("diamondScore")) {
			PlayerPrefs.SetInt ("diamondScore", diamondScore + diamondCount );
		} else {
			PlayerPrefs.SetInt ("diamondScore", diamondScore);
		}
		*/

	}

}
