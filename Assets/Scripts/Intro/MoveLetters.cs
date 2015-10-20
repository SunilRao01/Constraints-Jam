using UnityEngine;
using System.Collections;

public class MoveLetters : MonoBehaviour 
{
	private float movementSpeed = 0.005f;
	//private float movementSpeedIncreaseRate = 0.0001f;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(moveLetters());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.localPosition.y >= 0.01f)
		{
			// TODO: Randomize character
			string allChars = "qwertyuiopasdfghjklzxcvbnm[]{}|;:',<.>/?1234567890-=!@#$%^&*()+`~";
			GetComponent<GUIText>().text = allChars[Random.Range(0, allChars.Length)].ToString();

			// TODO: Move letter down below (set y to -1)
			Vector3 newPosition = transform.localPosition;
			newPosition.y = -1;
			transform.localPosition = newPosition;
		}
	}

	IEnumerator moveLetters()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			// TODO: Move letter up
			Vector3 newLetterPosition = transform.localPosition;
			newLetterPosition.y += movementSpeed;
			transform.localPosition = newLetterPosition;

			// TODO: Increase speed
		}
	}
}
