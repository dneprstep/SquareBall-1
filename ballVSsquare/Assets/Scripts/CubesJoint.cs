﻿using UnityEngine;
using System.Collections;

public class CubesJoint : MonoBehaviour {

	public float minDist;
	public float maxDist;
	public float jointSpring;
	public float breakPower;

	SpringJoint jointToSphere;
	// Use this for initialization
	void Start () {
/*		jointToSphere=gameObject.AddComponent<SpringJoint>();
		jointToSphere.minDistance = minDist;
		jointToSphere.maxDistance = maxDist;
		jointToSphere.breakForce = 5;
		jointToSphere.breakTorque = 5;
		jointToSphere.enableCollision = enabled;
		jointToSphere.spring = jointSpring;*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collider)
	{
		GameObject collideObject = collider.gameObject;
		if (collider.gameObject.tag == "Player") 
		{
			if(!gameObject.GetComponent<SpringJoint>())
			{
				this.gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);

				jointToSphere=gameObject.AddComponent<SpringJoint>();
				jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();
				jointToSphere.minDistance = minDist;
				jointToSphere.maxDistance = maxDist;
				jointToSphere.breakForce = breakPower;
				jointToSphere.breakTorque = breakPower;
//				jointToSphere.enableCollision = enabled;
				jointToSphere.spring = jointSpring;
				jointToSphere.anchor=new Vector3(0,0,0);
//				jointToSphere.autoConfigureConnectedAnchor=false;
//				jointToSphere.connectedAnchor=collideObject.transform.position;
			}
		}
	}

		

}
