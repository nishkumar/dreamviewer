using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	Button menuButton;
	public GameObject menuPanel;
	bool state;

	// Use this for initialization
	void Start () {
		state = false;
		menuButton = GetComponent<Button>();
		menuButton.onClick.AddListener(MenuOnClick);
		menuPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MenuOnClick()
	{
		if (state == false) {
			menuPanel.SetActive (true);
			state = true;
			Debug.Log("Open Menu options!");
		} else {
			menuPanel.SetActive (false);
			state = false;
			Debug.Log("Close menu options");
		}

	}
}
