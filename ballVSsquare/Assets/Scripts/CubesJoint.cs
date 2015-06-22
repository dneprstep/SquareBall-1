using UnityEngine;
using System.Collections;

public class CubesJoint : MonoBehaviour {
	
	public float minDist;
	public float maxDist;
	public float jointSpring;
	public float breakPower;
	
	SpringJoint jointToSphere;
	

	void Start () 
	{
		jointToSphere=gameObject.AddComponent<SpringJoint>();
		
		jointToSphere.minDistance = minDist;
		jointToSphere.maxDistance = maxDist;
		//		jointToSphere.breakForce = breakPower;
		//		jointToSphere.breakTorque = breakPower;
		jointToSphere.enableCollision = enabled;
		jointToSphere.spring = 0;
		jointToSphere.anchor=new Vector3(0,0,0);
		jointToSphere.autoConfigureConnectedAnchor = false;
		jointToSphere.connectedAnchor = new Vector3 (0, 0, 0);
	}
	
	
	void Update () {
		
	}

	//	TURN OFF GRAVITY
	void OnCollisionEnter(Collision collider)
	{
		GameObject collideObject = collider.gameObject;
		if (collider.gameObject.tag == "Player") 
		{
			if(gameObject.GetComponent<SpringJoint>() && gameObject.GetComponent<SpringJoint>().connectedBody==null)
			{
				gameObject.GetComponent<Rigidbody>().useGravity=false;
				jointToSphere.connectedBody=collideObject.GetComponent<Rigidbody>();
				jointToSphere.spring = jointSpring;
				Debug.Log ("Joint"+gameObject.name);
			}
		}
	}
	
	
	
}