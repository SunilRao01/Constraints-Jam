using UnityEngine;
using System.Collections;

public class HeroineBody : MonoBehaviour 
{
	// TODO: UGH how about actually adding an AI for the movement you dense fuck
	// TODO: Also maybe try not to forget you still need to animate the background
	// ASCII you bitch?
	public bool stage_1;
	public bool stage_2;
	public bool stage_3;

	private bool starting = false;
	
	// Update is called once per frame
	void Update () 
	{
		if (starting)
		{
			if (stage_1)
			{
				transform.Rotate(Vector2.up * 0.1f);
			}
			else
			{
				StartCoroutine(movementAI());
				starting = false;
			}
		}
	}

	public void startMovement()
	{
		starting = true;
		StartCoroutine(movementAI());
	}

	public void stopMovement()
	{
		starting = false;
		StopCoroutine(movementAI());
	}

	IEnumerator movementAI()
	{
		Debug.Log("Starting heroine movement AI");

		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			
			// Level 1 AI: Go left and right
			if (stage_1 || stage_3)
			{
				// Set the x position to loop between -3 and 3
				Vector3 newPosition = transform.position;
				newPosition.x = Mathf.Sin(Time.time * 0.5f) * 3;

				transform.position = newPosition;
			}
			// Level 2 AI: Continuously go in a circle
			else if (stage_2)
			{
				transform.RotateAround(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0)), Vector3.forward, 0.8f);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			PlayerPrefs.SetString("LastScene", Application.loadedLevelName);
			Application.LoadLevel("GameOver");
		}
	}
}
