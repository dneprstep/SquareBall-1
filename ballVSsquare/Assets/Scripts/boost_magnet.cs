using UnityEngine;
using System.Collections;

public class boost_magnet : MonoBehaviour 
{
	bool isActive;
	SphereControl sphereScript;

	void Start()
	{
		sphereScript = GameObject.FindWithTag ("Player").GetComponent<SphereControl> ();
		isActive = true;
	}


	void OnTriggerEnter(Collider collision)
	{
		if (isActive && collision.CompareTag ("Player")) 
		{
			isActive=false;
			onBoostCollision();
		}
	}
	void onBoostCollision()
	{
		sphereScript.TurnOffMagnet ();
	}




}
