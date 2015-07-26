using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crate_level : MonoBehaviour {
	public GameObject[] roadPrefab;
	public GameObject[] environmentPrefab;
	public GameObject[] bonusesPrefab;

	public GameObject checkPointPrefab;
	public GameObject finishPrefab;
	public GameObject cubePrefab;
	



	public Vector3 startPosition;
	public float angle;
	public int roadCount;
	public int cubesCount;

	List<GameObject> road;

	enum rDirection 
	{
		rightUp,
		right,
		rightDown,
		leftUp,
		left,
		leftDown
	}

	struct directionVariables
	{
		public int z;
		public int y;
		public int angle;

		public static directionVariables setVar (rDirection direction)
		{
			directionVariables dirResult = new directionVariables ();
			switch (direction) 
			{
			case rDirection.rightUp:
				dirResult.z=1;
				dirResult.y=1;
				dirResult.angle=-1;
				break;
			case rDirection.right:
				dirResult.z=1;
				dirResult.y=0;
				dirResult.angle=0;
				break;
			case rDirection.rightDown:
				dirResult.z=1;
				dirResult.y=-1;
				dirResult.angle=1;
				break;
			case rDirection.leftUp:
				dirResult.z=-1;
				dirResult.y=1;
				dirResult.angle=-1;
				break;
			case rDirection.left:
				dirResult.z=-1;
				dirResult.y=0;
				dirResult.angle=0;
				break;
			case rDirection.leftDown:
				dirResult.z=-1;
				dirResult.y=-1;
				dirResult.angle=1;
				break;
				
			default:
				dirResult.z=0;
				dirResult.y=0;
				dirResult.angle=0;
				Debug.Log ("Wrong direction");
				break;
			}

			return dirResult;
		}
	}

	float XCuberandom;
	float YCubemax;
	float ZCuberandom;

	void Start () 
	{
		Vector3 tempPosition=startPosition;
		road = new List<GameObject> ();
		tempPosition = AddRoad (roadPrefab[0], tempPosition, angle, directionVariables.setVar (rDirection.right));

		XCuberandom=road[road.Count-1].GetComponentInChildren<Collider>().bounds.extents.x-3f;
		YCubemax=road[road.Count-1].GetComponentInChildren<Collider>().bounds.max.y;
		ZCuberandom=road[road.Count-1].GetComponentInChildren<Collider>().bounds.extents.z;


		for (int i=0; i<roadCount; i++) 
		{
			tempPosition = AddRoad (roadPrefab[Random.Range (0,roadPrefab.Length-1)], tempPosition, angle, directionVariables.setVar (rDirection.right));
			if(i%4==0)
			{
				tempPosition = AddRoad (roadPrefab[0], tempPosition, angle, directionVariables.setVar (rDirection.right));
				for(int j=0;j<cubesCount;j++)
				{
					addCubes (tempPosition);
				}
			}
			if(i%2==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle* Random.Range(-15,15);
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+5,tempPosition.z+tempEnemyPos2.y);
				GameObject temp=(GameObject) Instantiate (environmentPrefab[Random.Range (0,environmentPrefab.Length-1)], tempEnemyPos3, Quaternion.Euler (0,Random.rotation.eulerAngles.y,0));
			}
			if(i%1==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle* Random.Range(-(XCuberandom-3),(XCuberandom-3));
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+2f,tempPosition.z+tempEnemyPos2.y);
				GameObject temp=(GameObject) Instantiate (bonusesPrefab[Random.Range (0,bonusesPrefab.Length-1)], tempEnemyPos3, Quaternion.Euler (0,90,0));
			}
			if(i%10==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle* Random.Range(-25,25);
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y-1,tempPosition.z+tempEnemyPos2.y);
				Instantiate (checkPointPrefab, tempEnemyPos3, Quaternion.identity);
			}
			for(int j=0;j<cubesCount;j++)
			{
				addCubes (tempPosition);
			}
		}

		Instantiate (finishPrefab, tempPosition, Quaternion.identity);
		tempPosition = AddRoad (roadPrefab[0], tempPosition, angle, directionVariables.setVar (rDirection.right));
		AddRoad (roadPrefab[0], tempPosition, angle, directionVariables.setVar (rDirection.right));

	}


	private void addCubes(Vector3 position)
	{
		Vector3 tempCubePosition=new Vector3(Random.Range (-XCuberandom,XCuberandom), YCubemax, position.z + Random.Range (-ZCuberandom,ZCuberandom ));
		
		GameObject tempCube =(GameObject) Instantiate (cubePrefab, tempCubePosition, Quaternion.identity);
	}


	private Vector3 AddRoad (GameObject prefab, Vector3 position, float angle, directionVariables direction)
	{
		Vector3 tempPosition=position;	
		directionVariables road_dir = direction;
		
		float distance = 0;
		float height = 0;

		Bounds Objectbound;

		road.Add ((GameObject)Instantiate (prefab, tempPosition, Quaternion.Euler (angle*road_dir.angle,0,0)));

			Objectbound=road[road.Count-1].GetComponentInChildren<Collider>().bounds;

			//figure lenght			
			distance=Objectbound.max.z-Objectbound.min.z;
			//figure height
			height=Objectbound.max.y-Objectbound.min.y;

			
			tempPosition.z+=distance*road_dir.z;
			tempPosition.y+=height*road_dir.y;


		return tempPosition;
	}


}
