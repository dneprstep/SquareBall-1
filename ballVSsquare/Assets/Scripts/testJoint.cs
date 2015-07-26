using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testJoint : MonoBehaviour {

	public bool isActive;
	public bool isJoint;
	public bool isMagneted;

	public float jointRadius;
	public float stunTime;

	public float speed;

	public float explForce; 

	public Color defaultColor;
	public Color deactiveColor;
	public Color magnetColor;
	public Color jointColor;

	GameObject sphere;
	Transform sphereTransform;
	SphereControl sphereScript;
	float sphereVelocity;

	Vector3 direction;

	Rigidbody cubeRB;
	Transform cubeTransorm;
	Collider cubeCollider;
	Material cubeMaterial;

	float distance;
	Vector3 explDirection;

	void Start ()
	{

		sphere = GameObject.FindWithTag ("Player");
		sphereTransform = sphere.transform;
		sphereScript = GameObject.FindWithTag("Player").GetComponent<SphereControl> ();
		sphereVelocity = sphere.GetComponent<Rigidbody> ().velocity.magnitude;

		isJoint = false;
		isMagneted = false;
		isActive = true;

		cubeRB = GetComponent<Rigidbody> ();
		cubeTransorm = GetComponent<Transform> ();
		cubeCollider = GetComponent<Collider> ();
		cubeMaterial = GetComponent<MeshRenderer> ().material;

		cubeMaterial.color = defaultColor;

		Physics.IgnoreCollision (GetComponent<Collider> (), sphere.GetComponent<Collider> ());

	}

	void StunCube()
	{
		isActive = false;
	}

	

	void Update () 
	{

		if (isActive ) 
		{
			if (isMagneted && sphereScript.isMagnet)
				StartCoroutine (Magnet ());

			if (isJoint) 
				StartCoroutine (CubeJoint ());

		}

	}

	IEnumerator Magnet()
	{

		direction = sphereTransform.position - cubeTransorm.position;
		distance = direction.sqrMagnitude;
		if (distance <= jointRadius) 
		{
			inJointRadius();

		} else 
		{
			direction.Normalize ();
			cubeTransorm.Translate (direction * speed * Time.deltaTime);
		}
		yield return null;
	}
	void inJointRadius()
	{
		isMagneted = false;
		cubeTransorm.SetParent (sphereTransform);

		cubeRB.isKinematic = true;
		cubeRB.detectCollisions = false;

		isJoint = true;
		sphereScript.addCollideCube (this);

		cubeMaterial.color = jointColor;

//		Debug.Log ("Cube joint");
	}

	IEnumerator CubeJoint()
	{
		yield return null;
	}


	void OnTriggerEnter(Collider collider)
	{
		if (isActive) 
		{
			if (collider.gameObject.CompareTag ("Player") && sphereScript.isMagnet) 
			{
				OnMagnetToSphere ();
			}
		}
	}


	public void OnMagnetToSphere()
	{
		isMagneted=true;
//		cubeRB.useGravity=false;
		cubeRB.isKinematic = true;
		cubeMaterial.color = magnetColor;
	}
	



	void OnTriggerExit(Collider collider)
	{
		if (isActive) 
		{
			if (collider.gameObject.CompareTag ("Player")) 
				OffMagnetToSphere ();
		}
	}
	public void OffMagnetToSphere()
	{
		isJoint = false;
		isMagneted = false;
//		cubeRB.useGravity = true;
//		cubeRB.isKinematic = false;

		cubeMaterial.color=defaultColor;
		//		cubeRB.useGravity = true;
	}


	public void breakJoint()
	{
		isJoint = false;
		isMagneted = false;
		isActive = false;
		cubeTransorm.parent = null;

		cubeRB.detectCollisions = true;
		cubeRB.isKinematic = false;


		cubeRB.useGravity = true;

		cubeMaterial.color = deactiveColor;
//		sphereScript.deleteCollideCube (this);
//		Invoke ("StunCube", stunTime);
	}

	public void ExplJointBreak(Vector3 explDirection)
	{
		breakJoint ();

		if (explDirection == Vector3.zero) 
		{
			Debug.Log ("Zero");
			explDirection = cubeTransorm.position - sphereTransform.position;

			explDirection.Normalize ();
		}
		cubeRB.AddForce(explDirection*(sphereVelocity),ForceMode.Impulse);
	}

}
