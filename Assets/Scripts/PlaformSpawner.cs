using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformSpawner : MonoBehaviour {

	public static PlaformSpawner instance;
	public GameObject platform;
	public GameObject diamond;
	public GameObject[] respawns;
	public GameObject[] respawnsDiamond;
	public bool flag = false;
	public Vector3 lastPos;
	float size;
	Color newColor;
	public bool gameOver;
	int platformCount = 0;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
			
		lastPos = platform.transform.position;
		size = platform.transform.localScale.x;
		for (int i = 0; i < 20; i++){
			SpawnPlatforms ();
		}

	}
	public void StartSpawningPlatform(){
		InvokeRepeating ("SpawnPlatforms", 0.1f, 0.2f); 
	}
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.gameOver == true) {
			CancelSpawningPllatform ();
		}
	}

	public void CancelSpawningPllatform(){
		CancelInvoke("SpawnPlatforms");
	}

	void SpawnPlatforms (){
		
		int random = Random.Range (0, 6);
		if(platformCount++ != 0 && platformCount % 110 == 0) {
			flag = true;
			newColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		}
		if (random < 3) {
			SpawnX ();
		} else if(random >= 3){
			SpawnZ ();
		}
		if (flag) {
			respawns = GameObject.FindGameObjectsWithTag("Platform");
			foreach (GameObject respawn in respawns)
			{
				respawn.GetComponent<MeshRenderer> ().material.color = newColor;
			}
		}
	}
	public void destroyPlatform(){
		respawns = GameObject.FindGameObjectsWithTag("Platform");
		foreach (GameObject respawn in respawns)
		{
			if (respawn != null) {					
				if (respawn.transform.position.x < lastPos.x || respawn.transform.position.z < lastPos.z) {
					Destroy (respawn, 0f);
				}
			}

		}
		respawnsDiamond = GameObject.FindGameObjectsWithTag("diamond");
		foreach (GameObject respawn in respawnsDiamond)
		{
			if (respawn != null) {					
				if (respawn.transform.position.x < lastPos.x || respawn.transform.position.z < lastPos.z) {
					Destroy (respawn, 0f);
				}
			}

		}

		SpawnX ();
		SpawnX ();
		SpawnX ();
		SpawnX ();
		SpawnX ();
		SpawnZ ();
		SpawnZ ();
		for (int i = 0; i < 15; i++){
			SpawnPlatforms ();
		}
	}
	void SpawnX(){
		Vector3 pos = lastPos;
		pos.x += size;
		lastPos = pos;

		Instantiate (platform, pos, Quaternion.identity);
		
		int random = Random.Range (0, 4);
		if (random < 1) {
			Instantiate (diamond, new Vector3(pos.x, pos.y + 2f, pos.z), diamond.transform.rotation);
		}
	}

	void SpawnZ(){
		Vector3 pos = lastPos;
		pos.z += size;
		lastPos = pos;
		Instantiate (platform, pos, Quaternion.identity);

		int random = Random.Range (0, 4);
		if (random < 1) {
			Instantiate (diamond, new Vector3(pos.x, pos.y + 2f, pos.z), diamond.transform.rotation);
		}

	}
}
