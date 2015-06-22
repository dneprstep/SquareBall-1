using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereControl : MonoBehaviour {
	
	public Rigidbody power;
	public float forcePower;
	public float magnetPower;
	public float magnetRadius;
	public Vector3 HorizontalForce;
	public Vector3 VerticalForce;
	public bool isMagnet;
	public float explosivePower;
	public float explosiveRadius;
	public float turnOffMagnetTime;
	
	Collider [] magnetZone;
	Vector3 explosivePos;
	Rigidbody rb;
	Vector3 direction;
	
	void Start () {
		power = gameObject.GetComponent<Rigidbody> ();
		HorizontalForce = new Vector3 (forcePower, 0.0f, 0.0f);
		VerticalForce = new Vector3 (0.0f, 0.0f, forcePower);	
		direction = new Vector3 ();

	}
	
	
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.M))
		{
			isMagnet=false;
			magnetZone = Physics.OverlapSphere(transform.position,magnetRadius);
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes" && item.GetComponent<SpringJoint>().connectedBody==gameObject.GetComponent<Rigidbody>())
					return true;
				else
					return false;
			});


			foreach (var item in magnetZone) 
			{
					item.attachedRigidbody.useGravity=true;
					item.GetComponent<SpringJoint>().spring=0;
			}
			// TURN ON Sphere magnet til variable turnOffMagnetTime
			Invoke ("TurnOnMagnet", turnOffMagnetTime);
		}
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
		
		float key;
		if ((key = Input.GetAxis("Horizontal")) != 0) 
		{
			power.AddForce (HorizontalForce*key);
		}
		if ((key = Input.GetAxis("Vertical")) != 0) 
		{
			power.AddForce (VerticalForce*key);
		}
		
		if ((key = Input.GetAxis ("Jump")) != 0) 
		{
			
			magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);
			
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
				item.attachedRigidbody.AddExplosionForce(explosivePower,this.gameObject.transform.position,explosiveRadius);
			}
		}
		
		
		
		if (isMagnet == true) 
		{
			
			magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);
			
			
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.GetComponent<SpringJoint>())
					if(item.gameObject.tag=="Cubes" && item.GetComponent<SpringJoint>().spring==0)
						return true;
				else
					return false;
				else
					return false;
			});
			
			foreach (var item in magnetZone) {
				direction=(transform.position-item.transform.position).normalized;
				item.gameObject.GetComponent<Rigidbody>().velocity=direction * magnetPower;
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