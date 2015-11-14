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
	public GameObject heartPrefab;
	private List<GameObject> hearts;
	private int heartIndex;

	private bool rotate;
	private Vector3 rotationDirection;
	public float rotationSpeed;
	public bool lockMovement;
	public int numOfHearts;

	void Awake () 
	{
		// Retrive relevant variables
		rigidbody = GetComponent<Rigidbody2D>();

		int currentPhase = PlayerPrefs.GetInt("Phase");

		hearts = new List<GameObject>();
		numOfHearts = 0;
		if (currentPhase < 3)
		{
			numOfHearts = 3;
		}
		else if (currentPhase < 6)
		{
			numOfHearts = 4;
		}
		else if (currentPhase < 9)
		{
			numOfHearts = 5;
		}

		for (int i = 0; i < numOfHearts; i++)
		{
			Vector3 heartPosition = heartPrefab.transform.position;
			heartPosition.x += (1.4f * i);
			
			Vector3 heartRotation = heartPrefab.transform.rotation.eulerAngles;
			heartRotation.y += 180;
			
			GameObject currentHeart = (GameObject) Instantiate(heartPrefab, heartPosition, Quaternion.Euler(heartRotation));
			hearts.Add(currentHeart);
		}
		heartIndex = hearts.Count;
		Debug.Log("Done adding hearts! Heart count: " + hearts.Count);
	}

	public List<GameObject> getHearts()
	{
		return hearts;
	}

	void Update () 
	{
		if (!lockMovement)
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
	}

	void damage()
	{
		if (heartIndex == 1)
		{
			Application.LoadLevel("GameOver");
		}
		else if (heartIndex != hearts.Count)
		{
			Color changeLineColor = hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor;
			changeLineColor.a = 0;
			hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor = changeLineColor;
		}

		heartIndex--;
		Debug.Log("Heart Index: " + heartIndex);
		Color newLineColor = hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor;
		newLineColor.a = 0;
		hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor = newLineColor;
		

		if (!healing)
		{
			StartCoroutine(healingHeart());
		}
	}

	public void makeNewHeartVisible()
	{
		Debug.Log("Making new heart visible!");
		StartCoroutine(newHeart());
	}

	IEnumerator newHeart()
	{
		while (hearts[numOfHearts-1].transform.GetChild(1).GetComponent<Wireframe>().lineColor.a < 1)
		{
			Color newHeartColor = hearts[numOfHearts-1].transform.GetChild(1).GetComponent<Wireframe>().lineColor;
			newHeartColor.a += 0.1f;

			hearts[numOfHearts-1].transform.GetChild(1).GetComponent<Wireframe>().lineColor = newHeartColor;

			yield return new WaitForSeconds(0.1f);
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
				Color newLineColor = hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor;
				newLineColor.a += 0.08f;
				hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor = newLineColor;

				if (hearts[heartIndex].transform.GetChild(1).GetComponent<Wireframe>().lineColor.a >= 1)
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
