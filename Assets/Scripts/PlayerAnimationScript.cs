﻿using UnityEngine;
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

	public bool playingDeath;

	public GameObject shadow;
	public GameObject specialEffectprefab;
	public GameObject se;

	CharacterController characterController;

	public void Awake() {
		anim = GetComponent<Animator>();
		characterController = GetComponent<CharacterController> ();
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
		dustTimer -= Time.deltaTime;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			float distanceToGround = hit.distance;
			//set shadow position to hit.collider.ClosestPointOnBounds().y;
			shadow.transform.position = new Vector3(shadow.transform.position.x, hit.collider.ClosestPointOnBounds(transform.position).y, shadow.transform.position.z);
		}

		if(playingDeath)return;

		deltaPos = transform.position - prevPos;

		if(deltaPos.magnitude > 0f) {
			if((deltaPos.x < 0f && transform.localScale.x > 0f) || (deltaPos.x > 0f && transform.localScale.x < 0f)) {
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			if(!characterController.isGrounded) {
				anim.Play(animPrefix + "Jump");

			} else {
				if(GetComponent<PlayerScript>().playerAcceleration == AccelerationState.ACCELERATING){
					anim.Play(animPrefix + "Run");
				}
				else if(GetComponent<PlayerScript>().playerAcceleration == AccelerationState.DECCELERATING_HEAVY){
					anim.Play(animPrefix + "Stop");
					//TODO:  add little dust cloud particles
					Debug.Log ("curr position"+transform.position.ToString());
					if(se == null){
						if(deltaPos.x < 0){
						se = (GameObject)Instantiate(specialEffectprefab, transform.position+(new Vector3(-5.5f, -1.87f, 0)), Quaternion.identity);
							se.transform.localScale = new Vector3(-se.transform.localScale.x,se.transform.localScale.y,se.transform.localScale.z); 
						}else{
							se = (GameObject)Instantiate(specialEffectprefab, transform.position+(new Vector3(5.5f, -1.87f, 0)), Quaternion.identity);
						}
					}
					//TODO: add sound effect for braking
				}
				else if(GetComponent<PlayerScript>().playerAcceleration == AccelerationState.DECCELERATING){
					anim.Play(animPrefix + "Stop");
				}
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
