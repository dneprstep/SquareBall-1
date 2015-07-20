using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform lookAtObject;

	private GameObject playerPos;
//	private Vector3 offset;
	private Vector3 velo=Vector3.zero;
	private Vector3 min;

	void Start () 
	{

		playerPos = GameObject.FindWithTag ("Player");
//		offset = new Vector3 ();
		min = new Vector3 ();
		min.Set (0, 4, -5);


//		lookAtObject = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
	//	this.transform.position.Set( lookAtObject.position.x, lookAtObject.position.y+3, lookAtObject.position.z-2);

		transform.rotation =new Quaternion(0.2f,0,0,1);

		transform.position = min;


	//	Debug.Log ("Position:" + transform.position + ": rotation" + transform.rotation);
	}
	

	void LateUpdate()
	{

		this.transform.position = lookAtObject.position + min;


//		transform.rotation.SetLookRotation (lookAtObject.GetComponent<Rigidbody> ().velocity);
	//	transform.RotateAround (lookAtObject.position, Vector3.up, 10*Time.deltaTime);
	//	this.transform.LookAt (lookAtObject.position + lookAtObject.GetComponent<Rigidbody> ().velocity);
	//	Vector3 rotat = new Vector3 ();
	//	rotat = lookAtObject.GetComponent<Rigidbody> ().velocity;
	//	float speed = lookAtObject.GetComponent<Rigidbody> ().velocity.magnitude;

	//	offset.Set (lookAtObject.position.x, Mathf.Clamp (lookAtObject.position.y, lookAtObject.position.y, lookAtObject.position.y + speed / 5), lookAtObject.position.z + speed / 3);

	//	offset += min;

	//	transform.position = Vector3.SmoothDamp (transform.position, lookAtObject.position+min, ref velo, 0.3f);
//		transform.LookAt(Vector3.SmoothDamp (transform.position, lookAtObject.position, ref velo, 3f));



	//	transform.rotation.SetFromToRotation(lookAtObject.position-transform.position,lookAtObject.position-offset);
	}

}
