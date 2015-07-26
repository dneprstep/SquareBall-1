using UnityEngine;
using System.Collections;

public class BigMagnet : MonoBehaviour 
{
	public float magnetForce;

	Rigidbody sphereRB;
	Transform sphereTransform;
	SphereControl sphereScript;

	Vector3 direction;

	void Start()
	{
		sphereTransform = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		sphereRB = GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ();
		sphereScript=GameObject.FindWithTag ("Player").GetComponent<SphereControl> ();
	}
	void OnTriggerStay(Collider collider)
	{
		if (collider.CompareTag ("Player") && sphereScript.isActive && sphereScript.isMagnet) 
		{
			direction = transform.position-sphereTransform.position;
			direction.Normalize ();
			sphereRB.AddForce (direction*magnetForce,ForceMode.Impulse);
		}
	}

}
