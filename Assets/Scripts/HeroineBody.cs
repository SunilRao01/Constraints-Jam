using UnityEngine;
using System.Collections;

public class HeroineBody : MonoBehaviour 
{
	// TODO: UGH how about actually adding an AI for the movement you dense fuck
	// TODO: Also maybe try not to forget you still need to animate the background
	// ASCII you bitch?

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector2.up * 0.1f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Application.LoadLevel("GameOver");
		}
	}
}
