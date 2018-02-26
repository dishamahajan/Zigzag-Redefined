using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

	public static BallController instance;

	[SerializeField]
	private float speed;
	public AudioManager am;
	public GameObject partical;
	public GameObject ball;
	public GameObject saveMe;
	bool started;
	bool gameOver;
	public bool saveMeFlag;
	Rigidbody rb;
	public DiamondAudioController diamondAudioController;
	public Button muteButton;
	public Sprite mute; 
	public Sprite unMute;
	private bool isAndroid;

	private bool is300;
	private bool is50;
	private bool is20;
	public GameObject quitobject;
	private bool clickedBefore = false;
	Vector3 previousPorsition;
	IEnumerator quitingTimer()
	{
		//Wait for a frame so that Input.GetKeyDown is no longer true
		yield return null;

		//3 seconds timer
		const float timerTime = 3f;
		float counter = 0;

		while (counter < timerTime)
		{
			counter += Time.deltaTime;
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Quit();
			}
			yield return null;
		}

		quitobject.SetActive(false);
		clickedBefore = false;
	}

	void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		//Application.Quit();
		System.Diagnostics.Process.GetCurrentProcess().Kill();
		#endif
	}

	public void playVolume(){

		if (PlayerPrefs.HasKey ("mute")) {
			if (PlayerPrefs.GetString ("mute").Equals ("false")) {
				if (Time.timeScale == 1) {
					am.gameSound.mute = false;
				} else {
					am.gameSound.mute = true;
				}
			}
		}
	}

	public void SaveMe (){
		GameObject.Find ("PlaformSpawner").GetComponent<PlaformSpawner> ().StartSpawningPlatform ();
		saveMe.SetActive (false);
		rb.velocity = new Vector3 (3, 0, 0); 
		Camera.main.GetComponent<CameraFollow> ().gameOver = false;
		ScoreManagerScript.instance.startScore ();

	}

	public void NotSaveMe(){
		saveMe.SetActive (false);
		am.gameSound.Stop ();
		am.gameOverSound.Play ();
		gameOver = true;
		rb.velocity = new Vector3 (0, 0, 6); 
		Camera.main.GetComponent<CameraFollow> ().gameOver = true;
		GameManager.instance.GameOver ();
	//	rb.velocity = new Vector3 (0, -25f, 0);

	}

	void Awake(){

		if (instance == null) {
			instance = this;
		}
		rb = GetComponent<Rigidbody> ();
		am.gameSound.Play ();
		if (PlayerPrefs.HasKey ("mute")) {
			if (PlayerPrefs.GetString ("mute").Equals ("true")) {
				am.gameSound.mute = true;	
				am.gameOverSound.mute = true;	
				muteButton.image.sprite = mute;
				diamondAudioController.diamondAudioSource.mute = true;
			} else {
				am.gameSound.mute = false;
				am.gameOverSound.mute = false;	
				muteButton.image.sprite = unMute;
				diamondAudioController.diamondAudioSource.mute = false;
			}
		} else {
			diamondAudioController.diamondAudioSource.mute = false;
		}

	}
	// Use this for initialization
	void Start () {
		started = false;
		gameOver = false;
		saveMeFlag = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !clickedBefore)
		{
			clickedBefore = true;
			quitobject.SetActive(true);
			StartCoroutine(quitingTimer());
		}

		if (ScoreManagerScript.instance.score > 300 && !is300) {
			speed = 9;
			is300 = true;
		}else if (ScoreManagerScript.instance.score > 50 && !is50) {
			speed = 8;
			is50 = true;
		}else if (ScoreManagerScript.instance.score > 20 && !is20) {
			speed = 7;
			is20 = true;
		}

		if (!started) {
			if (Application.platform == RuntimePlatform.Android) {
				isAndroid = true;
					if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
						int pointerId = Input.GetTouch (0).fingerId;
						if (!EventSystem.current.IsPointerOverGameObject (pointerId)) {
							rb.velocity = new Vector3 (speed, 0, 0);
							started = true;

							GameManager.instance.StartGame ();
						}
				}
			} else {
				isAndroid = false;
					if (Input.GetMouseButtonDown (0)) {
						if (!EventSystem.current.IsPointerOverGameObject ()) {
							rb.velocity = new Vector3 (speed, 0, 0);
							started = true;

							GameManager.instance.StartGame ();
					}
				}
			}
		}
		 
		if (!Physics.Raycast (transform.position, Vector3.down, 1f) && !gameOver) {
			ScoreManagerScript.instance.stopScore ();
			saveMe.SetActive (true);
			saveMeFlag = true;
			transform.position = new Vector3 (PlaformSpawner.instance.lastPos.x, PlaformSpawner.instance.lastPos.y + 2, PlaformSpawner.instance.lastPos.z);
			rb.velocity = new Vector3 (0, 0, 0); 
			PlaformSpawner.instance.destroyPlatform ();
			GameObject.Find ("PlaformSpawner").GetComponent<PlaformSpawner> ().CancelSpawningPllatform ();
		}

		if(Input.GetMouseButtonDown (0) && !gameOver && Time.timeScale == 1) {
			SwitchDirection ();
		}

	}

	public void SwitchDirection(){
		if (rb.velocity.z > 0) {
			rb.velocity = new Vector3 (speed,0,0);		
		}
		else if (rb.velocity.x > 0) {
			rb.velocity = new Vector3 (0,0,speed);		
		}		 
	}

	void OnTriggerEnter(Collider col){ 
		if(col.gameObject.tag == "diamond"){
			GameObject part = Instantiate (partical , col.gameObject.transform.position, Quaternion.identity) as GameObject;
			if (PlayerPrefs.HasKey ("diamondScore1")) {
				PlayerPrefs.SetInt ("diamondScore1", PlayerPrefs.GetInt ("diamondScore1") + 1);
			} else {
				PlayerPrefs.SetInt ("diamondScore1", 1);
			}
			Destroy (col.gameObject); 
			Destroy (part, 2f);
		}
	}

	public void ChangeMute(){
		if (am.gameSound.mute) {
			am.gameSound.mute = false;	
			am.gameOverSound.mute = false;	
			muteButton.image.sprite = unMute;
			diamondAudioController.diamondAudioSource.mute = false;
			PlayerPrefs.SetString("mute","false");
		} else {
			am.gameSound.mute = true;	
			am.gameOverSound.mute = true;	
			muteButton.image.sprite = mute;
			diamondAudioController.diamondAudioSource.mute = true;
			PlayerPrefs.SetString("mute","true");
		}
	}
}
