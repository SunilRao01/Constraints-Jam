using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blanket : MonoBehaviour 
{
	public void makeVisible()
	{
		Color newImageColor = transform.GetChild(0).GetComponent<Image>().color;
		newImageColor.a = 1;
		transform.GetChild(0).GetComponent<Image>().color = newImageColor;

		Color newTextColor = transform.GetChild(1).GetComponent<Text>().color;
		newTextColor.a = 1;
		transform.GetChild(1).GetComponent<Text>().color = newTextColor;
	}

	public void makeInvisible()
	{
		Color newImageColor = transform.GetChild(0).GetComponent<Image>().color;
		newImageColor.a = 0;
		transform.GetChild(0).GetComponent<Image>().color = newImageColor;
		
		Color newTextColor = transform.GetChild(1).GetComponent<Text>().color;
		newTextColor.a = 0;
		transform.GetChild(1).GetComponent<Text>().color = newTextColor;
	}

	public void setText(string inputText)
	{
		transform.GetChild(1).GetComponent<Text>().text = inputText;
	}
}
