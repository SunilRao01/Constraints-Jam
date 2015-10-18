using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RealTimeLines : MonoBehaviour 
{
	public List<Vector3> vertexList;

	private int deleteCounter;
	public int deleteTime;
	public Color lineColor;
	
	void Start () 
	{
		vertexList.Add(transform.position);
		StartCoroutine(drawLines());
	}

	public void deleteLines()
	{
		vertexList.Clear();
	}
	
	void OnRenderObject()
	{
		GL.Begin(GL.LINES);
		GL.Color(lineColor);
		{
			for (int i = 0; i < vertexList.Count-1; i++)
			{
				GL.Vertex(vertexList[i]);
				GL.Vertex(vertexList[i+1]);
			}
		}
		GL.End();
	}

	IEnumerator drawLines()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.05f);
			vertexList.Add(transform.position);

			deleteCounter++;
			if (deleteCounter % deleteTime == 0)
			{
				vertexList.RemoveAt(0);
			}
		}
	}
}