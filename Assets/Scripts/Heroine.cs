﻿using UnityEngine;
using System.Collections;

public class Heroine : MonoBehaviour 
{
	public GameObject shooter;
	private Transform lookTarget;
	public GameObject bulletPrefab;
	public float bulletForce;

	private bool waiting;
	public float shootingInterval;

	// Use this for initialization
	void Start () 
	{
		shooter = transform.GetChild(0).GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lookTarget)
		{
			shooter.transform.up = lookTarget.position - shooter.transform.position;
		}
	}

	IEnumerator waitBeforeShooting()
	{
		waiting = true;
		yield return new WaitForSeconds(shootingInterval);
		waiting = false;
	}

	public void resetAimer()
	{
		lookTarget.position = new Vector2(0, 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy") && !waiting)
		{
			// TODO: Look at enemy, shoot bullet (that will hit), unfocus enemy
			lookTarget = other.transform;

			GameObject bullet = (GameObject) Instantiate(bulletPrefab, shooter.transform.GetChild(0).position, Quaternion.identity);
			bullet.GetComponent<HeroineBullet>().target = lookTarget;

			Vector2 bulletForceDirection = lookTarget.position - bullet.transform.position;
			bulletForceDirection *= bulletForce;
			bullet.GetComponent<Rigidbody2D>().AddForce(bulletForceDirection);

			StartCoroutine(waitBeforeShooting());
		}
	}
}