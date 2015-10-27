using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PreLevelScripting : MonoBehaviour 
{
	private GameObject player;
	private GameObject dialogueBox;
	private GameObject dialogueText;

	public float waitBeforeDialogue;
	public static int currentPhase;

	// Player rotation (at phase 1)
	private Vector3 rotationDirection;
	private bool rotate;

	public Blanket blanket;
	
	// Dialoge
	public List<string> dialogue;

	void Start () 
	{
		// Retrieve player object
		player = GameObject.FindGameObjectWithTag("Player");
		dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
		dialogueText = GameObject.FindGameObjectWithTag("DialogueText");

		// If phase 1, start player at top and make him rotate
		if (currentPhase == 0)
		{
			// Start player at top
			Vector3 playerStartPosition = player.transform.localPosition;
			playerStartPosition.y = 5.5f;
			player.transform.localPosition = playerStartPosition;
			
			// Set Random rotation as player falls
			rotationDirection = new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
			rotate = true;

			StartCoroutine(rotatePlayer());
			StartCoroutine(waitThenTalk());
		}
		else
		{
			// Make dialogue box visible
			Color newDialogueColor = dialogueBox.GetComponent<Image>().color;
			newDialogueColor.a = 1;
			dialogueBox.GetComponent<Image>().color = newDialogueColor;
			
			// iTween scale dialogue box
			Vector3 targetScale = new Vector3(2.5f, 2.5f, 2.5f);
			iTween.ScaleTo(dialogueBox, iTween.Hash("scale", targetScale, "time", 0.5f,
			                                        "easetype", iTween.EaseType.linear,
			                                        "oncompletetarget", gameObject,
			                                        "oncomplete", "afterDialogueScale"));
		}

	}

	public int getCurrentPhase()
	{
		return currentPhase;
	}

	public void nextPhase()
	{
		currentPhase++;
	}

	IEnumerator rotatePlayer()
	{
		while (rotate)
		{
			yield return new WaitForSeconds(0.01f);
			
			player.transform.Rotate(rotationDirection);
		}
	}

	IEnumerator waitThenTalk()
	{
		yield return new WaitForSeconds(waitBeforeDialogue);

		if (currentPhase == 0)
		{
			rotate = false;
			StopCoroutine(rotatePlayer());
		}
		

		// Make dialogue box visible
		Color newDialogueColor = dialogueBox.GetComponent<Image>().color;
		newDialogueColor.a = 1;
		dialogueBox.GetComponent<Image>().color = newDialogueColor;
		
		// iTween scale dialogue box
		Vector3 targetScale = new Vector3(2.5f, 2.5f, 2.5f);
		iTween.ScaleTo(dialogueBox, iTween.Hash("scale", targetScale, "time", 0.5f,
		                                        "easetype", iTween.EaseType.linear,
		                                        "oncompletetarget", gameObject,
		                                        "oncomplete", "afterDialogueScale"));
	}

	public void afterDialogueScale()
	{
		// Set up next level scene for Dialogue script
		string[] currentDialogues = dialogue[currentPhase].Split(new char[]{'|'});

		for (int i = 0; i < currentDialogues.Length; i++)
		{
			dialogueText.GetComponent<Dialogue>().dialogueList.Add(currentDialogues[i]);
		}

		dialogueText.GetComponent<Dialogue>().startDialogue();
	}
}
