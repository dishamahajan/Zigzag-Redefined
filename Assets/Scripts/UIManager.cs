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
	public Text DiamondText;
	public Text score;
	public Text highScore1;
	public Text highScore2;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		highScore1.text = "High Score: "+ PlayerPrefs.GetInt ("highScore").ToString();
		DiamondText.text = "x "+ PlayerPrefs.GetInt ("diamondScore1").ToString();
	}

	public void GameStart(){
		
		//tapText.SetActive (false);
		tapText.GetComponent<Animator> ().Play ("textDown");
		zigZagPanel.GetComponent<Animator> ().Play ("PanelUp");
		diamondPanel.SetActive(false);
		leaderBoardButton.SetActive (false); 
		//Diamond.SetActive (false);
		//DiamondText (false);
		MuteButton.SetActive (false);
		ShareButton.SetActive (false);
	}

	public void GameOver(){
		score.text = PlayerPrefs.GetInt ("score").ToString();
		highScore2.text = PlayerPrefs.GetInt ("highScore").ToString();
		gameOverPanel.SetActive (true);
	}

	public void Reset(){
		SceneManager.LoadScene (0);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ShowLeaderBoard(){
		LeaderBoardManager.instance.ShowLeaderBoard ();
	}

}
