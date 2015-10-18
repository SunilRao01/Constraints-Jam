using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BlendColors : MonoBehaviour 
{
	public List<Color> blendColors;
	public float colorChangeRate;

	private SpriteRenderer spriteRenderer;
	private Color currentColor;
	private Color newColor;
	private int colorChoice;
	private float colorCount;

	public bool randomColors;
	public bool customAlpha;
	public float customAlphaValue;

	public bool isText;
	public bool isGuiText;
	public bool isImage;
	public bool isMaterial;
	public bool isLine;

	public float introDelay;
	public bool enableBlend;

	public bool sequential;
	private int sequenceCount;
	public bool customStart;

	public bool startRandomColors;

	public BlendColors()
	{
		randomColors = true;

		blendColors = new List<Color>();
		blendColors.Add(Color.white);

		colorChangeRate = 0.1f;
	}

	void Awake () 
	{
		if (!isText && !isImage && !isGuiText && !isLine)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			currentColor = spriteRenderer.color;
		}
		else if (isImage)
		{
			currentColor = GetComponent<Image>().color;
		}
		else if (isText)
		{
			currentColor = GetComponent<Text>().color;
		}
		else if (isMaterial)
		{
			currentColor = GetComponent<Renderer>().material.color;
		}
		else if (isGuiText)
		{
			currentColor = GetComponent<GUIText>().color;
		}
		else if (isLine)
		{
			currentColor = GetComponent<RealTimeLines>().lineColor;
		}

		if (gameObject.tag == "Block")
		{
			introDelay = transform.localPosition.x + 6; 
		}

		enableBlend = false;

		if (!customStart)
		{
			startBlend();
		}
	}

	public void startBlend()
	{
		StartCoroutine(delay());
	}

	public IEnumerator delay()
	{
		yield return new WaitForSeconds(introDelay);
		enableBlend = true;

	}
	
	void Update () 
	{
		if (enableBlend)
		{
			changeColors();
		}
	}

	void changeColors()
	{
		if (colorCount == 0)
		{
			colorChoice = Random.Range(0, (blendColors.Count));
			newColor = blendColors[colorChoice];

			if (sequential)
			{
				newColor = blendColors[sequenceCount];

				if (sequenceCount == blendColors.Count-1)
				{
					sequenceCount = 0;
				}
				else
				{
					sequenceCount++;
				}
			}

			if (randomColors)
			{
				newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
			}

			if (customAlpha)
			{
				newColor.a = customAlphaValue;
			}

			colorCount += colorChangeRate;
		}
		else if (colorCount > 0 && colorCount < 1)
		{
			colorCount += colorChangeRate;
		}
		else if (colorCount >= 1)
		{
			colorCount = 0;


			if (isImage)
			{
				currentColor = GetComponent<Image>().color;
			}
			else if (isText)
			{
				currentColor = GetComponent<Text>().color;
			}
			else if (isGuiText)
			{
				currentColor = GetComponent<GUIText>().color;
			}
			else if (isLine)
			{
				currentColor = GetComponent<RealTimeLines>().lineColor;
			}
			else
			{
				currentColor = spriteRenderer.color;
			}
		}


		if (isText)
		{
			GetComponent<Text>().color = Color.Lerp(currentColor, newColor, colorCount);
		}
		else if (isImage)
		{
			GetComponent<Image>().color = Color.Lerp(currentColor, newColor, colorCount);
		}
		else if (isGuiText)
		{
			GetComponent<GUIText>().color = Color.Lerp(currentColor, newColor, colorCount);
		}
		else if (isLine)
		{
			GetComponent<RealTimeLines>().lineColor = Color.Lerp(currentColor, newColor, colorCount);
		}
		else
		{
			spriteRenderer.color = Color.Lerp(currentColor, newColor, colorCount);
		}
	}
}
