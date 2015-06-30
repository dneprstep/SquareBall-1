using UnityEngine;
using System.Collections;

public class create_enemy : MonoBehaviour {

	public GameObject[] connectedObject;
	FixedJoint[] joint;
	Rigidbody [] rBodies;
	public bool isStun;
	Vector3 direction;
	public float explForce;
	public float explRadius;
	// Use this for initialization
	void Start () 
	{
		direction = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collider)
	{

		if (collider.gameObject.tag == "Player") 
		{
			rBodies = gameObject.GetComponentsInChildren<Rigidbody> ();
			foreach (var item in rBodies) 
			{
				Debug.Log(item.gameObject.name);
				if(!item.GetComponent<create_enemy>().isStun)
				{
					item.GetComponent<create_enemy>().isStun=true;
					item.useGravity = true;
					direction=(transform.position-item.transform.position).normalized;
					Debug.Log ("direction:"+direction);
//					item.AddForce(direction*explForce);
				}
			}
			rBodies = gameObject.GetComponentsInParent<Rigidbody> ();
			foreach (var item in rBodies) 
			{
				Debug.Log(item.gameObject.name);
				if(!item.GetComponent<create_enemy>().isStun)
				{
					item.GetComponent<create_enemy>().isStun=true;
					item.useGravity = true;
					direction=(transform.position-item.transform.position);
					Debug.Log ("direction:"+direction);
	//				item.AddForce(direction*explForce);
				}
			}

		}
	}
}
