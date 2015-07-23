using UnityEngine;
using System.Collections;

public class Boosts : MonoBehaviour 
{

	public enum BoostType
	{
		forward=1,
		back,
		right,
		left,
		stop
	};
	public GameObject controlObject;
	public BoostType enumForceDirection;
	public float forcePower;

	Vector3 forceDirection;
	Rigidbody sphereRB;
	SphereControl sphereScript;
	bool isActive;

	public void setBoostDirection(int dir)
	{
		enumForceDirection =(BoostType) dir;
	}


	void Start () 
	{
		controlObject = GameObject.FindWithTag ("Player");
		sphereRB = controlObject.GetComponent<Rigidbody> ();
		sphereScript = controlObject.GetComponent<SphereControl> ();
		forceDirection = Vector3.zero;
		isActive = true;
	}

	void OnTriggerEnter(Collider collision)
	{
		if (isActive && collision.CompareTag ("Player")) 
		{
			isActive=false;
			StartCoroutine (onBoostCollision());
		}
	}
	IEnumerator onBoostCollision()
	{
		yield return null;
		switch (enumForceDirection) 
		{
		case BoostType.forward:
			forceDirection=new Vector3(0,forcePower,forcePower);
			break;
		case BoostType.back:
			forceDirection=new Vector3(0,forcePower,-forcePower);
			break;
		case BoostType.left:
			forceDirection=new Vector3(-forcePower,0,0);
			break;
		case BoostType.right:
			forceDirection=new Vector3(forcePower,0,0);
			break;
		case BoostType.stop:
			sphereScript.isActive=false;
			sphereRB.velocity=Vector3.ClampMagnitude(sphereRB.velocity,0);
			yield return new WaitForSeconds(1f);
			sphereScript.isActive=true;;
			break;
			
		default:
			break;
		}
		sphereRB.AddForce (forceDirection, ForceMode.Acceleration);
	}

}
