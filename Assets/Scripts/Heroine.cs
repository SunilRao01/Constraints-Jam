using UnityEngine;
using System.Collections;

public class Heroine : MonoBehaviour 
{
	private GameObject shooter;
	private Transform lookTarget;
	public GameObject bulletPrefab;
	public float bulletForce;

	// Use this for initialization
	void Start () 
	{
		shooter = transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lookTarget)
		{
			shooter.transform.up = lookTarget.position - shooter.transform.position;
		}
	}

	public void resetAimer()
	{
		lookTarget.position = new Vector2(0, 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			// TODO: Look at enemy, shoot bullet (that will hit), unfocus enemy
			lookTarget = other.transform;

			GameObject bullet = (GameObject) Instantiate(bulletPrefab, shooter.transform.GetChild(0).position, Quaternion.identity);
			bullet.GetComponent<HeroineBullet>().target = lookTarget;

			Vector2 bulletForceDirection = lookTarget.position - bullet.transform.position;
			bulletForceDirection *= bulletForce;
			bullet.GetComponent<Rigidbody2D>().AddForce(bulletForceDirection);
		}
	}
}
