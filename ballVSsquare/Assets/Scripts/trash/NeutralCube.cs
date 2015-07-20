using UnityEngine;
using System.Collections;

public class NeutralCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision collider)
	{
		Debug.Log ("On Collision");
		if (collider.gameObject.CompareTag ("Cubes")) 
		{
//			collider.gameObject.GetComponent<testJoint>().breakJoint (transform.position);
		}

	}

}
