using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Hand:MonoBehaviour {
	public Transform followTarget;

	const float ATTACK_Y_POS = 0f;
	const float RECOVER_Y_POS = 5f;

	private Vector3 startScale;

	private Vector3 startPos;
	private Vector3 deltaPos;
	private Vector3 targetPos;
	private Vector3 pos;

	private bool attacking = false;
	private bool recovered = true;

	public BodyPartControls bpc;
	private Animator anim;

	// Init
	public void Awake() {
		startPos = transform.position;
		startScale = transform.localScale;
		if(bpc == null) {
			bpc = GetComponent<BodyPartControls>();
		}
		anim = GetComponent<Animator>();
	}

	// Input
	public void Update() {
		pos = transform.position;

		// Attack
		if(pos.y < ATTACK_Y_POS && ! attacking && recovered) {
			if(bpc.bodyPartClosed) {
				HandAttack();
			} else {
				recovered = false;
			}
		} else if(! recovered && pos.y > RECOVER_Y_POS && ! attacking) {
			recovered = true;
			collider.enabled = true;
		}

		// Follow
		if(! attacking) {
			// Move towards follow target
			deltaPos = Vector3.ClampMagnitude(followTarget.position - pos, 10f);
			// Z adjustment
			if(! recovered && pos.z < 30f) {
				deltaPos.z = Time.deltaTime * 300f;
			} else if(recovered && pos.z > 20f) { 
				deltaPos.z = -Time.deltaTime * 300f;
			} else {
				deltaPos.z = 0f;
			}
			// Pre-adjust
			if(deltaPos.magnitude > 1f) {
				pos += deltaPos * Time.deltaTime * 5f;
			}
			// Check bounds
			if(pos.x > 23f) {
				pos.x = 23f;
			}
			if(pos.x < -23f) {
				pos.x = -23f;
			}
			if(pos.y < -10f) {
				pos.y = -10f;
			}
			if(pos.y > 18f) {
				pos.y = 18f;
			}
			// Adjust
			transform.position = pos;
		}

		// Hand animation
		if( bpc.bodyPartClosed) {
			anim.Play("fistanim");
		} else {
			anim.Play("handanim");
		}
	}

	// Attack!
	private void HandAttack() {
		attacking = true;
		recovered = false;
		targetPos = transform.position;
		targetPos.y = -8f;
		targetPos.z = startPos.z;
		HOTween.To(transform, 1f, new TweenParms().Prop("position", targetPos).Ease(EaseType.EaseOutBounce).OnComplete(DoneHandAttack));
	}

	private void DoneHandAttack() {
		attacking = false;
		collider.enabled = false;
	}
}
