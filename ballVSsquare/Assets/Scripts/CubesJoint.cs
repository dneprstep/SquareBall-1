using UnityEngine;
using System.Collections;

public class CubesJoint : MonoBehaviour {
	
	public float minDist;
	public float maxDist;

	public float magnetPower;

	public float jointSpring;
	public float weakJointSpring;

	public float jointDamper;
	public float weakJointDamper;

	public float breakPower;
	public bool isCollide;
	
	SpringJoint jointToSphere;
	Rigidbody cubeRB;
	public bool isActive;
	

	void Start () 
	{
		cubeRB = gameObject.GetComponent<Rigidbody> ();
		jointToSphere=gameObject.AddComponent<SpringJoint>();
		
		jointToSphere.minDistance = minDist;
		jointToSphere.maxDistance = maxDist;
		//		jointToSphere.breakForce = breakPower;
		//		jointToSphere.breakTorque = breakPower;
		jointToSphere.enableCollision = isCollide;
		jointToSphere.spring = 0;
//		jointToSphere.damper=jointDamper;
		jointToSphere.anchor=new Vector3(0,0,0);
		jointToSphere.autoConfigureConnectedAnchor = false;
		jointToSphere.connectedAnchor = new Vector3 (0, 0, 0);

		isActive = true;
	}
	
	
	void Update () 
	{
	}

	public void stun_cube(int stun_time)
	{
		isActive = false;
		Invoke ("wake_up_cube", stun_time);
	}

	void wake_up_cube()
	{
		isActive = true;
	}

	public void collideCube(Rigidbody RB)
	{
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		gameObject.GetComponent<SpringJoint> ().connectedBody = RB;
		gameObject.GetComponent<SpringJoint> ().spring = jointSpring;
		gameObject.GetComponent<SpringJoint> ().damper = jointDamper;
	}
	public void magnetCube(Rigidbody RB)
	{
//		gameObject.GetComponent<Rigidbody> ().useGravity = false;

		cubeRB.velocity = (RB.transform.position - transform.position).normalized*magnetPower;


/*		gameObject.GetComponent<SpringJoint> ().connectedBody = RB;
		gameObject.GetComponent<SpringJoint> ().spring = weakJointSpring;
		gameObject.GetComponent<SpringJoint> ().damper = weakJointDamper;
*/	}

	//	TURN OFF GRAVITY
	void OnCollisionEnter(Collision collider)
	{
		GameObject collideObject = collider.gameObject;
		if (collider.gameObject.tag == "Player" 
		  //  && collideObject.GetComponent<SphereControl>()!=null
		    ) 
		{
			if(collideObject.GetComponent<SphereControl>().isMagnet && this.isActive==true)
			{
				if(
				//	gameObject.GetComponent<SpringJoint>()!=null && 
					gameObject.GetComponent<SpringJoint>().connectedBody==null)
				{
					Debug.Log ("Joint"+gameObject.name);

					gameObject.GetComponent<Rigidbody>().useGravity=false;
					gameObject.GetComponent<SpringJoint>().connectedBody=collideObject.GetComponent<Rigidbody>();
					gameObject.GetComponent<SpringJoint>().spring = jointSpring;
					gameObject.GetComponent<SpringJoint> ().damper=jointDamper;

					collideObject.GetComponent<SphereControl>().addCollideCube (gameObject);
				}
			}
		}
	}
	
	
	
}