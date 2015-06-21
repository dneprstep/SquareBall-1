using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform lookAtObject;

	private GameObject playerPos;
	private Vector3 offset;


	void Start () {

		playerPos = GameObject.FindWithTag ("Player");
		offset = new Vector3 (3, 8, -4);

//		lookAtObject = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		this.transform.position = lookAtObject.position + offset;
	}
	

	void Update () {
	}
	void LateUpdate()
	{
		this.transform.position = lookAtObject.position + offset;
		this.transform.LookAt (lookAtObject);
	}

}
