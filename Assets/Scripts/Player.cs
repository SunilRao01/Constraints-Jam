using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	[Header("Movement")]
	public float movementForce;
	public float maximumVelocity;
	public float jumpForce;

	private Rigidbody2D rigidbody;
	private bool onAxis;

	private bool healing;
	public float healingInterval;

	[Header("Player Stats")]
	public Text healthLabel;
	public List<GameObject> hearts;
	private int heartIndex;

	private bool rotate;
	private Vector3 rotationDirection;
	public float rotationSpeed;

	// TODO: Fix player janky ass jumping

	void Awake () 
	{
		// Retrive relevant variables
		rigidbody = GetComponent<Rigidbody2D>();

		heartIndex = hearts.Count;
	}
	
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

		if (rotate)
		{
			transform.Rotate(rotationDirection * rotationSpeed);
		}
	}

	void damage()
	{
		heartIndex--;

		if (heartIndex == 0)
		{
			Application.LoadLevel("GameOver");
		}

		Color newLineColor = hearts[heartIndex].GetComponent<Wireframe>().lineColor;
		newLineColor.a = 0;
		hearts[heartIndex].GetComponent<Wireframe>().lineColor = newLineColor;
		

		if (!healing)
		{
			StartCoroutine(healingHeart());
		}
	}

	IEnumerator healingHeart()
	{
		healing = true;

		while (heartIndex < hearts.Count)
		{
			yield return new WaitForSeconds(healingInterval);

			if (hearts[heartIndex])
			{
				Color newLineColor = hearts[heartIndex].GetComponent<Wireframe>().lineColor;
				newLineColor.a += 0.08f;
				hearts[heartIndex].GetComponent<Wireframe>().lineColor = newLineColor;

				if (hearts[heartIndex].GetComponent<Wireframe>().lineColor.a >= 1)
				{
					heartIndex++;
				}
			}
			else
			{
				Application.LoadLevel("GameOver");
			}
		}

		healing = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PlayerMovementAxis")
		{
			onAxis = true;
			rotate = false;
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
			rotationDirection = new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
			rotate = true;
		}
	}
}
