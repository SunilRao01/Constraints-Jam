using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour {

	public int nextPhase;

	// Use this for initialization
	void Awake () 
	{
		PlayerPrefs.SetInt("Phase", nextPhase);

		Application.LoadLevel("Talking");
	}
}
