using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject zigZagPanel;
	public GameObject diamondPanel;
	public GameObject gameOverPanel;
	public GameObject tapText;
	public GameObject leaderBoardButton;
	public GameObject MuteButton;
	public GameObject ShareButton;
	public Button pauseButton;
	public Text DiamondText;
	public Text score;
	public Text highScore1;
	public Text highScore2;
	public Text scoreText; 

	public Sprite pauseImage;
	public Sprite playImage;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		pauseButton.gameObject.SetActive (false);
		scoreText.enabled = false;
		highScore1.text = "High Score: "+ PlayerPrefs.GetInt ("highScore").ToString();
		DiamondText.text = "x "+ PlayerPrefs.GetInt ("diamondScore1").ToString();
	}

	public void GameStart(){
		
		tapText.GetComponent<Animator> ().Play ("textDown");
		zigZagPanel.GetComponent<Animator> ().Play ("PanelUp");
		diamondPanel.SetActive(false);
		leaderBoardButton.SetActive (false); 
		scoreText.enabled = true;
		pauseButton.gameObject.SetActive (true);
		MuteButton.SetActive (false);
		ShareButton.SetActive (false);
	}

	public void GameOver(){
		score.text = PlayerPrefs.GetInt ("score").ToString();
		highScore2.text = PlayerPrefs.GetInt ("highScore").ToString();
		gameOverPanel.SetActive (true);
		scoreText.enabled = false;
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
		scoreText.text = ScoreManagerScript.instance.score.ToString();
	}

	public void ShowLeaderBoard(){
		LeaderBoardManager.instance.ShowLeaderBoard ();
	}
		
	public void pause(){
		if (Time.timeScale == 1) {
			pauseButton.image.sprite = playImage;
			Time.timeScale = 0;
		} else {
			pauseButton.image.sprite = pauseImage;
			Time.timeScale = 1;
		}
	}

}
