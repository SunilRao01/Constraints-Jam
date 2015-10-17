using UnityEngine;
using System.Collections;

public class HeroineBody : MonoBehaviour 
{

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
