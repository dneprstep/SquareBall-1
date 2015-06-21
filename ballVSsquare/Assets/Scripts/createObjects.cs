using UnityEngine;
using System.Collections;

public class createObjects : MonoBehaviour {

	public GameObject cloneObject;
	public int max;
	public float spawnTime;
	public Vector3 spawnPosition;
	// Use this for initialization
	void Start () {
	//	cloneObject = GameObject.FindWithTag ("Cubes").gameObject;
		spawnPosition = new Vector3 (-1, 350, -850);

		InvokeRepeating ("CreateObjects", 0, spawnTime);
/*		for (int i = 0; i < max; i++) 
		{
			InvokeRepeating(
			Instantiate(cloneObject,new Vector3(0,350,-900),Quaternion.identity);

		}
*/	
	}
	
	private void CreateObjects()
				{
		Vector3 pos = new Vector3 (spawnPosition.x + Random.Range (-1, 1), spawnPosition.y, spawnPosition.z + Random.Range (-3, 3));
		Instantiate (cloneObject, pos, Quaternion.identity);
	}

	void Update () {
	
	}
}
