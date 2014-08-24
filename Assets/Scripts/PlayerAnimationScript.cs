using UnityEngine;
using System.Collections;

public class PlayerAnimationScript : MonoBehaviour {
	
	public Transform gunTransform;

	private Vector3 prevPos;
	private Vector3 deltaPos;

	private string animPrefix = "R";

	private int myPlayer;

	private Animator anim;

	public bool playingDeath;

	public void Awake() {
		anim = GetComponent<Animator>();
	}

	public void Start() {
		myPlayer = GetComponent<PlayerScript>().myPlayer;

		switch(myPlayer) {
		case 1:
			animPrefix = "B";
			break;
		case 2:
			animPrefix = "R";
			break;
		case 3:
			animPrefix = "G";
			break;
		case 4:
			animPrefix = "Y";
			break;
		}
	} 

	public void PlayDeath(){
		anim.Play("Death");
		playingDeath = true;
	}

	public void Update() {
		if(playingDeath)return;

		deltaPos = transform.position - prevPos;

		if(deltaPos.magnitude > 0f) {
			if((deltaPos.x < 0f && transform.localScale.x > 0f) || (deltaPos.x > 0f && transform.localScale.x < 0f)) {
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			if(transform.position.y > -10.5f) {
				anim.Play(animPrefix + "Jump");
			} else {
				anim.Play(animPrefix + "Run");
			}
		} else {
			anim.Play(animPrefix + "Idle");
		}

		prevPos = transform.position;

		// Gun pointing
		float x = Input.GetAxis ("Horizontal2P"+myPlayer);
		float y = Input.GetAxis ("Vertical2P"+myPlayer);

		if(transform.localScale.x < 0f) {
			//gunTransform.eulerAngles = new Vector3(0f, 0f, -Mathf.Atan2(y, x) * 60f + 155f);
		} else {
			//gunTransform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(y, x) * 60f - 35f);
		}
	}
}
