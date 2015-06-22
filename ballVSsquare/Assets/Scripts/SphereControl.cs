using UnityEngine;
using System.Collections;

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


	Rigidbody rb;

	void Start () {
		power = gameObject.GetComponent<Rigidbody> ();
		HorizontalForce = new Vector3 (forcePower, 0.0f, 0.0f);
		VerticalForce = new Vector3 (0.0f, 0.0f, forcePower);	
	}
	

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.M))
		{
			isMagnet=false;
			GameObject[] allCubes =  GameObject.FindGameObjectsWithTag("Cubes");
			foreach (var item in allCubes) {
				Destroy(item.GetComponent<SpringJoint>());
			}
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
<<<<<<< HEAD

			magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);
			
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                                =>
			                                {
				if(item.gameObject.tag=="Cubes")
					return true;
				else
					return false;
			});

			foreach (var item in magnetZone) 
			{
				item.GetComponent<SpringJoint>().connectedBody=null;
				item.GetComponent<SpringJoint>().spring=0;
				item.attachedRigidbody.AddExplosionForce(explosivePower,this.gameObject.transform.position,explosiveRadius);
			}





/*			explosivePos=transform.position;
=======
			Vector3 explosivePos=transform.position;
>>>>>>> parent of 9aa5d42... Magnet
			Collider[] magnetZone = Physics.OverlapSphere (this.transform.position, explosiveRadius);

			foreach (var item in magnetZone)
			{
				if ( item.gameObject.tag == "Cubes")
				{
					rb=item.GetComponent<Rigidbody>();
					rb.AddExplosionForce (explosivePower, explosivePos, explosiveRadius);
					Debug.Log ("Add AddExplosionForce to"+item.gameObject.name);
				}
			}
		
		}



		if (isMagnet == true) {

			Collider[] magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);

<<<<<<< HEAD
			magnetZone=System.Array.FindAll(magnetZone,(Collider item)
			                     =>
			                     {
				if(item.gameObject.tag=="Cubes")
					if(item.GetComponent<SpringJoint>() && item.GetComponent<SpringJoint>().connectedBody==null)
						return true;
					else
						return false;
				else
					return false;
			});

			foreach (var item in magnetZone) {
				direction=(transform.position-item.transform.position).normalized;
				Debug.Log((direction*magnetPower));
				item.gameObject.GetComponent<Rigidbody>().AddForce(direction * magnetPower);
			}

/*			for (int i=0; i<magnetZone.Length; i++) 
			{
				if ( magnetZone [i].gameObject.tag == "Cubes" && (magnetZone [i].gameObject.GetComponent<SpringJoint>().spring==0)) 
=======
			for (int i=0; i<magnetZone.Length; i++) {
				if ( magnetZone [i].gameObject.tag == "Cubes" && !(magnetZone [i].gameObject.GetComponent<SpringJoint>())) 
>>>>>>> parent of 9aa5d42... Magnet
				{
					Vector3 direction = new Vector3 ();

					direction = this.transform.position - magnetZone [i].transform.position;
//					magnetZone [i].gameObject.GetComponent<Rigidbody> ().velocity = direction * magnetPower;
					magnetZone [i].gameObject.GetComponent<Rigidbody> ().AddForce (direction * magnetPower);
					Debug.Log ("Add Force to"+magnetZone[i].gameObject.name);

				}
			}
		}

	}
}
