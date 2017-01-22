using UnityEngine;

class ShotDestroyer : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Shot" || col.tag == "EnemyShot" || col.tag == "Hazard")
			Destroy(col.gameObject);
	}
}

