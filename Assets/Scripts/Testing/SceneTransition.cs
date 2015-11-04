using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		PlayerPrefs.SetInt("Phase", 1);

		Application.LoadLevel("Dodge_1");
	}
}
