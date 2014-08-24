using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Hand:MonoBehaviour {
	public bool leftHand;
	public Transform followTarget;
	public Transform hitTarget;

	private Vector3 startPos;
	private Vector3 deltaToTargetPos;

	private Vector3 deltaPos;

	private Sequence seq;

	// Init
	public void Awake() {
		startPos = transform.position;
		deltaToTargetPos = hitTarget.position - startPos;

		seq = new Sequence();
		seq.Append(HOTween.To(transform, 1f, "position", deltaToTargetPos, true, EaseType.EaseOutBounce, 0f));
		seq.Append(HOTween.To(transform, 1f, "position", -deltaToTargetPos, true, EaseType.EaseInOutCubic, 0f));
	}

	// Input
	public void Update() {
		if(leftHand) {
			if(Input.GetKeyUp(KeyCode.Space)) {
				HandAttack();
			}
		} else {
			if(Input.GetKeyUp(KeyCode.Space)) {
				HandAttack();
			}
		}

		// Follow
		deltaPos = Vector3.ClampMagnitude(followTarget.position - transform.position, 5f);
		if(deltaPos.magnitude > 1f) {
			transform.position += deltaPos * Time.deltaTime * 5f;
		}
	}

	// Attack!
	private void HandAttack() {
		if(seq.isPaused) {
			seq.Restart();
			seq.Play();
		}
	}
}
