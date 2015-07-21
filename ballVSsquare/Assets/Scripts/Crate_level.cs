using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crate_level : MonoBehaviour {
	public GameObject roadPrefab;
	public GameObject enemyPrefab;
	public GameObject cubePrefab;
	public GameObject springBoard;


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


	void Start () 
	{

		road = new List<GameObject> ();

		Vector3 tempPosition=startPosition;

		road.Add ((GameObject)Instantiate (roadPrefab, tempPosition, Quaternion.identity));

		for (int i=0; i<roadCount; i++) 
		{
			tempPosition = AddRoad (roadPrefab, tempPosition, angle, directionVariables.setVar (rDirection.right));
			if(i%4==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle*5;
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+2,tempPosition.z+tempEnemyPos2.y);
				Instantiate (enemyPrefab, tempEnemyPos3, Quaternion.Euler (0,Random.rotation.eulerAngles.y,0));
			}
			if(i%6==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle*6;
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+.5f,tempPosition.z+tempEnemyPos2.y);
				Instantiate (springBoard, tempEnemyPos3, Quaternion.Euler (0,-90,0));
			}
			for(int j=0;j<cubesCount;j++)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle*5;
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+.5f,tempPosition.z+tempEnemyPos2.y);
				Instantiate (cubePrefab, tempEnemyPos3, Quaternion.identity);
			}

			
			//			Instantiate (enemyPrefab, tempPosition+(new Vector3(0,30,0)), Quaternion.identity);
		}

		tempPosition.y -= 2.5f;



		for (int i=0; i<roadCount; i++) {
			tempPosition = AddRoad (roadPrefab, tempPosition, angle, directionVariables.setVar (rDirection.rightDown));
			if(i%4==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle*5;
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+2,tempPosition.z+tempEnemyPos2.y);
				Instantiate (enemyPrefab, tempEnemyPos3, Quaternion.identity);
			}
			if(i%6==0)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle*6;
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+.5f,tempPosition.z+tempEnemyPos2.y);
				Instantiate (springBoard, tempEnemyPos3, Quaternion.Euler (angle,-90,0));
			}
			for(int j=0;j<cubesCount;j++)
			{
				Vector2 tempEnemyPos2= Random.insideUnitCircle*5;
				Vector3 tempEnemyPos3=new Vector3(tempEnemyPos2.x,tempPosition.y+.5f,tempPosition.z+tempEnemyPos2.y);
				Instantiate (cubePrefab, tempEnemyPos3, Quaternion.identity);
			}

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
