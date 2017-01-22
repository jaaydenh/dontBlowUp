using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject enemy;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float enemySpawnOffset;

	public AudioClip BackgroundMusicAudioClip;

	public Transform Player;

	//public GUIText scoreText;

	private int score;
	private int enemyDestroyedCount;

	void Start () {

		GetComponent<AudioSource>().Play();
		score = 0;
		enemyDestroyedCount = 0;
		UpdateScore ();
	}

	public void SpawnNextEncounter() {

		if (enemyDestroyedCount == 3) {
			StartCoroutine (SpawnHazards ());
			enemyDestroyedCount = 0;
		}  else {
			StartCoroutine (SpawnEnemy ());
		}
	}

	public IEnumerator SpawnHazards () {
		yield return new WaitForSeconds (startWait);

		for (int i = 0; i < hazardCount; i++) {
			float xSpawnPosition = Player.position.x + spawnValues.x;
			Vector3 spawnPosition = new Vector3 (xSpawnPosition, Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (hazard, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(spawnWait);
		}
		yield return new WaitForSeconds(waveWait);
		SpawnNextEncounter();
	}

	public IEnumerator SpawnEnemy () {
		yield return new WaitForSeconds(spawnWait);
		float xSpawnPosition = Player.position.x + enemySpawnOffset;
		Vector3 position = new Vector3 (xSpawnPosition, 0, 0);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (enemy, position, spawnRotation);
	}

	public void EnemyDestroyed () {
		enemyDestroyedCount++;
		SpawnNextEncounter();
	}
		
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		//scoreText.text = "Score: " + score;
	}
}
