using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereControl : MonoBehaviour 
{
	
	Vector3 HorizontalForce;
	Vector3 VerticalForce;

	public bool isMagnet;
	public bool isActive;
	public float turnOffMagnetTime;
	public float forcePower;
	public float maxVelocity;
	public float cubeUnjointPower;

	public Vector3 startPosition;
	public Vector3 constVelocity;

	public float energyRecharge;
	public float maxEnergy;
	public float Energy;

	public float springBoardBoost;
	public float veloBoostTime;

	public float maxAngularVelocity;
	public float cubeMass;

	List<testJoint> collideCubes;


	Transform sphereTransofrm;
	Rigidbody sphereRB;

	levelControl lvlC;
	
	Vector2 playerTouch;

	float key;

	bool isBoosted;

	public float velocityPogreshnost;



	float magnetEnergyCost=30;
	float explEnergyCost=50;

	Vector3 tempVelocity;

	float distanceToGround;

	void Start () 
	{

		transform.position = startPosition;
		sphereRB = gameObject.GetComponent<Rigidbody> ();
		sphereTransofrm = GetComponent<Transform> ();

		lvlC = GameObject.FindObjectOfType<levelControl> ();

		collideCubes = new List<testJoint> ();

		HorizontalForce = new Vector3 (forcePower, 0.0f, 0.0f);
		VerticalForce = new Vector3 (0.0f, 0.0f, forcePower);

		GetComponent<MeshFilter> ().mesh.Optimize ();

		sphereRB.AddForce(new Vector3(0,0,100),ForceMode.Force);

		Energy = maxEnergy;
		isBoosted = false;
		isActive = false;

		distanceToGround = GetComponent<Collider> ().bounds.extents.y+0.1f;

		StartCoroutine ("EnergyFill");
		StartCoroutine ("sphereConstForce");
	}
	IEnumerator EnergyFill()
	{
		while (true) 
		{
			if(Energy>maxEnergy)
				Energy=maxEnergy;
			else
			{
				yield return null;
				Energy += energyRecharge * Time.deltaTime;
			}
		}
	}
	IEnumerator sphereConstForce()
	{
		while (true) 
		{
			yield return new WaitForSeconds(.1f);
			if (sphereRB.velocity.magnitude > maxVelocity) 
			{
//				Debug.Log ("Max magnitude:" + sphereRB.velocity.magnitude);
				sphereRB.velocity = Vector3.ClampMagnitude (sphereRB.velocity, maxVelocity);
			} else 
			{
				if(IsGrounded () && isActive)
					sphereRB.AddForce (constVelocity, ForceMode.Impulse);
			}
		}
	}

	bool IsGrounded()
	{
		return Physics.Raycast (transform.position, Vector3.down, distanceToGround);
	}


	public void addCollideCube(testJoint collideCube)
	{
		collideCubes.Add (collideCube);
		sphereRB.mass += cubeMass;
	}
	public void deleteCollideCube(testJoint collideCube)
	{
		collideCubes.Remove (collideCube);
		sphereRB.mass -= cubeMass;
		Debug.Log ("mass remove:" + sphereRB.mass);
	}
	
	public void TurnOffMagnet()
	{
		StartCoroutine (TurnOffMagnetTillTime ());
	}
	IEnumerator TurnOffMagnetTillTime()
	{
		isMagnet = false;
		yield return new WaitForSeconds (turnOffMagnetTime);
		isMagnet = true;
	}



/*	void OnCollisionEnter(Collision collision)
	{
//		StartCoroutine (collisionCheck(-collision.relativeVelocity));

		if (collision.gameObject.CompareTag ("Plane")) 
		{

			Debug.Log ("relativeVelocity"+collision.relativeVelocity);

			if(collision.relativeVelocity.magnitude>cubeUnjointPower)
			{

				for(int i=0;i<collideCubes.Count;i++)
				{
					collideCubes[i].ExplJointBreak (-collision.relativeVelocity);
					deleteCollideCube (collideCubes[i]);
				}
				Debug.Log ("Warning collision");
			} 
		}
	}
	IEnumerator collisionCheck(Vector3 collision)
	{
		Vector3 oldVelocity = sphereRB.velocity;
		yield return new WaitForSeconds (.15f);
		Vector3 newVelocity = sphereRB.velocity;

		if (newVelocity.magnitude + velocityPogreshnost < oldVelocity.magnitude) 
		{
			Debug.Log ("oldVelocity"+oldVelocity);
			Debug.Log ("newVelocity"+newVelocity);
			for(int i=0;i<collideCubes.Count;i++)
			{
				collideCubes[i].ExplJointBreak (-collision);
				deleteCollideCube (collideCubes[i]);
			}

			Debug.Log ("Warning collision");
		}
	}
*/


	IEnumerator VelocityBoost(float veloBoost)
	{
		isBoosted = true;
		float temp = maxVelocity;
		maxVelocity += veloBoost;
		yield return new WaitForSeconds (veloBoostTime);
		maxVelocity = temp;
		isBoosted = false;
	}
	
	
	
	
	void Update()
	{
//		Debug.Log ("Energy:" + Energy);
//		Debug.Log ("Sphere Velocity:" + sphereRB.velocity);
//		Debug.Log ("SacelerateSphere:" + acelerateSphere.z);
		if (isActive) 
		{
			if (IsGrounded ()) 
			{
				touchScan ();


				if ((key = Input.GetAxis ("Horizontal")) != 0) {
					sphereRB.AddForce (HorizontalForce * key);
				}
				if ((key = Input.GetAxis ("Vertical")) != 0) {
					sphereRB.AddForce (VerticalForce * key);
				}

			}

			if (Input.GetKeyDown (KeyCode.M)) {
				if (Energy - magnetEnergyCost > 0) 
				{
					Energy -= magnetEnergyCost;
					if(isMagnet)
					{	
						isMagnet=false;
						TurnOffMagnet ();
						if (collideCubes.Count > 0)
							StartCoroutine (Demagnetized (false));
					}
				} else 
				{
					Debug.Log ("Not enought energy");
				}
				
			}

		
			if ((key = Input.GetAxis ("Jump")) != 0) 
			{
				if (Energy - explEnergyCost > 0) {
					Energy -= explEnergyCost;
					if(isMagnet)
					{	
						isMagnet=false;
						TurnOffMagnet ();
						if (collideCubes.Count > 0)
							StartCoroutine (Demagnetized (true));
					}
				} else {
					Debug.Log ("Not enought energy");
				}
			}
		}
	}

	IEnumerator Demagnetized(bool explosive)
	{
		Debug.Log ("Magnet:" + isMagnet.ToString ());
		sphereRB.velocity = Vector3.ClampMagnitude (sphereRB.velocity, 0);
		sphereRB.angularVelocity = Vector3.ClampMagnitude (sphereRB.angularVelocity, maxAngularVelocity);
		if (explosive)
		{
			sphereRB.AddForce (Vector3.up * forcePower, ForceMode.Acceleration);

			yield return new WaitForSeconds(0.5f);

			sphereRB.constraints = RigidbodyConstraints.FreezePosition;
		}
		Debug.Log ("Cubes on sphere" + collideCubes.Count);
		int cubesCount = collideCubes.Count;

//		yield return new WaitForSeconds(0.8f);

		int i = 0;

		foreach (var item in collideCubes)
		{
			if(!explosive)
			{
//				if(i%(cubesCount/4)==0)
				{
//					Debug.Log ("i:"+i);
					yield return new WaitForSeconds(0.15f);
				}
				
				item.breakJoint ();
				sphereRB.mass -= cubeMass;
			}
			else
			{
				yield return null;
				item.ExplJointBreak (Vector3.zero);
				sphereRB.mass -= cubeMass;
			}
			i++;
		}
		if (explosive) 
		{
			sphereRB.constraints = RigidbodyConstraints.None;
			sphereRB.AddForce (HorizontalForce * key,ForceMode.Impulse);
		}

		collideCubes.Clear ();

		// TURN ON Sphere magnet til variable turnOffMagnetTime
//		TurnOffMagnet ();
//		Invoke ("TurnOffMagnetTillTime", turnOffMagnetTime);

	}

	void touchScan()
	{
		if (Input.touchCount > 0) 
		{
			foreach (var touch in Input.touches) 
			{
				if (touch.phase == TouchPhase.Began)
				if (touch.tapCount == 2)
					StartCoroutine (Demagnetized (true));
			}

			if (Input.touchCount > 0 
				&& (Input.touches [0].phase == TouchPhase.Moved)) {
				playerTouch = Input.GetTouch (0).deltaPosition;
				playerTouch.y = 0;
				sphereRB.AddForce (playerTouch * forcePower);
			}
		}
	}

}