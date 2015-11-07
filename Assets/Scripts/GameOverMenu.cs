using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour 
{
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(PlayerPrefs.GetString("LastScene"));
		}
	}
}
