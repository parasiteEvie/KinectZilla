using UnityEngine;
using System.Collections;

public class PlayerAnimationScript : MonoBehaviour {
	
	public GameObject gunArm;

	public Sprite redArmSpr;
	public Sprite greenArmSpr;
	public Sprite blueArmSpr;
	public Sprite yellowArmSpr;

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
			gunArm.GetComponent<SpriteRenderer>().sprite = blueArmSpr;
			break;
		case 2:
			animPrefix = "R";
			gunArm.GetComponent<SpriteRenderer>().sprite = redArmSpr;
			break;
		case 3:
			animPrefix = "G";
			gunArm.GetComponent<SpriteRenderer>().sprite = greenArmSpr;
			break;
		case 4:
			animPrefix = "Y";
			gunArm.GetComponent<SpriteRenderer>().sprite = yellowArmSpr;
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
		float x;
		
		if ( Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			
			x = Input.GetAxis ("Horizontal2P"+myPlayer);
		}
		else{
			x = Input.GetAxis ("JumpP"+myPlayer+"alt");
		}
		float y;
		
		if ( Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			y = Input.GetAxis ("Vertical2P"+myPlayer);
		}
		else{
			y = -Input.GetAxis ("Horizontal2P"+myPlayer);
		}

		var lookPos = new Vector3(y, 0f, -x).normalized;
		if(transform.localScale.x < 0f) {
			//gunArm.transform.rotation = Quaternion.LookRotation(lookPos);
			if(x == 0 && y == 0)
			{
				gunArm.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);
			}
			else
			{
				gunArm.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(y, -x) *Mathf.Rad2Deg - 30f), Vector3.forward);
			}
		} else {
			gunArm.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(y, x) *Mathf.Rad2Deg - 30f), Vector3.forward);
		}
	}
}
