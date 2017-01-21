using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public float XSpeed = 1;
	private GameController gameController;

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
	}

	void Update () {
		MoveEnemyOnXAxis();
	}

	void MoveEnemyOnXAxis()
	{
		transform.position += new Vector3(Time.deltaTime * XSpeed, 0, 0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log("OnTriggerEnter Enemy");
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("EnemyShot"))
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (this.CompareTag("Enemy")) {
			Debug.Log ("Enemy Destroyed");
			gameController.EnemyDestroyed ();
		}
		//gameController.AddScore(scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}

