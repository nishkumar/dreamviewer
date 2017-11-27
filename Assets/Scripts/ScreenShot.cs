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
	public Button shareButton;

	string toastString;
	string input;
	string bundleId;
	AndroidJavaObject currentActivity;
	AndroidJavaClass UnityPlayer;
	AndroidJavaObject context;
	AndroidJavaObject packageManager;
	AndroidJavaObject launchIntent;


	// Use this for initialization
	void Start () {
		Button btn = saveButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);

		if(shareButton)
			shareButton.onClick.AddListener(OnClickShare);
		// Audio
		AudioSource[] audios = GetComponents<AudioSource>();
		clickAudio = audios[0];

		// Android
		launchIntent = null;
		bundleId = "com.android.gallery3d";
		bundleId = " android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI";
		if (Application.platform == RuntimePlatform.Android)
		{
			UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
			AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
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


	public void OpenAndroidGallery()
	{
		#region [ Intent intent = new Intent(); ]
		//instantiate the class Intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		//instantiate the object Intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		#endregion [ Intent intent = new Intent(); ]
		#region [ intent.setAction(Intent.ACTION_VIEW); ]
		//call setAction setting ACTION_SEND as parameter
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_VIEW"));
		#endregion [ intent.setAction(Intent.ACTION_VIEW); ]
		#region [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//instantiate the class Uri
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		//instantiate the object Uri with the parse of the url's file
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/internal/images/media");
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		#endregion [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//set the type of file
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
		#region [ startActivity(intent); ]
		//instantiate the class UnityPlayer
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//instantiate the object currentActivity
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//call the activity with our Intent
		currentActivity.Call("startActivity", intentObject);
		#endregion [ startActivity(intent); ]
	}



	void OnClick()
	{
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



	void OnClickShare()
	{
		if (Application.platform == RuntimePlatform.Android){
			showToastOnUiThread ("  Opening gallery  ");

			// Open Gallery
			OpenAndroidGallery();

		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
