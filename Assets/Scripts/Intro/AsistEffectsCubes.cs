using UnityEngine;
using System.Collections;

public class AsistEffectsCubes : MonoBehaviour 
{
	public float rotationSpeed;
	public float comeDownTime;
	public float movementDistance;
	public bool negativeDirection;

	// Use this for initialization
	void Start () 
	{
		GetComponent<MeshRenderer>().enabled = false;

		// Set the position on top, off the screen
		Vector3 newPosition = transform.position;
		newPosition.y += (movementDistance/2);
		transform.localPosition = newPosition;

		StartCoroutine(rotateAround());
	}

	public void comeDown()
	{
		GetComponent<RealTimeLines>().enabled = true;
		/*
		Vector3 newPosition = transform.position;
		newPosition.y -= movementDistance;

		iTween.MoveTo(gameObject, iTween.Hash("position", newPosition, "time", comeDownTime,
		                                      "easetype", iTween.EaseType.linear,
		                                      "islocal", true));*/
	}

	IEnumerator rotateAround()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			if (negativeDirection)
				transform.RotateAround(transform.parent.GetChild(0).position, -Vector3.up, rotationSpeed);
			else
				transform.RotateAround(transform.parent.GetChild(0).position, Vector3.up, rotationSpeed);
		}
	}
}
