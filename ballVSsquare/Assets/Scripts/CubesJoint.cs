using UnityEngine;
using System.Collections;

public class CubesJoint : MonoBehaviour {

	public float minDist;
	public float maxDist;
	public float jointSpring;
	public float breakPower;

	SpringJoint jointToSphere;
<<<<<<< HEAD


=======
>>>>>>> parent of 9aa5d42... Magnet
	// Use this for initialization
	void Start () {
/*		jointToSphere=gameObject.AddComponent<SpringJoint>();
		jointToSphere.minDistance = minDist;
		jointToSphere.maxDistance = maxDist;
<<<<<<< HEAD

//		jointToSphere.breakForce = breakPower;
//		jointToSphere.breakTorque = breakPower;

		jointToSphere.enableCollision = enabled;
		jointToSphere.spring = 0;

		jointToSphere.anchor=new Vector3(0,0,0);
		jointToSphere.autoConfigureConnectedAnchor=false;
		jointToSphere.connectedAnchor=new Vector3(0,0,0);
=======
		jointToSphere.breakForce = 5;
		jointToSphere.breakTorque = 5;
		jointToSphere.enableCollision = enabled;
		jointToSphere.spring = jointSpring;*/
>>>>>>> parent of 9aa5d42... Magnet
	}
	
	// Update is called once per frame
	void Update () {
	
	}
<<<<<<< HEAD


	//TURN OFF GRAVITY on collision
=======
>>>>>>> parent of 9aa5d42... Magnet
	void OnCollisionEnter(Collision collider)
	{
		GameObject collideObject = collider.gameObject;
		if (collider.gameObject.tag == "Player") 
		{
<<<<<<< HEAD
			if(gameObject.GetComponent<SpringJoint>() && gameObject.GetComponent<SpringJoint>().connectedBody==null)
			{
				jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();
				jointToSphere.spring=jointSpring;
//				gameObject.GetComponent<Rigidbody>().useGravity=false;

/*				jointToSphere=gameObject.AddComponent<HingeJoint>();
				jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();
//				jointToSphere.breakForce=breakPower;
//				jointToSphere.breakTorque=breakPower;
				jointToSphere.enableCollision=true;
				jointToSphere.anchor=new Vector3(0,0,0);

				jointToSphere.useSpring=true;
				JointSpring JSstruct;
				JSstruct.damper=2;
				JSstruct.spring=10;
				JSstruct.targetPosition=0;
				jointToSphere.spring=JSstruct;
*/



				/*
				jointToSphere=gameObject.AddComponent<SpringJoint>();
				jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();

				jointToSphere.breakForce = breakPower;
				jointToSphere.breakTorque = breakPower;

				jointToSphere.minDistance = minDist;
				jointToSphere.maxDistance = maxDist;
				jointToSphere.enableCollision = enabled;

				jointToSphere.anchor=new Vector3(0,0,0);


				jointToSphere.spring = jointSpring;
				*/



/*				jointToSphere=gameObject.AddComponent<SpringJoint>();

=======
			if(!gameObject.GetComponent<SpringJoint>())
			{
				this.gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);

				jointToSphere=gameObject.AddComponent<SpringJoint>();
				jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();
>>>>>>> parent of 9aa5d42... Magnet
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
