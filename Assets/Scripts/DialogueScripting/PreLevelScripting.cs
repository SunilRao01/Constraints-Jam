﻿using UnityEngine;
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
	
	// Dialogue
	public List<string> preLevelPlayerDialogue;
	public List<string> postLevelPlayerDialogue;
	public List<GameObject> heroines;
	public int preLevelDialogueIndex;
	public int postLevelDialogueIndex;
	private GameObject currentHeroine;
	
	void Start () 
	{
		// Retrieve player object
		player = GameObject.FindGameObjectWithTag("Player");
		dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
		dialogueText = GameObject.FindGameObjectWithTag("DialogueText");

		currentPhase = PlayerPrefs.GetInt("Phase");
		switch (currentPhase)
		{
			case 2:
				postLevelDialogueIndex = 0;
				break;
			case 3:
				preLevelDialogueIndex = 1;
				break;
			case 5:
				postLevelDialogueIndex = 1;
				break;
			case 6:
				preLevelDialogueIndex = 2;
				break;
			case 8:
				postLevelDialogueIndex = 2;
				break;
			case 9:
				preLevelDialogueIndex = 0;
				postLevelDialogueIndex = 0;
				break;
			default:
				break;
		}

		Debug.Log("Current phase: " + PlayerPrefs.GetInt("Phase"));

		// If phase 0 (after intro), start player at top and make him rotate
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
		// Implement start of prelevel dialogue of player
		else if (currentPhase % 2 != 0)
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
		// Implement start of postlevel dialogue of heroine
		else
		{

			// TODO: Spawn CORRECT heroine (probably depending on PlayerPrefs value)
			// Spawn heroine
			currentHeroine = (GameObject) Instantiate(heroines[postLevelDialogueIndex]);

			// Make dialogue box visible
			Color newDialogueColor = dialogueBox.GetComponent<Image>().color;
			newDialogueColor.a = 1;
			dialogueBox.GetComponent<Image>().color = newDialogueColor;

			// Set dialogue box position to heroine's
			Vector3 startingDialoguePosition = dialogueBox.transform.parent.position;
			startingDialoguePosition.y += 5;
			dialogueBox.transform.position = startingDialoguePosition;
			
			// iTween scale dialogue box
			Vector3 targetScale = new Vector3(2.5f, 2.5f, 2.5f);
			iTween.ScaleTo(dialogueBox, iTween.Hash("scale", targetScale, "time", 0.5f,
			                                        "easetype", iTween.EaseType.linear,
			                                        "oncompletetarget", gameObject,
			                                        "oncomplete", "afterDialogueScale"));
		}

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
		if (currentPhase == 0)
		{
			dialogueText.GetComponent<Dialogue>().dialogueList.Add("Shit.");

			dialogueText.GetComponent<Dialogue>().startDialogue();
		}
		else if (currentPhase % 2 != 0)
		{
			// Set up next level scene for Dialogue script
			string[] currentDialogues = preLevelPlayerDialogue[preLevelDialogueIndex].Split(new char[]{'|'});
		
			for (int i = 0; i < currentDialogues.Length; i++)
			{
				dialogueText.GetComponent<Dialogue>().dialogueList.Add(currentDialogues[i]);
			}

			dialogueText.GetComponent<Dialogue>().startDialogue();
		}
		else
		{
			// TODO: Get dialogue from CORRECT heroine
			for (int i = 0; i < heroines[postLevelDialogueIndex].GetComponent<HeroineDialogue>().postFightDialogue.Count; i++)
			{
				dialogueText.GetComponent<Dialogue>().dialogueList.Add(heroines[postLevelDialogueIndex].GetComponent<HeroineDialogue>().postFightDialogue[i]);
			}

			GetComponent<DialogueCallback>().currentHeroine = currentHeroine;
			dialogueText.GetComponent<Dialogue>().startDialogue();
		}
	}
}
