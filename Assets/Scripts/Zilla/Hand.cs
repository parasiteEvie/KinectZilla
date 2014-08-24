using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Hand:MonoBehaviour {
	public bool leftHand;
	public Transform followTarget;

	private Vector3 startScale;

	private Vector3 startPos;
	private Vector3 deltaPos;
	private Vector3 targetPos;

	private bool attacking = false;
	private bool recovered = true;

	// Init
	public void Awake() {
		startPos = transform.position;
		startScale = transform.localScale;
	}

	// Input
	public void Update() {
		// Attack
		if(transform.position.y < 0f && ! attacking && recovered) {
			HandAttack();
		} else if(! recovered && transform.position.y > 0f && ! attacking) {
			recovered = true;
		}

		// Follow
		if(! attacking) {
			deltaPos = Vector3.ClampMagnitude(followTarget.position - transform.position, 5f);
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
		HOTween.To(transform, 0.5f, new TweenParms().Prop("localScale", startScale).Ease(EaseType.EaseInOutCubic));
	}
}
