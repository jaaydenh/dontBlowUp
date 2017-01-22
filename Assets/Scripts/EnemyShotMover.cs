using UnityEngine;
using System.Collections;

public class EnemyShotMover : MonoBehaviour {

	public float speed;

	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
	}
}
