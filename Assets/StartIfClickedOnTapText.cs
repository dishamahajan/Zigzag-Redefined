using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIfClickedOnTapText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		GameManager.instance.StartGame ();
	}
}
