using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

	public Button saveButton;

	// Use this for initialization
	void Start () {
		Button btn = saveButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}




	void OnClick()
	{
		// Debug.Log("You have clicked the button!");
		// Debug.LogError("You have clicked the button!");
		// Screenshot will be stored in Application.persistentDataPath
		// On MAC data is written into ~/Library/Application Support/company/product
		string fileName = "Dreamviewer";
		ScreenshotManager.SaveScreenshot(fileName, fileName);
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
