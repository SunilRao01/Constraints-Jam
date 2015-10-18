using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerIntro : MonoBehaviour 
{
	public Vector3 rotationDirection;
	public float rotationForce;
	public Blanket mainBlanket;

	public float phaseInterval;
	public float textInterval;

	public List<GameObject> phaseOneEffects;
	public List<GameObject> phaseTwoEffects;
	public List<GameObject> phaseThreeEffects;
	public List<GameObject> phaseFourEffects;

	void Start()
	{
		StartCoroutine(rotatingCube());
		StartCoroutine(phaseRoutine());
	}

	IEnumerator phaseRoutine()
	{
		yield return new WaitForSeconds(phaseInterval);

		mainBlanket.setText("This is phase 1!");
		mainBlanket.makeVisible();

		yield return new WaitForSeconds(textInterval);

		mainBlanket.makeInvisible();

		for (int i = 0; i < phaseOneEffects.Count; i++)
		{
			phaseOneEffects[i].GetComponent<AsistEffectsCubes>().comeDown();
		}

		yield return new WaitForSeconds(phaseInterval);

		mainBlanket.setText("This is phase 2!");
		mainBlanket.makeVisible();

		yield return new WaitForSeconds(textInterval);

		mainBlanket.makeInvisible();

		for (int i = 0; i < phaseTwoEffects.Count; i++)
		{
			phaseTwoEffects[i].GetComponent<AsistEffectsCubes>().comeDown();
		}

		yield return new WaitForSeconds(phaseInterval);

		mainBlanket.setText("This is phase 3!");
		mainBlanket.makeVisible();

		yield return new WaitForSeconds(textInterval);

		mainBlanket.makeInvisible();
		
		for (int i = 0; i < phaseThreeEffects.Count; i++)
		{
			phaseThreeEffects[i].GetComponent<AsistEffectsCubes>().comeDown();
		}

		yield return new WaitForSeconds(phaseInterval);

		mainBlanket.setText("This is phase 4!");
		mainBlanket.makeVisible();

		yield return new WaitForSeconds(textInterval);

		mainBlanket.makeInvisible();
		
		for (int i = 0; i < phaseFourEffects.Count; i++)
		{
			phaseFourEffects[i].GetComponent<AsistEffectsCubes>().comeDown();
		}

		yield return new WaitForSeconds(phaseInterval);

		// TODO: Load scene after intro
		Application.LoadLevel("Sandbox");
	}

	IEnumerator rotatingCube()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			transform.Rotate(rotationDirection * rotationForce);
		}
	}
}
