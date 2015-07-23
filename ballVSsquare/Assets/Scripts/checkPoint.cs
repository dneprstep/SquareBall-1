using UnityEngine;
using System.Collections;

public class checkPoint : MonoBehaviour {

	levelControl lvlC;
	Collider cpCollider;
	void Start () 
	{
		lvlC = GameObject.FindObjectOfType<levelControl> ();
		cpCollider = GetComponent<Collider> ();
	
	}


	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag ("Player")) 
		{
			cpCollider.enabled=false;
			lvlC.OvercomeCheckPoint();
		}
		if (collision.gameObject.CompareTag ("Finish")) 
		{
			cpCollider.enabled=false;
			lvlC.Complete();
		}

	}
}
