using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ShotDestroyer : MonoBehaviour
{
	void OnTriggerEnter(Collider col) {


	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Shot")
			Destroy(col.gameObject); //free up some memory
	}
}

