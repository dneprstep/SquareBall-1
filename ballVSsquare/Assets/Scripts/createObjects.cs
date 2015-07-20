using UnityEngine;
using System.Collections;

public class createObjects : MonoBehaviour 
{

	public GameObject cloneObject;
	public int max;
	public float spawnTime;
	public Vector3 spawnPosition;


	// Use this for initialization
	void Start () 
	{
	//	spawnPosition = new Vector3 (-1, 350, -850);
	//	cloneObject = GameObject.FindWithTag ("Cubes").gameObject;
		for (int i = 0; i < max; i++) 
		{

			GameObject newClone = Instantiate(cloneObject,new Vector3(spawnPosition.x,spawnPosition.y+(i*0.2f),spawnPosition.z+(i*0.2f)),Quaternion.identity) as GameObject;

		}
	
	}
	

}
