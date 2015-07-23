using UnityEngine;
using System.Collections;

public class levelControl : MonoBehaviour {

	public float roundTime;
	public float currentTime;
	public float checkPointTimeIncrease;

	public float gameStartTime;

	public GameObject[] CheckPoint;
	public GameObject sphereObject;

	gui guiScript;

	void Start () 
	{
		sphereObject = GameObject.FindWithTag ("Player");
		CheckPoint = GameObject.FindGameObjectsWithTag ("CheckPoint");

		guiScript = GameObject.FindObjectOfType<gui> ();
		currentTime = roundTime;

		Invoke ("startLevel", gameStartTime);
	}

	public void OvercomeCheckPoint()
	{
		currentTime += checkPointTimeIncrease;
	}
	public void startLevel()
	{
		sphereObject.GetComponent<SphereControl> ().isActive = true;
		StartCoroutine (levelTimeCheck ());
	}
	IEnumerator levelTimeCheck()
	{
		while (currentTime>0) 
		{
			yield return new WaitForSeconds(1f);
			Debug.Log("time:"+currentTime);
			currentTime--;
		}
		EndGame ();
	}

	public void EndGame ()
	{
		guiScript.window = 2;
	}
	public void Complete ()
	{
		guiScript.window = 1;
	}
}
