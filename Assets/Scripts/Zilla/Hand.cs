using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Hand:MonoBehaviour {
	public bool leftHand;
	public Transform followTarget;

	const float RECOVER_Y_POS = 5f;

	private Vector3 startScale;

	private Vector3 startPos;
	private Vector3 deltaPos;
	private Vector3 targetPos;
	float targetZ;

	private bool attacking = false;
	private bool recovered = true;

	// Init
	public void Awake() {
		startPos = transform.position;
		startScale = transform.localScale;
		targetZ = startPos.z;
	}

	// Input
	public void Update() {
		// Attack
		if(transform.position.y < RECOVER_Y_POS && ! attacking && recovered) {
			HandAttack();
		} else if(! recovered && transform.position.y > RECOVER_Y_POS && ! attacking) {
			recovered = true;
			collider.enabled = true;
		}

		// Follow
		if(! attacking) {
			// Move towards follow target
			deltaPos = Vector3.ClampMagnitude(followTarget.position - transform.position, 10f);
			// Z adjustment
			if(! recovered && transform.position.z < 30f) {
				deltaPos.z = Time.deltaTime * 300f;
			} else if(recovered && transform.position.z > 20f) { 
				deltaPos.z = -Time.deltaTime * 300f;
			} else {
				deltaPos.z = 0f;
			}
			// Adjust!
			if(deltaPos.magnitude > 1f) {
				transform.position += deltaPos * Time.deltaTime * 5f;
			}
		}
	}

	// Attack!
	private void HandAttack() {
		attacking = true;
		recovered = false;
		targetPos = transform.position;
		targetPos.y = -8f;
		HOTween.To(transform, 1f, new TweenParms().Prop("position", targetPos).Ease(EaseType.EaseOutBounce).OnComplete(DoneHandAttack));
	}

	private void DoneHandAttack() {
		attacking = false;
		collider.enabled = false;
	}
}
