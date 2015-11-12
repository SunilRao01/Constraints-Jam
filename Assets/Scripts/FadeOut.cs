using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {
	
	// FadeInOut
	public Texture2D fadeTexture;

	public bool fade = false;

	public float alpha;
	public float alphaSpeed;

	public string nextScene;

	void Update()
	{
		if (fade)
		{
			alpha += alphaSpeed;
		}


		if (alpha >= 1)
		{
			Application.LoadLevel(nextScene);
		}
	}

	void OnGUI()
	{
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		
	}
}
