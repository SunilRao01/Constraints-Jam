using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyASCIISpawner : MonoBehaviour 
{
	public GameObject asciiPrefab;
	public float minSpawnInterval;
	public float maxSpawnInterval;
	public float asciiForce;

	private GameObject asciiParent;

	public float spawnIntervalScaling;
	private char chosenLetter;
	private bool spawning = true;

	public bool phase1Path1;
	public bool phase1Path2;

	// Use this for initialization
	void Start () 
	{
		asciiParent = GameObject.Find("WorldCanvas");
		chosenLetter = GetComponent<Text>().text.ToCharArray()[0];
	}

	public void stopSpawn()
	{
		spawning = false;
	}

	public IEnumerator waitThenSpawn()
	{
		while (spawning)
		{
			GameObject ascii = (GameObject) Instantiate(asciiPrefab, transform.position, Quaternion.identity);
			ascii.GetComponent<Text>().text = "" + chosenLetter; 
			ascii.transform.SetParent(asciiParent.transform);
			ascii.transform.localScale = new Vector3(1, 1, 1);

			if (phase1Path1)
			{
				// TODO: Add circular path to ascii 
			}
			else if (phase1Path2)
			{
				// TODO: Add lines and shoot characters really fast
			}
			else
			{
				// Add force
				Vector3 asciiShootPosition = new Vector3(Random.Range(-45, 45), -25, 0);
				asciiShootPosition.Normalize();
				asciiShootPosition *= asciiForce;
				ascii.GetComponent<Rigidbody2D>().AddForce(asciiShootPosition);
			}

			minSpawnInterval -= spawnIntervalScaling;
			maxSpawnInterval -= spawnIntervalScaling;

			yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
		}
	}
}
