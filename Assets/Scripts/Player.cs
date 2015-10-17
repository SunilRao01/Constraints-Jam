using UnityEngine;
using UnityEngine.UI;
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

	private bool healing;
	public float healingInterval;

	[Header("Player Stats")]
	public int health = 5;
	public Text healthLabel;

	// TODO: Fix player janky ass jumping

	// Use this for initialization
	void Awake () 
	{
		// Retrive relevant variables
		rigidbody = GetComponent<Rigidbody2D>();

		// Set relevant variables
		healthLabel.text = health.ToString();
	}
	
	// Update is called once per frame
	void Update () 
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
			movementDirection.y += jumpForce;
			rigidbody.AddForce(movementDirection);
		}
	}

	void damage()
	{
		health--;

		if (health <= 0)
		{
			Application.LoadLevel("GameOver");
		}

		// Set relevant variables
		healthLabel.text = health.ToString();

		if (!healing)
		{
			StartCoroutine(healingHeart());
		}
	}

	IEnumerator healingHeart()
	{
		healing = true;

		while (health < 5)
		{
			yield return new WaitForSeconds(healingInterval);
			health++;
			healthLabel.text = health.ToString();
		}

		healing = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PlayerMovementAxis")
		{
			onAxis = true;
		}

		if (other.CompareTag("Enemy"))
		{
			damage();

			Destroy(other.gameObject);
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
