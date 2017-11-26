using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Credits:
// Audio source: http://soundbible.com/330-Camera-Click.html


public class ScreenShot : MonoBehaviour {
	private AudioSource[] audioSources;
	private AudioSource clickAudio;
	public Button saveButton;

	string toastString;
	string input;
	AndroidJavaObject currentActivity;
	AndroidJavaClass UnityPlayer;
	AndroidJavaObject context;

	// Use this for initialization
	void Start () {
		Button btn = saveButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);

		// Audio
		AudioSource[] audios = GetComponents<AudioSource>();
		clickAudio = audios[0];


		// Android
		if (Application.platform == RuntimePlatform.Android)
		{
			UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
		}
	}


	public void showToastOnUiThread(string toastString)
	{
		this.toastString = toastString;
		currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
	}

	void showToast()
	{
		Debug.Log(this + ": Running on UI thread");

		AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
		AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
		AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
		toast.Call("show");
	}

	void OnClick()
	{
		// Debug.Log("You have clicked the button!");
		// Debug.LogError("You have clicked the button!");
		// Screenshot will be stored in Application.persistentDataPath
		// On MAC data is written into ~/Library/Application Support/company/product
		string fileName = "Dreamviewer";
		ScreenshotManager.SaveScreenshot (fileName, fileName);
		clickAudio.Play ();

		if (Application.platform == RuntimePlatform.Android){
			showToastOnUiThread ("  Saving image to gallery  ");
		}
		// System.DateTime.UtcNow.ToString("HH:mm:ss dd MMMM, yyyy") 
		// fileName = fileName + System.DateTime.UtcNow.ToString("-yyyy");
		// fileName = fileName + System.DateTime.UtcNow.ToString("-MMMM");
		// fileName = fileName + System.DateTime.UtcNow.ToString("-HHmmss"); 
		// fileName = fileName + ".png";
		// Debug.LogError(fileName);
		// print(Application.persistentDataPath);
		// ScreenCapture.CaptureScreenshot(fileName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
