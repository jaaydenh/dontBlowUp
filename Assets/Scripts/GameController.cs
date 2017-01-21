using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject enemy;
	public Vector3 spawnValues;
	public int asteroidCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public AudioClip BackgroundMusicAudioClip;

	public Transform Player;

	//public GUIText scoreText;
	//public GUIText gameOverText;
	//public GameObject restartButton;

	//private bool gameOver;
//	private bool restart;
	private int score;
	private int enemyDestroyedCount;

	void Start () {

		GetComponent<AudioSource>().Play();

		//gameOver = false;
		//restart = true;
	//	restartButton.SetActive (true);
		//gameOverText.text = "";
		score = 0;
		enemyDestroyedCount = 0;
		UpdateScore ();
		StartCoroutine (SpawnEnemy ());
	}

//	void Update ()
//	{
//		if (restart)
//		{
//			if (Input.GetKeyDown (KeyCode.R))
//			{
//				RestartGame();
//			}
//		}
//		if (gameOver)
//		{
//			StartCoroutine(ReloadGame ());
//		}
//	}

	IEnumerator SpawnAsteroids () {
		yield return new WaitForSeconds (startWait);

		//while (true) {
		for (int i = 0; i < asteroidCount; i++) {
			Vector3 spawnPosition = new Vector3 (spawnValues.x, spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (hazard, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(spawnWait);
		}
		//}
	}

	IEnumerator SpawnEnemy () {
		yield return new WaitForSeconds(spawnWait);
		float xPos = Player.position.x + 5.0f;
		Vector3 position = new Vector3 (xPos, 0, 0);
		Quaternion spawnRotation = Quaternion.identity;
		//Instantiate (enemy, position, spawnRotation);
	}

	public void EnemyDestroyed () {
		enemyDestroyedCount++;

		//if (enemyDestroyedCount == 3) {
		//	StartCoroutine (SpawnAsteroids ());
		//}  else {
			StartCoroutine (SpawnEnemy ());
		//}
	}

//	IEnumerator ReloadGame () {
//		yield return new WaitForSeconds(3);
//		SceneManager.LoadScene ("Main");
//	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		//scoreText.text = "Score: " + score;
	}

//	public void GameOver ()
//	{
//		gameOverText.text = "Game Over!";
//		gameOver = true;
//	}
//
//	public void RestartGame () {
//		StartCoroutine (SpawnEnemyShip ());
//		restartButton.SetActive (false);
//		restart = false;
//	}
}

