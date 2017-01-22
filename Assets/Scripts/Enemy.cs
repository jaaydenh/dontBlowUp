using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	private ParticleSystem explosion1;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		explosion1 = GetComponent<ParticleSystem>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("EnemyShot"))
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (this.CompareTag("Enemy")) {
			
			explosion1.Play();
			StartCoroutine(DestroyEnemy(other));
		}
	}

	IEnumerator DestroyEnemy(Collider2D other) {

		yield return new WaitForSeconds(0.01f);
		Destroy (other.gameObject);
		Destroy (gameObject);
		gameController.EnemyDestroyed ();
	}
}
