using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
public class NativeShareScript : MonoBehaviour {
	public string subject,ShareMessage,url,body;
	private bool isProcessing = false;
	public string ScreenshotName = "screenshot.png";
	 
	public void ShareHignScore()
	{		
		subject = "WORD-O-MAZE";
		body = "My score is " +PlayerPrefs.GetInt ("highScore").ToString() + ". Can you beat my score?"  + "https://play.google.com/store/apps/details?id=androidflashlightapp.inducesmile.com.myapp1";
/*		#if UNITY_ANDROID
		if(!isProcessing)
			StartCoroutine( ShareScreenshot() );
		#endif*/
		#if UNITY_ANDROID
		//Refernece of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		//Refernece of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//start the activity by sending the intent data
		currentActivity.Call ("startActivity", intentObject);
		#endif
	}

	public void ShareCurrentScore()
	{		
		subject = "WORD-O-MAZE";
		body = "My score is " +PlayerPrefs.GetInt ("score").ToString() + ". Can you beat my score?"  + "https://play.google.com/store/apps/details?id=androidflashlightapp.inducesmile.com.myapp1";
		/*		#if UNITY_ANDROID
		if(!isProcessing)
			StartCoroutine( ShareScreenshot() );
		#endif*/
		#if UNITY_ANDROID
		//Refernece of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		//Refernece of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//start the activity by sending the intent data
		currentActivity.Call ("startActivity", intentObject);
		#endif
	}


	public IEnumerator ShareScreenshot()
	{
	#if UNITY_ANDROID
	url = "https://play.google.com/store/apps/details?id=androidflashlightapp.inducesmile.com.myapp1";
	ShareMessage = "Can you beat my score?" + url;

	isProcessing = true;
	// wait for graphics to render
	yield return new WaitForEndOfFrame();
	string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
	ScreenCapture.CaptureScreenshot(ScreenshotName);
	yield return new WaitForSeconds(1f);
	if(!Application.isEditor)
	{
	AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
	AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
	intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
	AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
	AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);
	intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
	intentObject.Call<AndroidJavaObject>("setType", "image/png");
	intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ShareMessage);
	AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
	AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share");
	currentActivity.Call("startActivity", jChooser);
	}
	isProcessing = false;
	#endif
	}

	
	public void RateUs()
	{
		#if UNITY_ANDROID
		Application.OpenURL("market://details?id=androidflashlightapp.inducesmile.com.myapp1");
		#endif
	}

	public void PlayRewardedVideo(){
		UnityAdManager.instance.ShowRewardedVideoAd();
	}
}