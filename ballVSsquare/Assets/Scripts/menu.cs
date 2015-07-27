using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour 
{
	Rect buttonRect;

	void Start()
	{
		buttonRect = new Rect (Screen.width / 2, Screen.height / 2, 500, 100);
	}

	void OnGUI()
	{
		if (GUI.Button (buttonRect, "PLAY"))
			Application.LoadLevel ("18_07_15");
	}
}
