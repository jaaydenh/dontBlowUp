using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ShootAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private bool touched;
	private int pointerID;
	private bool canShoot;

	void Awake () {
		touched = false;
	}		

	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			canShoot = true;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			touched = false;
			canShoot = false;
		}
	}

	public bool CanShoot () {
		return canShoot;
	}
}
