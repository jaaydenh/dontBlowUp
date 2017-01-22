using UnityEngine;

class ShotDestroyer : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Shot" || col.tag == "EnemyShot")
			Destroy(col.gameObject);
	}
}

