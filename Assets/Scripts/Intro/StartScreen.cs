using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StartScreen : MonoBehaviour 
{
	public Text instructions;
	public List<Text> startKeys;

	private bool enablePressKeys;
	private bool wPressed;
	private bool aPressed;
	private bool sPressed;
	private bool dPressed;
	private bool spacePressed;
	
	void Start () 
	{

	}

	void Update () 
	{
		if (enablePressKeys)
		{
			handleKeys();
		}
	}

	public void enableKeys()
	{
		Color newColor = Color.white;
		newColor.a = 1;

		instructions.color = newColor;

		for (int i = 0; i < startKeys.Count; i++)
		{
			startKeys[i].color = newColor;
		}

		enablePressKeys = true;
	}

	void handleKeys()
	{
		if (wPressed && aPressed && sPressed && dPressed && spacePressed)
		{
			GetComponent<PreLevelScripting>().nextPhase();
			Application.LoadLevel("Talking");
		}

		if (Input.GetKeyDown(KeyCode.W) && !wPressed)
		{
			startKeys[0].GetComponent<BlendColors>().enabled = true;
			wPressed = true;
		}
		if (Input.GetKeyDown(KeyCode.A) && !aPressed)
		{
			startKeys[1].GetComponent<BlendColors>().enabled = true;
			aPressed = true;
		}
		if (Input.GetKeyDown(KeyCode.S) && !sPressed)
		{
			startKeys[2].GetComponent<BlendColors>().enabled = true;
			sPressed = true;
		}
		if (Input.GetKeyDown(KeyCode.D) && !dPressed)
		{
			startKeys[3].GetComponent<BlendColors>().enabled = true;
			dPressed = true;
		}

		if (Input.GetKeyDown(KeyCode.Space) && !spacePressed)
		{
			startKeys[4].GetComponent<BlendColors>().enabled = true;
			spacePressed = true;
		}
	}
}
