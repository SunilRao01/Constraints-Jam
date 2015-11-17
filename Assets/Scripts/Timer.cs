using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public float timer = 15.0f;
	public float previousTime = 15.0f;
	private bool fading = false;
	private bool timing = true;

	void Awake()
	{
		int currentPhase = PlayerPrefs.GetInt("Phase");
		GetComponent<Text>().text = timer.ToString("F1");
		Debug.Log("Current phase: " + currentPhase.ToString());
	}

	public void pause()
	{
		timing = false;
	}

	public void resume()
	{
		timing = true;
	}

	void Update () 
	{
		if (timing)
		{
			timer -= Time.deltaTime;
		}

		if (timer > 0)
		{
			GetComponent<Text>().text = timer.ToString("F1");

		}
		else
		{
			if (!fading)
			{
				// Update current phase
				int currentPhase = PlayerPrefs.GetInt("Phase");
				currentPhase++;

				PlayerPrefs.SetInt("Phase", currentPhase);
				fading = true;
			}

			Camera.main.GetComponent<FadeOut>().fade = true;
		}

	}
}
