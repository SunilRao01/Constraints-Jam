using UnityEngine;
using System.Collections;

public class BackgroundAsciiEffect : MonoBehaviour 
{
	public GameObject letterPrefab;
	public int letterAmount;
	public float xSpacing;
	public float ySpacing;
	// Use this for initialization
	void Start () 
	{
		// Initialize all letters
		string allChars = "qwertyuiopasdfghjklzxcvbnm[]{}|;:',<.>/?1234567890-=!@#$%^&*()+`~";


		for (int i = 0; i < letterAmount; i++)
		{
			for (int j = 0; j < letterAmount; j++)
			{
				Vector3 letterPosition = new Vector3((i)*xSpacing, 1+(j)*-ySpacing, 50);
				GameObject letter = (GameObject) Instantiate(letterPrefab, letterPosition, Quaternion.identity);
				letter.GetComponent<GUIText>().text = allChars[Random.Range(0, allChars.Length)].ToString();
				//letter.transform.SetParent(transform);
				letter.GetComponent<BlendColors>().introDelay = i*0.2f;
				letter.GetComponent<BlendColors>().startBlend();
			}
		}
	}
	
	void Update () 
	{
		
	}
}
