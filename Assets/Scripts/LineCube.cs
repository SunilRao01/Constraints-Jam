using UnityEngine;
using System.Collections;

public class LineCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Main"), "time", 5.0,
		                                      "easetype", iTween.EaseType.linear));
	}
}
