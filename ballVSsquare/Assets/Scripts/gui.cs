using UnityEngine;
using System.Collections;

public class gui : MonoBehaviour 
{
	public int window;

	Rect barRect;
	Rect timeRect;
	Rect endGameRect;

	GUIStyle barStyle;
	
	float Energy;
	float levelTime;

	SphereControl sphereScript;
	levelControl lvlTimeManager;
	
	void Start () 
	{
		window = 0;
		sphereScript = GameObject.FindWithTag ("Player").GetComponent<SphereControl> ();
		lvlTimeManager = GameObject.FindObjectOfType<levelControl> ();

		barStyle = new GUIStyle ();
		barStyle.normal.background = MakeTex (10,10,new Color(0.1f,0.1f,1,.5f));

		timeRect = new Rect ((Screen.width / 2)+25, 45, 50, 20);
		endGameRect = new Rect (5, 10, Screen.width - 10, Screen.height - 20);
	}
	
	void OnGUI()
	{
		Energy = sphereScript.Energy;
		barRect = new Rect ((Screen.width-Energy) / 2, 10, Energy*2, 20);
		GUI.Box (barRect,"", barStyle);


		GUI.Box (timeRect, lvlTimeManager.currentTime.ToString ());

		if (window == 2) 
		{
			GUI.Box (endGameRect,"GAME OVER");
		}

		if (window == 1) 
		{
			GUI.Box (endGameRect,"FINISH");
		}


	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
	
}
