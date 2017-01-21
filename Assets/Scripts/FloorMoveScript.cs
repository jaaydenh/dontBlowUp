using UnityEngine;
using System.Collections;

public class FloorMoveScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < -3.5f)	
        {
            transform.localPosition = new Vector3(-3.0f, transform.localPosition.y, transform.localPosition.z);
        }
        transform.Translate(-Time.deltaTime, 0, 0);
    }
}
