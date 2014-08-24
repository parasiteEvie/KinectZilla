using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class ArrowControls:MonoBehaviour {
	public bool isLeftHand;

	private Vector3 vel = Vector3.zero;

	private Vector3 pos;

	public void Update() {
		/*if(isLeftHand) {
			vel.x = Input.GetAxis("HorizontalLeft");
			vel.y = Input.GetAxis("VerticalLeft");
		} else {
			vel.x = Input.GetAxis("HorizontalRight");
			vel.y = Input.GetAxis("VerticalRight");
		}

		pos = transform.position;

		if(pos.x < 25f && pos.x > -25f && pos.y > -13f && pos.y < 21f) {
			rigidbody.velocity = vel * 20f;
		} else {
			rigidbody.velocity = (-transform.position).normalized * 20f;
		}*/
	}

	// Draw Gizmo
	public void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.position, 1f);
	}
}
