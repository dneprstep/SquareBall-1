using UnityEngine;
using System.Collections;

public class cube_experement : MonoBehaviour {

	public Rigidbody connectTo;

	private FixedJoint objJoint;
	// Use this for initialization
	void Start () 
	{
		objJoint = gameObject.AddComponent<FixedJoint>();
		objJoint.anchor = new Vector3 (0, 0, 0);
		objJoint.connectedBody = connectTo;
		objJoint.enableCollision = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
