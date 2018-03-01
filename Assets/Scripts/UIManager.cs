using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject zigZagPanel;
	public GameObject rewardPanel;
	//public Button rewardVideo;
	public GameObject diamondPanel;
	public GameObject gameOverPanel;
	public GameObject tapText;
	public GameObject leaderBoardButton;
	public GameObject AchievementButton;
	public GameObject MuteButton;
	public GameObject ShareButton;
	public Button pauseButton;
	public Button scoreButton;
	public Text DiamondText;
	public Text score;
	public Text highScore1;
	public Text rounds;
	public Text highScore2;

	public Sprite pauseImage;
	public Sprite playImage;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		if (Advertisement.IsReady ("rewardedVedio1")) {
			rewardPanel.SetActive (true);
		} else {
			rewardPanel.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
		pauseButton.gameObject.SetActive (false);
		scoreButton.gameObject.SetActive (false);
		highScore1.text = "Best Score: "+ PlayerPrefs.GetInt ("highScore").ToString();
		rounds.text = "Games Played: "+ PlayerPrefs.GetInt ("Round").ToString();
		DiamondText.text = "x "+ PlayerPrefs.GetInt ("diamondScore1").ToString();
	}

	public void GameStart(){
		
		//tapText.GetComponent<Animator> ().Play ("textDown");
		tapText.SetActive(false);
		zigZagPanel.GetComponent<Animator> ().Play ("PanelUp");
		diamondPanel.SetActive(false);
		leaderBoardButton.SetActive (false);
		AchievementButton.SetActive (false);
		scoreButton.gameObject.SetActive (true);
		pauseButton.gameObject.SetActive (true);
		MuteButton.SetActive (false);
		ShareButton.SetActive (false);
		//rewardVideo.gameObject.SetActive (false);
		rewardPanel.SetActive (false);
	}

	public void GameOver(){
		score.text = PlayerPrefs.GetInt ("score").ToString();
		highScore2.text = PlayerPrefs.GetInt ("highScore").ToString(); 	
		gameOverPanel.SetActive (true);
		scoreButton.gameObject.SetActive (false);
		pauseButton.gameObject.SetActive (false);
		if (Time.timeScale == 0) {
			pauseButton.image.sprite = pauseImage;
			Time.timeScale = 1;
		} 
	}

	public void Reset(){
		SceneManager.LoadScene (0);
	}

	// Update is called once per frame
	void Update () {
		
		scoreButton.GetComponentInChildren<Text>().text = ScoreManagerScript.instance.score.ToString();

	}

	public void ShowLeaderBoard(){
		LeaderBoardManager.instance.ShowLeaderBoard ();
	}

	public void ShowAchievements(){
		AchievementManager.instance.ShowAchievements();
	}

	public void pause(){
		if (Time.timeScale == 1) {
			
			pauseButton.image.sprite = playImage;
			Time.timeScale = 0;
			BallController.instance.SwitchDirection ();
		} else {
			pauseButton.image.sprite = pauseImage;
			Time.timeScale = 1;
		}
	}

}
