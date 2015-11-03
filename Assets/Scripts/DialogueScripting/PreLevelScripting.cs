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

		// Set current phase according ot last scene from PlayerPrefs
		switch (PlayerPrefs.GetString("LastScene"))
		{
		case "Intro":
			currentPhase = 0;
			preLevelDialogueIndex = 0;
			break;
		case "Game_1":
			currentPhase = 1;
			postLevelDialogueIndex = 0;
			break;
		case "Talking":
			// TODO Handle phase count for prelevel dialogue
			// TODO: Handle preLevelDialogueIndex
		case "Game_2":
			currentPhase = 3;
			postLevelDialogueIndex = 1;
			break;
		case "Game_3":
			currentPhase = 5;
			postLevelDialogueIndex = 2;
			break;

		}

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
		// TODO: Implement start of prelevel dialogue of player
		else if (currentPhase % 2 == 0)
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
		// TODO: Implement start of postlevel dialogue of heroine
		else
		{
			// Spawn heroine
			currentHeroine = (GameObject) Instantiate(heroines[postLevelDialogueIndex]);

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
			// TODO: Start heroine dialogue
			// TODO: Set proper callback for DialogueCallback
			/*
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
			*/
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
		if (currentPhase % 2 == 0)
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
			for (int i = 0; i < heroines[postLevelDialogueIndex].GetComponent<HeroineDialogue>().postFightDialogue.Count; i++)
			{
				dialogueText.GetComponent<Dialogue>().dialogueList.Add(heroines[postLevelDialogueIndex].GetComponent<HeroineDialogue>().postFightDialogue[i]);
			}

			dialogueText.GetComponent<Dialogue>().startDialogue();
		}
	}
}
