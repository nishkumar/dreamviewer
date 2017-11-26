using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideObject : MonoBehaviour {
	public GameObject armChair;
	public GameObject bed1;
	public GameObject bed2;
	public GameObject sofa;
	public GameObject table;
	public GameObject chair;
	public GameObject selectedObject;
	public Button leftButton;
	public Button rightButton;

	public int numObjects;
	public int index;
	public GameObject[] objectArray;

	// Use this for initialization
	void Start () {
		// Initialize object array
		objectArray = new GameObject[numObjects]; 
		objectArray [0] = null;

		if (bed1)
			objectArray [1] = bed1;

		if (bed2)
			objectArray [2] = bed2;
	
		if (sofa)
			objectArray [3] = sofa;

		if (armChair)
			objectArray [4] = armChair;

		if (table)
			objectArray [5] = table;

		if (chair)
			objectArray [6] = chair;

		//////////////////////////////////////////////////////////////////////
		index = 0;

		if(leftButton)
			leftButton.onClick.AddListener(OnClickLeft);

		if(rightButton)
			rightButton.onClick.AddListener(OnClickRight);
	}


	void HideAll(){
		
		if (bed1) {
			bed1.GetComponent<Renderer> ().enabled = false;
		}

		if (bed2) {
			bed2.GetComponent<Renderer> ().enabled = false;
		}

		if (sofa) {
			sofa.GetComponent<Renderer> ().enabled = false;
		}

		if (armChair) {
			armChair.GetComponent<Renderer> ().enabled = false;
		}

		if (table) {
			table.GetComponent<Renderer> ().enabled = false;
		}

		if (chair) {
			chair.GetComponent<Renderer> ().enabled = false;
		}

		/*
		for (int i = 0; i < numObjects; i++) {
			if (objectArray [i] != null) {
				objectArray [i].GetComponent<Renderer> ().enabled = false;
			}
		}
		*/
	}

	void OnClickLeft()
	{
		if (index <= 0) {
			// bed.GetComponent<Renderer> ().enabled = false;
			index = numObjects - 1;
		} else {
			index--;
		}
	}


	void OnClickRight()
	{
		if (index >= numObjects - 1) {
			index = 0;
		} else {
			index++;
		}
	}

	
	// Update is called once per frame
	void Update () {
		// gameObject.GetComponent<Renderer>().enabled = true;
		// bed.GetComponent<Renderer>().enabled = true;
		// Hide all 
		HideAll();

		if (index > 0) {
			selectedObject = objectArray [index];
			selectedObject.GetComponent<Renderer> ().enabled = true;
		} 
	}
}
