using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueCallback : MonoBehaviour 
{
	private Blanket mainBlanket;
	private Dialogue dialogueText;
	private GameObject dialogueBox;

	public List<GameObject> heroines;
	private bool finishedPreDialogue; // Used to signal when heroine entrance is done talking, switches to player

	private bool waitForInput;
	private int currentPhase;
	public GameObject currentHeroine;

	void Start()
	{
		currentPhase = PlayerPrefs.GetInt("Phase");

		mainBlanket = GameObject.FindGameObjectWithTag("Blanket").GetComponent<Blanket>();
		dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Dialogue>();
		dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
	}

	public void dialogueCallback()
	{
		if (currentPhase == 0)
		{
			mainBlanket.setText("Hearts\n\nA game by Sunil Rao");
			mainBlanket.makeVisible();
			
			GetComponent<StartScreen>().enabled = true;
			GetComponent<StartScreen>().enableKeys();
		}
		// Current phase will be odd, make heroines arrive!
		else if (currentPhase == 2 || currentPhase == 5 || currentPhase == 8)
		{
			int preDialogueIndex = GetComponent<PreLevelScripting>().preLevelDialogueIndex;

			if (!finishedPreDialogue)
			{	
				Vector3 heroineStartingPosition = heroines[preDialogueIndex].transform.position;
				heroineStartingPosition.y += 15;

				if (currentPhase == 8)
				{
					heroineStartingPosition.x -= 0.5f;
				}

				GameObject dialogueHeroine = (GameObject) Instantiate(heroines[preDialogueIndex], heroineStartingPosition, Quaternion.identity);

				if (currentPhase != 8)
				{
					// Move heroine down after instantiating it on top
					iTween.MoveTo(dialogueHeroine, iTween.Hash("position", heroines[preDialogueIndex].transform.position, "time", 5.0f,
					                                           "easetype", iTween.EaseType.linear,
					                                           "oncompletetarget", gameObject,
					                                           "oncomplete", "afterHeroineArrival"));
				}
				else
				{
					StartCoroutine(waitThenHeroineTalk());
				}
			}
			else
			{
				//  Move the dialogue box
				Vector3 targetPosition = dialogueBox.transform.parent.position;

				switch (currentPhase)
				{
				case 2:
					targetPosition.y -= 5;
					break;
				case 5:
					targetPosition.y -= 3;
					break;
				case 8: // TODO: Figure out third heroine entrance
					targetPosition.x += 2;
					break;
				}

				
				iTween.MoveTo(dialogueBox.transform.parent.gameObject, iTween.Hash("position", targetPosition, "time", 0.5f,
				                                                                   "easetype", iTween.EaseType.linear));
				
				// Start player's response dialogue
				dialogueText.GetComponent<Dialogue>().dialogueList.Clear();
				for (int i = 0; i < heroines[preDialogueIndex].GetComponent<HeroineDialogue>().playerResponseDialogue.Count; i++)
				{
					dialogueText.GetComponent<Dialogue>().dialogueList.Add(heroines[preDialogueIndex].GetComponent<HeroineDialogue>().playerResponseDialogue[i]);
				}

				int nextLevelNum = 0;

				switch (currentPhase)
				{
					case 2:
						nextLevelNum = 1;
						break;
					case 5:
						nextLevelNum = 2;
						break;
					case 8:
						nextLevelNum = 3;
						break;
					default:
						nextLevelNum = 1;
						break;
				}

				string nextLevel = "Game_" + nextLevelNum;
				dialogueText.GetComponent<Dialogue>().afterDialogueNextScene = nextLevel;
				dialogueText.GetComponent<Dialogue>().isDialogueCallbackFunction = false;
				dialogueText.GetComponent<Dialogue>().startDialogue();
			}
		}
		// This will be for the callback after the heroine talks, send her back!
		else if (currentPhase % 3 == 0)
		{
			// Move Heroine back up
			Vector3 targetHeroinePosition = currentHeroine.transform.position;
			targetHeroinePosition.y += 10;

			if (currentPhase != 9)
			{
				iTween.MoveTo(currentHeroine, iTween.Hash("position", targetHeroinePosition, "time", 5.0f,
				                                           "easetype", iTween.EaseType.linear,
				                                           "oncompletetarget", gameObject,
				                                           "oncomplete", "afterHeroineLeaves"));
			}
			else
			{
				currentHeroine.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
				StartCoroutine(waitThenHeroineKill());
			}
		}
	}

	IEnumerator waitThenHeroineTalk()
	{
		yield return new WaitForSeconds(3);

		afterHeroineArrival();
	}

	IEnumerator waitThenHeroineKill()
	{
		yield return new WaitForSeconds(2);

		Destroy (currentHeroine);
		afterHeroineLeaves();
	}

	void afterHeroineArrival()
	{
		Debug.Log("After heroine arrival!");
		int currentPhase = PlayerPrefs.GetInt("Phase");
		int preLevelIndex = GetComponent<PreLevelScripting>().preLevelDialogueIndex;

		// Move dialogue box up
		Vector3 targetPosition = dialogueBox.transform.parent.position;
		//targetPosition.y += 5;

		switch (currentPhase)
		{
			case 2:
				targetPosition.y += 5;
				break;
			case 5:
				targetPosition.y += 3;
				break;
			case 8: // TODO: Figure out third heroine entrance
				targetPosition.x -= 2;
				break;
		}
		
		iTween.MoveTo(dialogueBox.transform.parent.gameObject, iTween.Hash("position", targetPosition, "time", 0.5f,
		                                                                   "easetype", iTween.EaseType.linear));

		// Start heroine's dialogue
		dialogueText.GetComponent<Dialogue>().dialogueList.Clear();
		for (int i = 0; i < heroines[preLevelIndex].GetComponent<HeroineDialogue>().preFightDialogue.Count; i++)
		{
			dialogueText.GetComponent<Dialogue>().dialogueList.Add(heroines[preLevelIndex].GetComponent<HeroineDialogue>().preFightDialogue[i]);
		}

		finishedPreDialogue = true;
		dialogueText.GetComponent<Dialogue>().startDialogue();
	}

	void afterHeroineLeaves()
	{
		int currentPhase = PlayerPrefs.GetInt("Phase");

		// Move dialogue box down
		Vector3 targetPosition = dialogueBox.transform.parent.position;

		if (currentPhase != 9)
		{
			targetPosition.y -= 5;
		}
		else
		{
			targetPosition.y -= 1;
		}

		
		iTween.MoveTo(dialogueBox.transform.parent.gameObject, iTween.Hash("position", targetPosition, "time", 0.5f,
		                                                                   "easetype", iTween.EaseType.linear));

		// Start player's ending dialogue
		int postLevelDialogueIndex = GetComponent<PreLevelScripting>().postLevelDialogueIndex;
		string[] currentDialogues = GetComponent<PreLevelScripting>().postLevelPlayerDialogue[postLevelDialogueIndex].Split(new char[]{'|'});

		// THIS MAY BE A PROBLEM, FUCK IF I KNOW
		dialogueText.GetComponent<Dialogue>().dialogueList.Clear();
		for (int i = 0; i < currentDialogues.Length; i++)
		{
			dialogueText.GetComponent<Dialogue>().dialogueList.Add(currentDialogues[i]);
		}

		// Update current scene
		currentPhase++;
		
		PlayerPrefs.SetInt("Phase", currentPhase);

		// Set dialgue finish callback to start next level
		dialogueText.GetComponent<Dialogue>().afterDialogueNextScene = "Dodge_" + ((currentPhase/3) + 1);
		dialogueText.GetComponent<Dialogue>().isDialogueCallbackFunction = false;

		dialogueText.GetComponent<Dialogue>().startDialogue();
	}
}
