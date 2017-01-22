using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public float offset;
	public float forwardSpeed;
	private Rigidbody2D rigidBody;
	//private float targetManeuver;

	void Start ()
	{
		rigidBody = GetComponent <Rigidbody2D> ();
	}

	void Update () {
		MoveEnemyOnXAxis();
	}

	void MoveEnemyOnXAxis()
	{
		transform.position += new Vector3(Time.deltaTime * forwardSpeed, 0, 0);
	}

//	IEnumerator Evade ()
//	{
//		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
//		while (true)
//		{
//			targetManeuver = Random.Range (-10, 10) * -Mathf.Sign (transform.position.z);
//			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
//			targetManeuver = 0;
//			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
//		}
//	}

	void FixedUpdate ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			rigidBody.position = new Vector3(rigidBody.position.x, Mathf.Lerp(transform.position.y, 
				player.GetComponent<Rigidbody2D> ().position.y + offset, Time.deltaTime * smoothing));
		}
	}
}
