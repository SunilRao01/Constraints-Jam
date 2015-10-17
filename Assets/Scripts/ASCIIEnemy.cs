using UnityEngine;
using System.Collections;

public class ASCIIEnemy : MonoBehaviour 
{
	private int deathTimer = 8;

	void Start () 
	{
		StartCoroutine(selfDestruct());
	}
	
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("Bullet"))
		{
			Debug.Log("Letter collided with bullet!");
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}

	IEnumerator selfDestruct()
	{
		yield return new WaitForSeconds(deathTimer);

		Destroy (gameObject);
	}
}
