using UnityEngine;
using System.Collections;

public class HeroineBullet : MonoBehaviour 
{
	public Transform target;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(selfDestruct());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			transform.up = -(transform.position - target.position);
		}
	}

	IEnumerator selfDestruct()
	{
		yield return new WaitForSeconds(3);

		Destroy (gameObject);
	}
}
