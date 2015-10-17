using UnityEngine;
using System.Collections;

public class BackgroundAsciiEffect : MonoBehaviour 
{
	public GameObject letterPrefab;

	// Use this for initialization
	void Start () 
	{
		// Initialize all letters
		string allChars = "qwertyuiopasdfghjklzxcvbnm[]{}|;:',<.>/?1234567890-=!@#$%^&*()+`~";


		for (int i = 0; i < 50; i++)
		{
			for (int j = 0; j < 50; j++)
			{
				Vector3 letterPosition = new Vector3((i)*0.02f, 1+(j)*-0.03f, 0);
				GameObject letter = (GameObject) Instantiate(letterPrefab, letterPosition, Quaternion.identity);
				letter.GetComponent<GUIText>().text = allChars[Random.Range(0, allChars.Length)].ToString();
				//letter.transform.SetParent(transform);
			}
		}
	}
	
	void Update () 
	{
		
	}
}
