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
	private float dustTimer = 0f;

	private Animator anim;
	private AudioSource asource;

	public AudioClip scootSE;

	public bool playingDeath;

	public GameObject shadow;
	public GameObject specialEffectprefab;
	public GameObject se;

	CharacterController characterController;

	public string mac;

	public void Awake() {
		anim = GetComponent<Animator>();
		asource = GetComponentInParent<AudioSource> ();

	}

	public void Start() {
		GetComponent<SpriteRenderer> ().castShadows = true;
		myPlayer = this.GetComponentInParent<PlayerScript>().myPlayer;
		characterController = this.GetComponentInParent<CharacterController> ();

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

		mac = "Mac";
		if ( Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			mac = "";
		}
	} 

	public void PlayDeath(){
		anim.Play("Death");
		playingDeath = true;
	}

	public void Update() {
		dustTimer -= Time.deltaTime;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			float distanceToGround = hit.distance;
			//set shadow position to hit.collider.ClosestPointOnBounds().y;
			shadow.transform.position = new Vector3(shadow.transform.position.x, hit.collider.ClosestPointOnBounds(transform.position).y, shadow.transform.position.z);
		}

		if(playingDeath)return;

		deltaPos = transform.position - prevPos;

		if (deltaPos.magnitude > 0f) {
						if ((deltaPos.x < 0f && transform.localScale.x > 0f) || (deltaPos.x > 0f && transform.localScale.x < 0f)) {
								transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
						}
						if (!characterController.isGrounded) {
								anim.Play (animPrefix + "Jump");

						} else {
								if (this.GetComponentInParent<PlayerScript> ().playerAcceleration == AccelerationState.ACCELERATING) {
										anim.Play (animPrefix + "Run");
						} else if (this.GetComponentInParent<PlayerScript> ().playerAcceleration == AccelerationState.DECCELERATING_HEAVY) {
										anim.Play (animPrefix + "Stop");
										//TODO:  add little dust cloud particles
										if (se == null) {
												if (deltaPos.x < 0) {
														se = (GameObject)Instantiate (specialEffectprefab, transform.position + (new Vector3 (-5.5f, -1.87f, 0)), Quaternion.identity);
														se.transform.localScale = new Vector3 (-se.transform.localScale.x, se.transform.localScale.y, se.transform.localScale.z); 
												} else {
														se = (GameObject)Instantiate (specialEffectprefab, transform.position + (new Vector3 (5.5f, -1.87f, 0)), Quaternion.identity);
												}
												asource.clip = scootSE;
												asource.Play ();
										}
										//TODO: add sound effect for braking
								} else if (this.GetComponentInParent<PlayerScript>().playerAcceleration == AccelerationState.DECCELERATING) {
										anim.Play (animPrefix + "Stop");
								}
						}
		} else if (this.GetComponentInParent<PlayerScript>().healing) {
			anim.Play(animPrefix + "Kneel");
			return;
		}
		else {
			anim.Play(animPrefix + "Idle");
		}

		prevPos = transform.position;


		// Gun pointing
		float x;
		x = Input.GetAxis ("Horizontal2P"+myPlayer+mac);

		float y;

		y = Input.GetAxis ("Vertical2P"+myPlayer+mac);

		var lookPos = new Vector3(y, 0f, -x).normalized;
		gunArm.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(y, x) *Mathf.Rad2Deg - 30f), Vector3.forward);

		if(transform.localScale.x < 0f) {
			//gunArm.transform.rotation = Quaternion.LookRotation(lookPos);
			if(x == 0 && y == 0)
			{
				gunArm.transform.rotation = Quaternion.AngleAxis(-215, Vector3.forward);
			}

		} 
	}


}
