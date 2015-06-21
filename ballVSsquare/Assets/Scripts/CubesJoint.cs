using UnityEngine;
using System.Collections;

public class CubesJoint : MonoBehaviour {

	public float minDist;
	public float maxDist;
	SpringJoint jointToSphere;
	// Use this for initialization
	void Start () {
		jointToSphere=gameObject.AddComponent<SpringJoint>();
		jointToSphere.minDistance = minDist;
		jointToSphere.maxDistance = maxDist;
		jointToSphere.breakForce = 5;
		jointToSphere.breakTorque = 5;
		jointToSphere.enableCollision = enabled;
		jointToSphere.spring = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collider)
	{
		GameObject collideObject = collider.gameObject;
		if (collider.gameObject.tag == "Player") 
		{
			if(!gameObject.GetComponent("Spring Joint"))
				jointToSphere=gameObject.AddComponent<SpringJoint>();
			jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();
		}
	}

		

}
