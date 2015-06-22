using UnityEngine;
using System.Collections;

public class testJoint : MonoBehaviour {

	SpringJoint hJoint;
	// Use this for initialization
	void Start () {
		hJoint = gameObject.AddComponent<SpringJoint> ();
		hJoint.connectedBody = GameObject.FindWithTag ("Player").gameObject.GetComponent<Rigidbody> ();
		hJoint.spring = 10;
		hJoint.maxDistance = 0.5f;
		hJoint.autoConfigureConnectedAnchor = false;
//		hJoint.anchor = GameObject.FindWithTag ("Player").transform.position;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{

	}
}
