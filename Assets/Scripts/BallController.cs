using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	[SerializeField]
	private float speed;
	public AudioManager am;
	public GameObject partical;
	bool started;
	bool gameOver;
	Rigidbody rb;
	public DiamondAudioController diamondAudioController;
	public Button muteButton;
	public Sprite mute;
	public Sprite unMute;
	private bool isAndroid;

	void Awake(){
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

			//ChangeMute ();
		} else {
			diamondAudioController.diamondAudioSource.mute = false;
		}

	}
	// Use this for initialization
	void Start () {
		started = false;
		gameOver = false;
	}

	// Update is called once per frame
	void Update () {

		if (Application.platform == RuntimePlatform.Android) {
			isAndroid = true;
			if (!started) {
				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
					int pointerId = Input.GetTouch (0).fingerId;
					if (!EventSystem.current.IsPointerOverGameObject (pointerId)) {
						rb.velocity = new Vector3 (speed, 0, 0);
						started = true;

						GameManager.instance.StartGame ();
					}
				}
			}
		} else {
			isAndroid = false;
			if (!started) {
				if (Input.GetMouseButtonDown (0)) {
					if (!EventSystem.current.IsPointerOverGameObject ()) {
						rb.velocity = new Vector3 (speed, 0, 0);
						started = true;

						GameManager.instance.StartGame ();
					}
				}
			}
		}
		 
		if (!Physics.Raycast (transform.position, Vector3.down, 1f)) {
			am.gameSound.Stop ();
			am.gameOverSound.Play ();
			gameOver = true;
			rb.velocity = new Vector3 (0, -25f, 0);

			Camera.main.GetComponent<CameraFollow> ().gameOver = true;
			GameManager.instance.GameOver ();
		}


		if (isAndroid && Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			int pointerId = Input.GetTouch (0).fingerId;
			if (!EventSystem.current.IsPointerOverGameObject (pointerId) && !gameOver) {
				SwitchDirection ();
			}
		}else if(Input.GetMouseButtonDown (0) && !gameOver) {
			SwitchDirection ();
		}

	}

	void SwitchDirection(){
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
