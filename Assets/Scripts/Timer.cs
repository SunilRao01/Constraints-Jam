using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	// TODO: Count down timer
	// TODO: Go to 'Talking' Scene after timer reaches 0

	private float timer = 5;



	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer > 0)
		{
			GetComponent<Text>().text = timer.ToString("F1");;
		}
		else
		{
			Application.LoadLevel("Talking");
		}
	}
}
