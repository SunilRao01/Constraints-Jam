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

	// Use this for initialization
	void Start () 
	{
		// Start heroine AI
		StartCoroutine(movementAI());
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector2.up * 0.1f);
	}

	IEnumerator movementAI()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			
			// Level 1 AI: Go left and right
			if (stage_1)
			{
				// Set the x position to loop between -3 and 3
				Vector3 newPosition = transform.position;
				newPosition.x = Mathf.Sin(Time.time * 0.5f) * 3;

				transform.position = newPosition;
			}
			// Level 2 AI: Continuously go in a circle
			else if (stage_2)
			{
				
			}
			// Level 3 AI: Jump randomly and shoot at peak, chill with player
			else if (stage_3)
			{
				
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Application.LoadLevel("GameOver");
		}
	}
}
