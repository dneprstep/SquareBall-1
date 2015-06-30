using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereControl : MonoBehaviour {
	
	public Rigidbody sphereRB;
	public float forcePower;
	public float magnetPower;
	public float magnetRadius;
	public Vector3 HorizontalForce;
	public Vector3 VerticalForce;
	public bool isMagnet;
	public float explosivePower;
	public float explosiveRadius;
	public float turnOffMagnetTime;
	public int stun_cube_time;
	public float maxVelocity;
	public float colliderRadius;

	Collider [] magnetZone;
	List<GameObject> collideCubes;
	Vector3 explosivePos;
	Rigidbody rb;
	Vector3 direction;
	
	void Start () {
		sphereRB = gameObject.GetComponent<Rigidbody> ();
		collideCubes = new List<GameObject> ();
		HorizontalForce = new Vector3 (forcePower, 0.0f, 0.0f);
		VerticalForce = new Vector3 (0.0f, 0.0f, forcePower);	
		direction = new Vector3 ();
	}

	public void addCollideCube(GameObject collideCube)
	{
		collideCubes.Add (collideCube);
	}
	
	void Update () 
	{

	}
	void TurnOnMagnet()
	{
		isMagnet = true;
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "gate") 
			Destroy (col.gameObject);
	}
	void FixedUpdate()
	{

//		Debug.Log ("Sphere Velocity:" + gameObject.GetComponent<Rigidbody> ().velocity);

		//max Velocity
		if (gameObject.GetComponent<Rigidbody> ().velocity.magnitude > maxVelocity) 
		{
			Debug.Log("Magnitude"+gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
			gameObject.GetComponent<Rigidbody> ().velocity=Vector3.ClampMagnitude(gameObject.GetComponent<Rigidbody> ().velocity,maxVelocity);
		//	gameObject.GetComponent<Rigidbody> ().AddForce (0, 0, forcePower);
		}
		
		float key;
		if ((key = Input.GetAxis("Horizontal")) != 0) 
		{
			sphereRB.AddForce (HorizontalForce*key);
		}
		if ((key = Input.GetAxis("Vertical")) != 0) 
		{
			sphereRB.AddForce (VerticalForce*key);
		}
		if (Input.GetKeyDown (KeyCode.M))
		{
			isMagnet=false;
/*			magnetZone = Physics.OverlapSphere(transform.position,magnetRadius);
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes" && 
				   item.GetComponent<SpringJoint>().connectedBody==gameObject.GetComponent<Rigidbody>())
					return true;
				else
					return false;
			});
	*/		
			collideCubes.ForEach(item=> 
			                     {
			                     item.GetComponent<Rigidbody>().useGravity=true;
			                     item.GetComponent<SpringJoint>().spring=0;
				item.GetComponent<SpringJoint>().connectedBody=null;});

/*			foreach (var item in magnetZone) 
			{
				item.attachedRigidbody.useGravity=true;
				item.GetComponent<SpringJoint>().spring=0;
				item.GetComponent<SpringJoint>().connectedBody=null;
			}*/
			collideCubes.Clear ();
			// TURN ON Sphere magnet til variable turnOffMagnetTime
			Invoke ("TurnOnMagnet", turnOffMagnetTime);
		}

		
		if ((key = Input.GetAxis ("Jump")) != 0) 
		{
			collideCubes.ForEach(item=> 
			                     {
				item.GetComponent<Rigidbody>().useGravity=true;
				item.GetComponent<SpringJoint>().connectedBody=null;
				item.GetComponent<SpringJoint>().spring=0;
				item.gameObject.GetComponent<CubesJoint>().stun_cube (stun_cube_time);
				item.GetComponent<Rigidbody>().AddExplosionForce(explosivePower,transform.position,explosiveRadius);
			});

			collideCubes.Clear ();






	/*		magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);
			
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes" && item.gameObject.GetComponent<SpringJoint>())
					return true;
				else
					return false;
			});
			
			foreach (var item in magnetZone) 
			{
				item.GetComponent<Rigidbody>().useGravity=true;
				item.GetComponent<SpringJoint>().connectedBody=null;
				item.GetComponent<SpringJoint>().spring=0;
				item.gameObject.GetComponent<CubesJoint>().stun_cube (stun_cube_time);
				item.attachedRigidbody.AddExplosionForce(explosivePower,this.gameObject.transform.position,explosiveRadius);
			}

			collideCubes.Clear ();*/
		}
		

		
		if (isMagnet == true) 
		{
/*			magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius/3);
			
			
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes" && item.GetComponent<CubesJoint>().isActive)
					if(item.GetComponent<SpringJoint>()!=null && item.GetComponent<SpringJoint>().connectedBody==null)
						return true;
				else
					return false;
				else
					return false;
			});
			
			foreach (var item in magnetZone) 
			{
	//			item.GetComponent<CubesJoint>().collideCube (gameObject.GetComponent<Rigidbody>());
				//				item.GetComponent<Rigidbody>().useGravity=false;
				//				item.GetComponent<SpringJoint>().connectedBody=gameObject.GetComponent<Rigidbody>();
				//				item.GetComponent<SpringJoint>().spring=1;
				
				
				/*				direction=(transform.position-item.transform.position).normalized;
				item.gameObject.GetComponent<Rigidbody>().velocity=direction * magnetPower;
*///			}





			magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);
			
			
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes" && item.GetComponent<CubesJoint>().isActive)
					if(
					//	item.GetComponent<SpringJoint>()!=null && 
						item.GetComponent<SpringJoint>().connectedBody==null)
						return true;
					else
						return false;
				else
					return false;
			});
			
			foreach (var item in magnetZone) 
			{

//				item.GetComponent<CubesJoint>().magnetCube (gameObject.GetComponent<Rigidbody>());
//				item.GetComponent<Rigidbody>().useGravity=false;
//				item.GetComponent<SpringJoint>().connectedBody=gameObject.GetComponent<Rigidbody>();
//				item.GetComponent<SpringJoint>().spring=1;


				direction=(gameObject.transform.position-item.transform.position).normalized;
//				Debug.Log ("sphere force direction"+direction * magnetPower);
				item.GetComponent<Rigidbody>().velocity = direction * magnetPower;


			}


	/*		magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes" && item.GetComponent<CubesJoint>().isActive)
					if(
						//	item.GetComponent<SpringJoint>()!=null && 
						item.GetComponent<SpringJoint>().connectedBody==null &&
						(item.transform.position-transform.position).magnitude<colliderRadius
						)
						return true;
				else
					return false;
				else
					return false;
			});

			foreach (var item in magnetZone) 
			{
				item.GetComponent<CubesJoint>().collideCube (gameObject.GetComponent<Rigidbody>());
			}


			/*			for (int i=0; i<magnetZone.Length; i++) 
			{
				if ( magnetZone [i].gameObject.tag == "Cubes" && (magnetZone [i].gameObject.GetComponent<SpringJoint>().spring==0)) 
				{
					direction = (this.transform.position - magnetZone [i].transform.position).normalized;
//					magnetZone [i].gameObject.GetComponent<Rigidbody> ().velocity = direction * magnetPower;
					magnetZone [i].gameObject.GetComponent<Rigidbody> ().AddForce (direction * magnetPower);
					Debug.Log ("Add Force to"+magnetZone[i].gameObject.name);
				}
			}*/
		}
		
	}


}