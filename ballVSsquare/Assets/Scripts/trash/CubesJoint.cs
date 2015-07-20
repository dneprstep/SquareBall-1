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
	public float breakDistance;

	public bool isCollide;

	public bool isActive;
	public bool isJoint;
	
	public float stunTimeOnBreak;
	
	SpringJoint jointToSphere;


	Rigidbody cubeRB;
	Collider cubeCol;
	bool isGround;
	bool isMagneted;
	


	SphereControl sphereScript;
	GameObject spherePlayer;
	Rigidbody sphereRB;

	Vector3 cubeCurPos;
	Vector3 directionFromCubeToSphere;

	void Start () 
	{
		cubeRB = gameObject.GetComponent<Rigidbody> ();
		cubeCol = GetComponent<Collider> ();
/*		jointToSphere=gameObject.AddComponent<SpringJoint>();
		
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
*/


		sphereScript = GameObject.FindWithTag("Player").GetComponent<SphereControl>();
		sphereRB = GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ();
		spherePlayer = GameObject.FindWithTag ("Player");

		isActive = true;
		isJoint = false;
		isGround = false;
		isMagneted = false;
	}
	

	public void stun_cube(float stun_time)
	{
		isActive = false;
		Invoke ("wake_up_cube", stun_time);
	}

	void wake_up_cube()
	{
		isActive = true;
	}
/*	public void breakJoint()
	{
		cubeRB.useGravity = true;
		jointToSphere.connectedBody = null;
		jointToSphere.spring = 0;
		stun_cube (stunTimeOnBreak);
		isJoint = false;
		cubeRB.freezeRotation = false;
	}
	public void weakJointCube(Rigidbody RB)
	{
		cubeRB.useGravity = false;
		jointToSphere.connectedBody = RB;
		jointToSphere.spring = weakJointSpring;
		jointToSphere.damper = weakJointDamper;
	}

	public void jointCube(Rigidbody RB)
	{
		cubeRB.useGravity = false;


		jointToSphere.connectedBody = RB;
		jointToSphere.spring = jointSpring;
		jointToSphere.damper = jointDamper;
		isJoint = true;
		cubeRB.freezeRotation = true;
	}




	public void magnetCube(Rigidbody RB)
	{
		cubeRB.useGravity = false;
		cubeRB.velocity = (RB.transform.position - transform.position).normalized*magnetPower;
	}
	*/




	IEnumerator Magnet(Rigidbody RB)
	{
		cubeRB.useGravity = false;

		cubeRB.velocity = (RB.transform.position - transform.position).normalized*magnetPower;
		yield return new WaitForSeconds (.5f);		
		isMagneted = false;
	}
	void OnTriggerEnter(Collider col)
	{
		if (isActive && !isJoint && !isMagneted) 
		{
			if(col.CompareTag("Player"))
			{
				if(sphereScript.isMagnet)
				{
					Debug.Log ("Cube magneted");
					isMagneted=true;
					cubeRB.isKinematic=false;
					StartCoroutine(Magnet(col.attachedRigidbody));
	//				magnetCube (col.attachedRigidbody);
				}

			}
			
		}
	}

	void OnCollisionEnter(Collision collider)
	{
		if(!isGround && collider.gameObject.CompareTag ("Plane"))
		{
			cubeRB.isKinematic=true;
			isGround=true;
		}

		if (collider.gameObject.CompareTag ("Player")) 
		{
			if(isActive && !isJoint && sphereScript.isMagnet)
			{
				StartCoroutine (onCollisionSphere());
		//		cubeCol.enabled=false;
				//			cubeCurPos=transform.position;
	//			Debug.Log ("cubeCurPos"+cubeCurPos);
		//		directionFromCubeToSphere =transform.position - spherePlayer.transform.position ;
	//			Debug.Log ("directionFromCubeToSphere"+directionFromCubeToSphere);
		//		cubeRB.isKinematic=true;
		//		isJoint=true;

			}
		}
	}

	IEnumerator onCollisionSphere()
	{

		cubeCol.enabled=false;
		isMagneted = true;
		isJoint=true;
		cubeRB.isKinematic=true;
		yield return null;
		directionFromCubeToSphere =transform.position - spherePlayer.transform.position ;
	}


	void Update()
	{
		if (isJoint) 
		{
			StartCoroutine(moveCube());
		}
	}
	IEnumerator moveCube()
	{
		yield return null;

		Vector3 temp=spherePlayer.transform.position + directionFromCubeToSphere;


//		transform.position = temp;
		transform.RotateAround (spherePlayer.transform.position, spherePlayer.transform.localRotation.eulerAngles, Time.deltaTime * sphereRB.angularVelocity.magnitude);
	}

/*	void OnTriggerExit(Collider col)
	{

		if (isActive && isJoint) 
		{
			if(col.CompareTag("Player")) 
			{
				if(sphereScript.isMagnet)
				{
					breakJoint ();
					sphereScript.deleteCollideCube (gameObject);
				}

			}
		}
	}
	*/	
		
	//	TURN OFF GRAVITY
/*	void OnCollisionEnter(Collision collider)
	{
		GameObject collideObject = collider.gameObject;
		if (collider.gameObject.tag == "Player" 
		  //  && collideObject.GetComponent<SphereControl>()!=null
		    ) 
		{
			if(collideObject.GetComponent<SphereControl>().isMagnet 
			   && this.isActive==true)
			{
				if(
				//	gameObject.GetComponent<SpringJoint>()!=null && 
					gameObject.GetComponent<SpringJoint>().connectedBody==null)
				{
					Debug.Log ("Joint"+gameObject.name);


					gameObject.GetComponent<SpringJoint>().connectedBody=collideObject.GetComponent<Rigidbody>();
					gameObject.GetComponent<SpringJoint>().spring = jointSpring;
					gameObject.GetComponent<SpringJoint> ().damper=jointDamper;
					gameObject.GetComponent<Rigidbody>().useGravity=false;

					collideObject.GetComponent<SphereControl>().addCollideCube (gameObject);
				}
			}
		}
	}*/
	
	
	
}