using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerIntro : MonoBehaviour 
{
	public Vector3 rotationDirection;
	public float rotationForce;
	public Blanket mainBlanket;

	public List<GameObject> phaseOneEffects;
	public List<GameObject> phaseTwoEffects;
	public List<GameObject> phaseThreeEffects;
	public List<GameObject> phaseFourEffects;

	void Start()
	{
		StartCoroutine(rotatingCube());
		StartCoroutine(waitBeforeEffects());
	}

	// Update is called once per frame
	void Update () 
	{
		//transform.Rotate(rotationDirection * rotationForce);
	}

	IEnumerator rotatingCube()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			transform.Rotate(rotationDirection * rotationForce);
		}
	}

	IEnumerator waitBeforeEffects()
	{
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < phaseOneEffects.Count; i++)
		{
			phaseOneEffects[i].GetComponent<AsistEffectsCubes>().comeDown();
		}
	}
}
