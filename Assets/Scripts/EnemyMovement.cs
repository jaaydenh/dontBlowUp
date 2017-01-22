using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	//public Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public float offset;
	public float forwardSpeed;

	private float targetManeuver;
	private Rigidbody2D rigidBody;

	private float xpos;

	void Start ()
	{
		rigidBody = GetComponent <Rigidbody2D> ();
		//currentSpeed = GetComponent<Rigidbody>().velocity.z;
		//StartCoroutine(Evade());
		xpos = 25;
	}

	void Update () {
		MoveEnemyOnXAxis();
	}

	void MoveEnemyOnXAxis()
	{
		transform.position += new Vector3(Time.deltaTime * forwardSpeed, 0, 0);
	}

	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (-10, 10) * -Mathf.Sign (transform.position.z);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}

	void FixedUpdate ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			
			//xpos = Mathf.MoveTowards (xpos, 5, forwardSpeed * Time.deltaTime);
			xpos = 5;
			rigidBody.position = new Vector3(rigidBody.position.x, Mathf.Lerp(transform.position.y, player.GetComponent<Rigidbody2D> ().position.y + offset, Time.deltaTime * smoothing));
			//rigidBody.position = new Vector3(xpos, 0, Mathf.Lerp(transform.position.y, player.GetComponent<Rigidbody2D> ().position.y + offset, Time.deltaTime * smoothing));
		}
		//float xpos = Mathf.Lerp(transform.position.x, player.transform.position.x, forwardSpeed);
		//transform.position = new Vector3 (xpos, 0, transform.position.z); 

		//float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody>().velocity.x, 10, smoothing * Time.deltaTime);
		//GetComponent<Rigidbody>().velocity = new Vector3 (newManeuver, 0.0f, forwardSpeed);

		//GetComponent<Rigidbody> ().velocity = new Vector3 (0.0f, 0.0f, player.GetComponent<Rigidbody> ().position.z);
		//		float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody>().velocity.z, targetManeuver, smoothing * Time.deltaTime);
		//		GetComponent<Rigidbody>().velocity = new Vector3 (0.0f, 0.0f, -10);


//		GetComponent<Rigidbody2D>().position = new Vector3
//			(
//				Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
//				0.0f, 
//				Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.zMin, boundary.zMax)
//			);


		//GetComponent<Rigidbody2D>().rotation = Quaternion.Euler (0, 0, GetComponent<Rigidbody2D>().velocity.z * -tilt);
	}
}

