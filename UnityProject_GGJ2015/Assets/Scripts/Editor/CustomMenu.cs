using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomMenu : Editor {

	[MenuItem("Custom/Prepare Scene")]
	public static void PrepareScene()
	{
		Debug.Log ("Preparing the scene...");

		GameObject[] all = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[];

		foreach(GameObject obj in all)
		{
			Debug.Log ("...C'est bien gentil mais il n'y a rien à faire ici encore");
		}

		Debug.Log ("Done !");
	}
}
