using UnityEngine;
using System.Collections;

public class MeshesCombine : MonoBehaviour {

	private 
	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent<MeshFilter> ();



		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		int i = 0;
		while (i < meshFilters.Length) {
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.active = false;
			i++;
		}
		transform.GetComponent<MeshFilter>().mesh = new Mesh();
		transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
		gameObject.AddComponent<MeshCollider> ().sharedMesh=transform.GetComponent<MeshFilter>().mesh;
		transform.gameObject.active = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
