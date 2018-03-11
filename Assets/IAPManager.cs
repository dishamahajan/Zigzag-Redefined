using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : MonoBehaviour {

	public GameObject iapPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void shop(){
		iapPanel.SetActive (true);
	}
		

	public void diamond1k(){
		Purchaser.instance.Buy1000diamond ();
	}


	public void diamond3k(){
		Purchaser.instance.Buy3000diamond ();
	}

	public void noads(){
		Purchaser.instance.BuyNoAds ();
	}


	public void close(){
		iapPanel.SetActive (false);
	}
}
