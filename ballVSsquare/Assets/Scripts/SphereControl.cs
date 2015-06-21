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

	void Start () {
		power = gameObject.GetComponent<Rigidbody> ();
		HorizontalForce = new Vector3 (forcePower, 0.0f, 0.0f);
		VerticalForce = new Vector3 (0.0f, 0.0f, forcePower);	
	}
	

	void Update () {
		float key;
		if ((key = Input.GetAxis("Horizontal")) != 0) 
		{
			power.AddForce (HorizontalForce*key);
		}
		if ((key = Input.GetAxis("Vertical")) != 0) 
		{
			power.AddForce (VerticalForce*key);
		}


	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "gate") 
			Destroy (col.gameObject);
	}
	void FixedUpdate()
	{
		if (isMagnet == true) {

			Collider[] magnetZone = Physics.OverlapSphere (this.transform.position, magnetRadius);

			for (int i=0; i<magnetZone.Length; i++) {
				if ( magnetZone [i].gameObject.tag == "Cubes" ) {
					Vector3 direction = new Vector3 ();
					direction = this.transform.position - magnetZone [i].transform.position;
					magnetZone [i].gameObject.GetComponent<Rigidbody> ().AddForce (direction * magnetPower);
				}
			}
		}

	}
}
