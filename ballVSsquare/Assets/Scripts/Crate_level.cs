using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crate_level : MonoBehaviour {
	public GameObject roadPrefab;
	public GameObject enemyPrefab;
	public GameObject cubePrefab;


	public Vector3 startPosition;
	public float angle;
	public int roadCount;

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


	void Start () 
	{
		road = new List<GameObject> ();

		Vector3 tempPosition=startPosition;

		road.Add ((GameObject)Instantiate (roadPrefab, tempPosition, Quaternion.identity));

		for (int i=0; i<roadCount; i++) {
			tempPosition = AddRoad (roadPrefab, tempPosition, angle, directionVariables.setVar (rDirection.right));
	//		Instantiate (enemyPrefab, tempPosition+(new Vector3(0,30,0)), Quaternion.identity);
		}

		tempPosition.y -= 2.5f;


		for (int i=0; i<roadCount; i++) {
			tempPosition = AddRoad (roadPrefab, tempPosition, angle, directionVariables.setVar (rDirection.rightDown));
		}

		for (int i=0; i<roadCount; i++) {
			tempPosition = AddRoad (roadPrefab, tempPosition, angle, directionVariables.setVar (rDirection.right));
		}

/*		for (int i=0; i<roadCount; i++) {
			tempPosition = AddRoad (roadPrefab, tempPosition, angle, directionVariables.setVar (rDirection.rightUp));
		}
*/

	}
	private Vector3 AddRoad (GameObject prefab, Vector3 position, float angle, directionVariables direction)
	{
		Vector3 tempPosition=position;	
		directionVariables road_dir = direction;
		
		float distance = 0;
		float height = 0;

		Bounds Objectbound;

		road.Add ((GameObject)Instantiate (prefab, tempPosition, Quaternion.Euler (angle*road_dir.angle,0,0)));

			Objectbound=road[road.Count-1].GetComponentInChildren<Renderer>().bounds;

			//figure lenght			
			distance=Objectbound.max.z-Objectbound.min.z;
			//figure height
			height=Objectbound.max.y-Objectbound.min.y;

			
			tempPosition.z+=distance*road_dir.z;
			tempPosition.y+=height*road_dir.y;



		return tempPosition;
	}

/*	private directionVariables RoadDirection(rDirection direction)
	{
		directionVariables dirResult=new directionVariables();
			switch (direction) 
			{
			case rDirection.rightUp:
				dirResult.z=1;
				dirResult.y=1;
				dirResult.angle=-1;
				break;
			case rDirection.right:
				dirResult.z=1;
				dirResult.y=1;
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
				dirResult.y=1;
				dirResult.angle=0;
				break;
			case rDirection.leftDown:
				dirResult.z=-1;
				dirResult.y=-1;
				dirResult.angle=1;
				break;

			default:
			break;
			}

		return dirResult;
	}*/
	
}
