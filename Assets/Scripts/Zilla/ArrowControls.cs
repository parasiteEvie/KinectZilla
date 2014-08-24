using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class ArrowControls:MonoBehaviour {
	public bool isLeftHand;

	private Vector3 vel = Vector3.zero;

	private Vector3 pos;

	public void Update() {
		if(isLeftHand) {
			vel.x = Input.GetAxis("HorizontalLeft");
			vel.y = Input.GetAxis("VerticalLeft");
		} else {
			vel.x = Input.GetAxis("HorizontalRight");
			vel.y = Input.GetAxis("VerticalRight");
		}

		pos = transform.position;

		if(pos.x < 23f && pos.x > -23f && pos.y > -10f && pos.y < 18f) {
			rigidbody.velocity = vel * 10f;
		} else {
			rigidbody.velocity = (-transform.position).normalized * 10f;
		}
	}

	// Draw Gizmo
	public void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.position, 1f);
	}
}
