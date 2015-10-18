using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshTracer : MonoBehaviour 
{
	public float traceTime;
	public GameObject modelToTrace;
	public Mesh meshToTrace;
	public float sizeMultiplier;
	// Use this for initialization
	public Vector3 positionalDifference;
	void Start () 
	{
		// Put on path
		//iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Main"), "time", 5.0,
		 //                                     "easetype", iTween.EaseType.linear));

		// Clear any existing nodes
		//GetComponent<iTweenPath>().nodes.Clear();



		// TODO: Add iTween nodes from Mesh.vertices (returns a vector3)
		for (int i = 0; i < modelToTrace.GetComponent<MeshFilter>().mesh.vertices.Length; i++)
		{
			Debug.Log("Added vertex to nodes...");
			GetComponent<iTweenPath>().nodes.Add((modelToTrace.GetComponent<MeshFilter>().mesh.vertices[i]+positionalDifference) * sizeMultiplier);
		}

		// TODO: Put WireframePen on newly formed path
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Main"), "time", traceTime,
		                                      "easetype", iTween.EaseType.linear));
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
