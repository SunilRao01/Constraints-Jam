using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float movementForce;
	public float maximumVelocity;

	private Rigidbody2D rigidbody;
	private bool onAxis;

	// Use this for initialization
	void Awake () 
	{
		// Retrive relevant vairables
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 movementDirection = new Vector2();
		movementDirection.x = Input.GetAxisRaw("Horizontal");
		//movementDirection.y = Input.GetAxisRaw("Vertical");
		movementDirection *= movementForce;

		if (rigidbody.velocity.magnitude < maximumVelocity)
		{
			rigidbody.AddForce(movementDirection);
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PlayerMovementAxis")
		{
			onAxis = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "PlayerMovementAxis")
		{
			onAxis = false;
		}
	}
	*/
}
