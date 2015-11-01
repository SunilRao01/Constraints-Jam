using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour 
{
	public string gameScene;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(PlayerPrefs.GetString("LastScene"));
		}
	}
}
