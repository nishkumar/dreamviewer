using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageWall : MonoBehaviour {
	public GameObject painting1;
	public GameObject painting2;

	public int numObjects;
	public int index;
	public GameObject[] objectArray;
	public GameObject selectedObject;

	public Button addButton;

	// Use this for initialization
	void Start () {
		// Initialize object array
		objectArray = new GameObject[numObjects];
		// No object 
		objectArray [0] = null;

		if (painting1)
			objectArray [1] = painting1;

		if (painting2)
			objectArray [2] = painting2;

		//////////////////////////////////////////////////////////////////////
		index = 0;

		if(addButton)
			addButton.onClick.AddListener(OnClick);
	}


	void HideAll(){



		if (painting1) {
			painting1.GetComponent<Renderer> ().enabled = false;

			foreach (Transform child in painting1.transform) {
				child.GetComponent<Renderer> ().enabled = false;
			}
		}

		if (painting2) {
			foreach (Transform child in painting2.transform) {
				child.GetComponent<Renderer> ().enabled = false;
			}
		}
			
		/*
		for (int i = 0; i < numObjects; i++) {
			if (objectArray [i] != null) {
				objectArray [i].GetComponent<Renderer> ().enabled = false;
			}
		}
		*/
	}

	void OnClick()
	{
		if (index >= numObjects - 1) {
			// bed.GetComponent<Renderer> ().enabled = false;
			index = 0;
		} else {
			index++;
		}
	}


	// Update is called once per frame
	void Update () {
		// Hide all by defualt 
		HideAll();

		if (index > 0) {
			selectedObject = objectArray [index];
			selectedObject.GetComponent<Renderer> ().enabled = true;

			foreach (Transform child in selectedObject.transform) {
				child.GetComponent<Renderer> ().enabled = true;
			}
		} 
	}
}
