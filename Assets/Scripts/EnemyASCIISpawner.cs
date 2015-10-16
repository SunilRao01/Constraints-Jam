using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyASCIISpawner : MonoBehaviour 
{
	public GameObject asciiPrefab;
	public float spawnInterval;
	public float asciiForce;

	private GameObject asciiParent;

	// Use this for initialization
	void Start () 
	{
		asciiParent = GameObject.Find("WorldCanvas");
		StartCoroutine(waitThenSpawn());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	IEnumerator waitThenSpawn()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);

			GameObject ascii = (GameObject) Instantiate(asciiPrefab, transform.position, Quaternion.identity);
			string possibleCharacters = "qwertyuiopasdfghjkl;zxcvbnm,.!@#$$%^&*()-+=";
			ascii.GetComponent<Text>().text = "" + possibleCharacters[Random.Range(0, possibleCharacters.Length-1)]; 
			ascii.transform.parent = asciiParent.transform;
			Vector2 asciiShootPosition = new Vector2(Random.Range(-90, 90), 0);
			ascii.transform.localScale = new Vector3(1, 1, 1);
			ascii.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * asciiForce);
		}
	}
}
