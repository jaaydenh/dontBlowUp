using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float playerStartPosition = 2.0f;
	public Transform Player;

	void Start () {
        cameraZ = transform.position.z;
	}

    float cameraZ;

	void Update () {

		transform.position = new Vector3(Player.position.x + playerStartPosition, 0, cameraZ);
	}
}
