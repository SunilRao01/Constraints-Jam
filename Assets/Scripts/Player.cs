using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	[Header("Movement")]
	public float movementForce;
	public float maximumVelocity;
	public float jumpForce;
	//public float jumpingMultiplier;
	//public float jumpingGravity;

	private Rigidbody2D rigidbody;
	private bool onAxis;

	// TODO: Fix player janky ass jumping

	// Use this for initialization
	void Awake () 
	{
		// Retrive relevant vairables
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector2 movementDirection = new Vector2();
		movementDirection.x = Input.GetAxisRaw("Horizontal");
		movementDirection.y = 0;
		movementDirection.Normalize();
		movementDirection *= movementForce;

		if (rigidbody.velocity.magnitude < maximumVelocity)
		{
			rigidbody.AddForce(movementDirection);
		}

		if (Input.GetKeyDown(KeyCode.Space) && onAxis)
		{
			movementDirection.y = jumpForce;
			rigidbody.AddForce(movementDirection);
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PlayerMovementAxis")
		{
			onAxis = true;

			//Vector2 newVelocity = rigidbody.velocity;
			//newVelocity.y = 0;
			//rigidbody.velocity = newVelocity;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "PlayerMovementAxis")
		{
			onAxis = false;
		}
	}
}
