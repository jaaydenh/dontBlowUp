using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FlyAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private int pointerID;
	private bool canFly;

	public void OnPointerDown (PointerEventData data) {
		pointerID = data.pointerId;
		canFly = true;
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			canFly = false;
		}
	}

	public bool CanFly () {
		return canFly;
	}
}
