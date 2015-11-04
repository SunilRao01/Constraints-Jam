using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
	// Dialogue Box
	private string currentDialogue;
	
	// Variables
	public bool complete;
	
	// Scrolling Text Effect
	public float letterPause = 0.1f;
	public float dialogueInterval;
	
	private string dialogueText;

	public List<string> dialogueList;
	private int iterator;
	public string afterDialogueNextScene;

	public bool isDialogueCallbackFunction;
	public GameObject dialogueCallbackObject;

	void Start ()
	{
		iterator = 0;
		
		complete = false;
	}
	
	void Update ()
	{
		// Always keep the dialogue text updated
		GetComponent<Text>().text = dialogueText;
	}
	
	public void startDialogue()
	{
		dialogueText = "";
		iterator = 0;

		// Starting dialogue
		currentDialogue = dialogueList[iterator];
		StartCoroutine(TypeText());
	}
	
	IEnumerator TypeText ()
	{
		// Display the text on a dialogue box letter-by-letter using a retro "blip" sound effect
		while (iterator < dialogueList.Count)
		{
			dialogueText = "";
			currentDialogue = dialogueList[iterator];

			foreach (char letter in currentDialogue.ToCharArray())
			{
				dialogueText += letter;
				
				if (GetComponent<AudioSource>())
				{
					GetComponent<AudioSource>().Play();
				}
				
				// Wait a bit before displaying the next letter
				yield return new WaitForSeconds (letterPause);
				
			}
			
			// Wait so the user can read the sentence
			yield return new WaitForSeconds(dialogueInterval);
			
			iterator++;
		}

		if (isDialogueCallbackFunction)
		{
			dialogueCallbackObject.GetComponent<DialogueCallback>().dialogueCallback();
		}
		else if (afterDialogueNextScene != "")
		{
			Application.LoadLevel(afterDialogueNextScene);
		}
	}
}