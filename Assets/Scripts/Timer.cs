using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	private float timer = 15;
	private bool fading = false;
	private bool startingTimer = false;

	void Awake()
	{
		int currentPhase = PlayerPrefs.GetInt("Phase");
		GetComponent<Text>().text = timer.ToString("F1");
		Debug.Log("Current phase: " + currentPhase.ToString());
	}

	public void startTimer()
	{
		startingTimer = true;
	}

	void Update () 
	{
		if (startingTimer)
		{
			timer -= Time.deltaTime;

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
}
