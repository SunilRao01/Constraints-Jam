using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyASCII : MonoBehaviour 
{
	public GameObject textPrefab;
	public List<string> words;
	private List<GameObject> wordText;

	private int currentPhase = 1;
	public float letterDistance;
	private Timer timer;
	private HeroineBody heroineBody;

	void Start () 
	{
		heroineBody = GameObject.FindGameObjectWithTag("Heroine").transform.GetChild(0).GetComponent<HeroineBody>();
		timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
		wordText = new List<GameObject>();

		string firstWord = words[currentPhase-1];
		for (int i = 0; i < firstWord.Length; i++)
		{
			// Create letter
			GameObject currentLetter = (GameObject) Instantiate(textPrefab);

			// Set letter's corresponding letter
			currentLetter.GetComponent<Text>().text = "" + firstWord[i];

			// Set parent as this object
			currentLetter.transform.parent = transform;

			// Set letter's scale
			Vector3 letterScale = new Vector3(0.5f, 0.5f, 0.5f);

			// Set letter's position
			Vector3 letterPosition = textPrefab.transform.localPosition;
			letterPosition.y = 260;
			letterPosition.z = -5;
			if (firstWord.Length % 2 == 0)
			{

				if (i == ((firstWord.Length/2)-1))
				{
					letterPosition.x -= ((firstWord.Length/2) - i) * (letterDistance/1.5f);
				}
				else if (i == (firstWord.Length/2))
				{
					letterPosition.x += ((i+1) - (firstWord.Length/2)) * (letterDistance/1.5f);
				}
				else if (i < (firstWord.Length/2))
				{
					letterPosition.x -= ((firstWord.Length/2) - i) * letterDistance;
				}
				// Right
				else
				{
					letterPosition.x += ((i+1) - (firstWord.Length/2)) * letterDistance;
				}
			}
			else
			{
				// Left
				if (i < ((firstWord.Length/2) - 1))
				{
					letterPosition.x -= ((firstWord.Length/2) - (i)) * letterDistance;
				}
				// Center
				else if (i == ((firstWord.Length/2) ))
				{
					letterPosition.x = 0;
				}
				// Right
				else
				{
					letterPosition.x += ((i) - (firstWord.Length/2)) * letterDistance;
				}
			}
			currentLetter.transform.localPosition = letterPosition;
			currentLetter.transform.localScale = letterScale;

			// Add letter to list
			wordText.Add(currentLetter);
		}

		StartCoroutine(newWord());
	}
	
	public void startShooting()
	{
		for (int i = 0; i < wordText.Count; i++)
		{
			StartCoroutine(wordText[i].GetComponent<EnemyASCIISpawner>().waitThenSpawn());
			
		}
	}

	IEnumerator newWord()
	{
		// TODO: Stop heroine movement
		// TODO: Stop enemy spawning
		// TODO: Stop timer
		// TODO: Update wordText list with next word

		// Enable flashing colors
		foreach (GameObject g in wordText)
		{
			g.GetComponent<BlendColors>().enabled = true;
		}

		yield return new WaitForSeconds(5.0f);

		foreach (GameObject g in wordText)
		{
			g.GetComponent<BlendColors>().enabled = false;
		}

		timer.startTimer();

		// TODO: Start heroine movement
		heroineBody.startMovement();

		startShooting();
	}

	public void changePhase()
	{
		currentPhase++;

		// TODO: Set up word text
	}
}
