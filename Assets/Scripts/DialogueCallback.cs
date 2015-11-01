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
	private bool finishedPreDialogue;
	private bool finishedDialogue;

	private bool waitForInput;

	void Start()
	{
		mainBlanket = GameObject.FindGameObjectWithTag("Blanket").GetComponent<Blanket>();
		dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Dialogue>();
		dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
	}

	public void dialogueCallback()
	{
		int currentPhase = GetComponent<PreLevelScripting>().getCurrentPhase();

		if (currentPhase == 0)
		{
			mainBlanket.setText("Hearts\n\nA game by Sunil Rao");
			mainBlanket.makeVisible();
			
			GetComponent<StartScreen>().enabled = true;
			GetComponent<StartScreen>().enableKeys();
		}
		// TODO: Current phase will be odd, make heroines arrive!
		else if (currentPhase % 2 != 0)
		{
			if (!finishedPreDialogue)
			{	
				Vector3 heroineStartingPosition = heroines[currentPhase-1].transform.position;
				heroineStartingPosition.y += 15;
				GameObject dialogueHeroine = (GameObject) Instantiate(heroines[currentPhase-1], heroineStartingPosition, Quaternion.identity);
				
				// Move heroine down after instantiating it on top
				iTween.MoveTo(dialogueHeroine, iTween.Hash("position", heroines[currentPhase-1].transform.position, "time", 5.0f,
				                                           "easetype", iTween.EaseType.linear,
				                                           "oncompletetarget", gameObject,
				                                           "oncomplete", "afterHeroineArrival"));
			}
			else
			{
				//  Move the dialogue box
				Vector3 targetPosition = dialogueBox.transform.parent.position;
				targetPosition.y -= 5;
				
				iTween.MoveTo(dialogueBox.transform.parent.gameObject, iTween.Hash("position", targetPosition, "time", 0.5f,
				                                                                   "easetype", iTween.EaseType.linear));
				
				// Start player's response dialogue
				dialogueText.GetComponent<Dialogue>().dialogueList.Clear();
				for (int i = 0; i < heroines[currentPhase-1].GetComponent<HeroineDialogue>().playerResponseDialogue.Count; i++)
				{
					dialogueText.GetComponent<Dialogue>().dialogueList.Add(heroines[currentPhase-1].GetComponent<HeroineDialogue>().playerResponseDialogue[i]);
				}

				string nextLevel = "Game_" + currentPhase;
				dialogueText.GetComponent<Dialogue>().afterDialogueNextScene = nextLevel;
				dialogueText.GetComponent<Dialogue>().isDialogueCallbackFunction = false;
				dialogueText.GetComponent<Dialogue>().startDialogue();
			}
		}
		// TODO: This will be for the callback after the levels, make heroines leave!
		else
		{

		}

	}

	void Update()
	{
		if (waitForInput)
		{

		}
	}

	void afterHeroineArrival()
	{
		Debug.Log("After heroine arrival!");
		int currentPhase = GetComponent<PreLevelScripting>().getCurrentPhase();

		// Move dialogue box up
		Vector3 targetPosition = dialogueBox.transform.parent.position;
		targetPosition.y += 5;
		
		iTween.MoveTo(dialogueBox.transform.parent.gameObject, iTween.Hash("position", targetPosition, "time", 0.5f,
		                                                                   "easetype", iTween.EaseType.linear));

		// Start heroine's dialogue
		dialogueText.GetComponent<Dialogue>().dialogueList.Clear();
		for (int i = 0; i < heroines[currentPhase-1].GetComponent<HeroineDialogue>().preFightDialogue.Count; i++)
		{
			dialogueText.GetComponent<Dialogue>().dialogueList.Add(heroines[currentPhase-1].GetComponent<HeroineDialogue>().preFightDialogue[i]);
		}

		finishedPreDialogue = true;
		dialogueText.GetComponent<Dialogue>().startDialogue();
	}
}
