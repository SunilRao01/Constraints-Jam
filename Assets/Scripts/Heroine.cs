using UnityEngine;
using System.Collections;

public class Heroine : MonoBehaviour 
{
	private GameObject shooter;
	public GameObject lookTarget;

	// Use this for initialization
	void Start () 
	{
		shooter = transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		shooter.transform.up = lookTarget.transform.position - shooter.transform.position;
	}
}
