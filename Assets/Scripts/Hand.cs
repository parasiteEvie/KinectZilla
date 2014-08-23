using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Hand:MonoBehaviour {
	public bool leftHand;
	public Transform target;

	private Vector3 startPos;
	private Vector3 deltaToTargetPos;

	private Sequence seq;

	// Init
	public void Awake() {
		startPos = transform.position;
		deltaToTargetPos = target.position - startPos;

		seq = new Sequence();
		seq.Append(HOTween.To(transform, 1f, "position", deltaToTargetPos, true, EaseType.EaseOutBounce, 0f));
		seq.Append(HOTween.To(transform, 1f, "position", -deltaToTargetPos, true, EaseType.EaseInOutCubic, 0f));
		seq.autoKillOnComplete = false;
	}

	// Input
	public void Update() {
		if(leftHand) {
			if(Input.GetKeyUp(KeyCode.A)) {
				HandAttack();
			}
		} else {
			if(Input.GetKeyUp(KeyCode.D)) {
				HandAttack();
			}
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
