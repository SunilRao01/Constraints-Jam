using UnityEngine;
using System.Collections;

public class Heroine : MonoBehaviour 
{
	private GameObject shooter;
	private Transform lookTarget;
	public GameObject bulletPrefab;
	public float bulletForce;

	private bool waiting;
	public float shootingInterval;

	public bool phase1;
	public bool phase2;
	public bool phase3;

	public float phase3MinWaitTime;
	public float phase3MaxWaitTime;

	public float phase3MinRotationForce;
	public float phase3MaxRotationForce;

	public float phase3JumpForce;

	// Use this for initialization
	void Start () 
	{
		shooter = transform.GetChild(0).GetChild(0).gameObject;

		if (phase2)
		{
			StartCoroutine(waitThenShoot());
		}
		else if (phase3)
		{
			StartCoroutine(randomWaitThenJump());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (phase1)
		{
			if (lookTarget)
			{
				shooter.transform.up = lookTarget.position - shooter.transform.position;
			}
		}
	}

	IEnumerator waitThenShoot()
	{
		while (true)
		{
			yield return new WaitForSeconds(shootingInterval);

			GameObject bullet = (GameObject) Instantiate(bulletPrefab, shooter.transform.GetChild(0).position, Quaternion.identity);
			bullet.transform.rotation = shooter.transform.rotation;

			Vector2 bulletForceDirection = transform.GetChild(0).up;
			bulletForceDirection *= bulletForce;
			bullet.GetComponent<Rigidbody2D>().AddForce(bulletForceDirection);
		}
	}

	IEnumerator randomWaitThenJump()
	{
		while (true)
		{
			float waitTime = Random.Range(phase3MinWaitTime, phase3MaxWaitTime);
			yield return new WaitForSeconds(waitTime);

			// Make heroine jump up
			transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(Vector2.up * phase3JumpForce);

			// Make heroine rotate with a random force
			float rotationForce = Random.Range(-phase3MinRotationForce, phase3MaxRotationForce);
			transform.GetChild(0).GetComponent<Rigidbody2D>().AddTorque(rotationForce);

			// Shoot bullets out of each end
			GameObject bullet_1 = (GameObject) Instantiate(bulletPrefab, shooter.transform.GetChild(0).position, Quaternion.identity);
			bullet_1.transform.rotation = shooter.transform.rotation;
			GameObject bullet_2 = (GameObject) Instantiate(bulletPrefab, shooter.transform.GetChild(1).position, Quaternion.identity);
			bullet_2.transform.rotation = Quaternion.Inverse(shooter.transform.rotation);

			Vector2 bulletForceDirection_1 = shooter.transform.up;
			bulletForceDirection_1 *= bulletForce;
			bullet_1.GetComponent<Rigidbody2D>().AddForce(bulletForceDirection_1);

			Vector2 bulletForceDirection_2 = -shooter.transform.up;
			bulletForceDirection_2 *= bulletForce;
			bullet_2.GetComponent<Rigidbody2D>().AddForce(bulletForceDirection_2);
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
		if (phase1)
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
}
