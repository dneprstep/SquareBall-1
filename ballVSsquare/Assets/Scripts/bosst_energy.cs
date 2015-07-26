using UnityEngine;
using System.Collections;

public class bosst_energy : MonoBehaviour 
{
	public float energyFill;
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
			Debug.Log("Energy+");
			isActive=false;
			onBoostCollision();
		}
	}
	void onBoostCollision()
	{
		sphereScript.Energy+=energyFill;
	}

}
