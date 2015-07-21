using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereControl : MonoBehaviour {
	
	Rigidbody sphereRB;

	Vector3 HorizontalForce;
	Vector3 VerticalForce;

	public bool isMagnet;
	public float turnOffMagnetTime;
	public float forcePower;
	public float maxVelocity;
	public float cubeUnjointPower;


	public Vector3 startPosition;
	public Vector3 constVelocity;

	public float energyRecharge;
	public float maxEnergy;

	public float springBoardBoost;
	public float veloBoostTime;

	List<testJoint> collideCubes;


	Vector3 explosivePos;
	Rigidbody rb;

	Transform sphereTransofrm;

	Vector2 touchCoord;
	Vector2 playerTouch;

	float key;

	bool isBoosted;

	float Energy;

	float magnetEnergyCost=30;
	float explEnergyCost=50;


	Vector3 tempVelocity;

	void Start () 
	{
		transform.position = startPosition;
		sphereRB = gameObject.GetComponent<Rigidbody> ();
		sphereTransofrm = GetComponent<Transform> ();

		collideCubes = new List<testJoint> ();

		HorizontalForce = new Vector3 (forcePower, 0.0f, 0.0f);
		VerticalForce = new Vector3 (0.0f, 0.0f, forcePower);

		GetComponent<MeshFilter> ().mesh.Optimize ();
		sphereRB.AddForce(new Vector3(0,0,100),ForceMode.Force);
		touchCoord = new Vector2 ();


		Energy = maxEnergy;
		isBoosted = false;


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
			yield return null;
			if (sphereRB.velocity.magnitude > maxVelocity) 
			{
				Debug.Log ("Max magnitude:" + sphereRB.velocity.magnitude);
				sphereRB.velocity = Vector3.ClampMagnitude (sphereRB.velocity, maxVelocity);
			} else 
			{
				sphereRB.AddForce (constVelocity, ForceMode.Impulse);
			}
		}

	}


	public void addCollideCube(testJoint collideCube)
	{
		collideCubes.Add (collideCube);
	}
	public void deleteCollideCube(testJoint collideCube)
	{
		collideCubes.Remove (collideCube);
	}
	
	void TurnOnMagnet()
	{
		isMagnet = true;
	}

	void OnGUI()
	{
		GUI.Box (new Rect (10, 10, 100, 50), touchCoord.ToString());
	}



	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Terrain")) 
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


		if (collision.gameObject.name == "BoostQuad" && !isBoosted) 
		{
			StartCoroutine (VelocityBoost(springBoardBoost));
			                
			sphereRB.AddForce(sphereRB.velocity.normalized*forcePower*2,ForceMode.Impulse);
			Debug.Log ("Boost");

		}
	}
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

		touchScan ();


		if ((key = Input.GetAxis ("Horizontal")) != 0) {
			sphereRB.AddForce (HorizontalForce * key);
		}
		if ((key = Input.GetAxis ("Vertical")) != 0) {
			sphereRB.AddForce (VerticalForce * key);
		}



		if (Input.GetKeyDown (KeyCode.M)) 
		{
			if(Energy-magnetEnergyCost>0)
			{
				Energy-=magnetEnergyCost;
				if(collideCubes.Count>0 && isMagnet)
					StartCoroutine (Demagnetized(false)	);
			}
			else
			{
				Debug.Log ("Not enought energy");
			}
		}

		
		if ((key = Input.GetAxis ("Jump")) != 0) 
		{
			if(Energy-explEnergyCost>0)
			{
				Energy-=explEnergyCost;

			if(collideCubes.Count>0 && isMagnet)
				StartCoroutine (Demagnetized(true)	);
			}
			else
			{
				Debug.Log ("Not enought energy");
			}
		}
	}

	IEnumerator Demagnetized(bool explosive)
	{
		isMagnet = false;
		sphereRB.velocity = Vector3.ClampMagnitude (sphereRB.velocity, 0);
//		sphereRB.angularVelocity = Vector3.ClampMagnitude (sphereRB.angularVelocity, 0);
		if (explosive) 
		{
			sphereRB.AddForce (Vector3.up * forcePower, ForceMode.Acceleration);

			yield return new WaitForSeconds(0.5f);

			sphereRB.constraints = RigidbodyConstraints.FreezePosition;
		}
		Debug.Log ("Cubes on sphere" + collideCubes.Count);
		int cubesCount = collideCubes.Count;
		sphereRB.AddTorque(0,forcePower,0);
		yield return new WaitForSeconds(0.8f);

		int i = 0;

		foreach (var item in collideCubes) 
		{
			if(!explosive)
			{
				if(i%(cubesCount/4)==0)
				{
					Debug.Log ("i:"+i);
					yield return new WaitForSeconds(0.3f);
				}
				
				item.breakJoint ();
			}
			else
			{
				yield return null;
				item.ExplJointBreak (Vector3.zero);

			}
			i++;
		}
		if (explosive)
			sphereRB.constraints = RigidbodyConstraints.None;

		collideCubes.Clear ();

		// TURN ON Sphere magnet til variable turnOffMagnetTime
		Invoke ("TurnOnMagnet", turnOffMagnetTime);

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




/*	IEnumerator touchControl(Touch touch)
	{
		yield return new WaitForEndOfFrame();
		if(touch.position.x<Screen.width/2)
			sphereRB.AddForce (-HorizontalForce);
		else
			if(touch.position.x>Screen.width/2)
				sphereRB.AddForce (HorizontalForce);
	}
*/
}