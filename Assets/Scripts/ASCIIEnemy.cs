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

	IEnumerator selfDestruct()
	{
		yield return new WaitForSeconds(deathTimer);

		Destroy (gameObject);
	}
}
