using UnityEngine;
using System.Collections;

public enum EquipItem 
{
	BULLET,
	BOMB
}

public enum AccelerationState{
	DECCELERATING,
	DECCELERATING_HEAVY,
	NONE,
	ACCELERATING,

}
[RequireComponent(typeof(AudioSource))]
public class PlayerScript : MonoBehaviour 
{
	public float acceleration = 0f;
	public float jumpSpeed = 8.0f;
	public float JUMP_PUSH_TIMER = .25f;
	public float MAX_RUN_SPEED = 20;
	public float FRICTION_COOEFFICIENT = 0.19f;
	private float jumpTimer;
	public float gravity = 20.0f;
	public AccelerationState playerAcceleration = AccelerationState.NONE;

	public PlayerAnimationScript playerAnim;
	//gameobjects
	public GameObject healingWater;
	public GameObject bullet;
	public GameObject bomb;

	public bool healing;

	//delay timers
	public float BulletDelay = 0.25f;
	public float BombDelay = 2.0f;
	private float timer = 0.0f;
	public float invincibleTimer = 0f;

	public int myPlayer;

	public int bombCount;
	public EquipItem currentWeapon;

	public AudioClip jumpSound;
	public AudioClip bulletSound;
	public AudioClip bombSound;
	public AudioClip deathSound;

	public float deathCountdown = 0.0f;

	private Vector3 moveVelocity = Vector3.zero;
	public Vector3 warpPosition = Vector3.zero;

	public GameObject invincibilityEffect;
	public string mac;
	// Use this for initialization
	void Start () {
		healing = false;
		mac = "Mac";
		if (Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			mac = "";

		}

		invincibleTimer = 2.0f;
	}

	public void LateUpdate(){
		if (warpPosition == Vector3.zero){
			return;
		}
		transform.position = warpPosition;
		warpPosition = Vector3.zero;
	}
	// Update is called once per frame
	void Update () 
	{
		SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer sr in spr){
			sr.sortingOrder = (int)-transform.position.z;
		}
				playerAcceleration = AccelerationState.NONE;

		// Place your code to shoot
		float x;
		x = Input.GetAxis ("Horizontal2P" + myPlayer + mac);

		float y;
		y = Input.GetAxis ("Vertical2P" + myPlayer+mac);


				Vector3 currentVelocity = Vector3.zero;
				weaponSelection ();
				//update timers 
				timer += Time.deltaTime;
				invincibleTimer -= Time.deltaTime;

				if (invincibleTimer < 0f) {
						invincibilityEffect.SetActive (false);
				}

				if (myPlayer == 0)
						return;


				if (deathCountdown > 0f) {
						deathCountdown -= Time.deltaTime;
						if (deathCountdown <= 0f) {
								Destroy (this.gameObject);
						}
						return;
				}

		if (Input.GetButton("HealP" + myPlayer + mac)) {		
			//HEAL town
			healing = true;
			if (timer * 3f >= BulletDelay) {
				healingWater.SetActive(true);
				if(playerAnim.transform.localScale.x < 0f){
					healingWater.transform.localPosition = new Vector3(-Mathf.Abs(healingWater.transform.localPosition.x), healingWater.transform.localPosition.y,
					                                                   healingWater.transform.localPosition.z);
				}
				else{
					healingWater.transform.localPosition = new Vector3(Mathf.Abs(healingWater.transform.localPosition.x), healingWater.transform.localPosition.y,
					                                                   healingWater.transform.localPosition.z);
				}

				audio.clip = bulletSound;
				audio.Play ();
				timer = 0;

			}
			playerAnim.gunArm.SetActive(false);
			return;
		}
		else{
			healingWater.SetActive(false);
			playerAnim.gunArm.SetActive(true);

		}
		healing = false;

				CharacterController controller = GetComponent<CharacterController> ();


				//update timer 
				timer += Time.deltaTime;

				if (Mathf.Abs (moveVelocity.x) < MAX_RUN_SPEED) {
						moveVelocity.x = controller.velocity.x + (Input.GetAxis ("HorizontalP" + myPlayer + mac) * acceleration * Time.deltaTime);
						playerAcceleration = AccelerationState.ACCELERATING;
				}
				if (Mathf.Abs (moveVelocity.z) < MAX_RUN_SPEED) {
						moveVelocity.z = controller.velocity.z + (Input.GetAxis ("VerticalP" + myPlayer + mac) * acceleration * Time.deltaTime);
						playerAcceleration = AccelerationState.ACCELERATING;
				}
				if (Input.GetAxis ("HorizontalP" + myPlayer + mac) == 0 && moveVelocity.x != 0f) {
						//friction coefficient remove
						moveVelocity.x += (-moveVelocity.x) * FRICTION_COOEFFICIENT;
						playerAcceleration = AccelerationState.DECCELERATING;
						if (Mathf.Abs (moveVelocity.x) < 1.5f) {
								moveVelocity.x = 0;
						}
				} else if (Input.GetAxis ("HorizontalP" + myPlayer + mac) > 0 && moveVelocity.x < 0f) {
						//friction coefficient remove
						moveVelocity.x += (-moveVelocity.x) * .5f * FRICTION_COOEFFICIENT;
						playerAcceleration = AccelerationState.DECCELERATING_HEAVY;
				} else if (Input.GetAxis ("HorizontalP" + myPlayer + mac) < 0 && moveVelocity.x > 0f) {
						//friction coefficient remove
						moveVelocity.x += (-moveVelocity.x) * .5f * FRICTION_COOEFFICIENT;
						playerAcceleration = AccelerationState.DECCELERATING_HEAVY;
				}

				if (Input.GetAxis ("VerticalP" + myPlayer + mac) == 0 && moveVelocity.z != 0f) {
						//friction coefficient remove
						moveVelocity.z += (-moveVelocity.z) * FRICTION_COOEFFICIENT;
				} else if (Input.GetAxis ("VerticalP" + myPlayer) > 0 && moveVelocity.z < 0f) {
						//friction coefficient remove
						moveVelocity.z += (-moveVelocity.z) * 3f * FRICTION_COOEFFICIENT;
				} else if (Input.GetAxis ("VerticalP" + myPlayer) < 0 && moveVelocity.z > 0f) {
						//friction coefficient remove
						moveVelocity.z += (-moveVelocity.z) * 3f * FRICTION_COOEFFICIENT;
				}

				if (controller.isGrounded) {
						moveVelocity.y = 0;

						if (Input.GetButton ("JumpP" + myPlayer + mac) || Input.GetAxis ("JumpP" + myPlayer + "alt" + mac) > 0 ) {
								moveVelocity.y = jumpSpeed;
								audio.clip = jumpSound;
								audio.Play ();
								jumpTimer = JUMP_PUSH_TIMER;
						} 
			

				} else {

						jumpTimer -= Time.deltaTime;
						// jump push
						if (jumpTimer > 0) {
								if (Input.GetButton ("JumpP" + myPlayer +mac) || Input.GetAxis ("JumpP" + myPlayer + "alt" + mac) > 0) {
										moveVelocity.y += jumpSpeed * 2.0f * Time.deltaTime;

								} 
						}

				}



				//Vector3 newPosition = transform.position;
				//newPosition.x += Input.GetAxis("HorizontalP"+myPlayer) * speed * Time.deltaTime;
				//Debug.Log ("I am" +"HorizontalP"+myPlayer);
				//transform.position = newPosition;

				moveVelocity.y -= gravity * Time.deltaTime;
				controller.Move (moveVelocity * Time.deltaTime);
		
				//shoot
				if (Time.timeScale != 0) {  //Fixes pause issue. Without this IF statement the player is able to fire 1 bullet when the game is paused.
						
						if (x != 0f || y != 0f) {
								//Debug.Log(x + " " + y);
								switch (currentWeapon) {
								case EquipItem.BULLET:
										if (timer >= BulletDelay) {
												BulletAI bai1 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation)).GetComponent<BulletAI> ();
												bai1.targetDirection = new Vector3 (x, y, 0f).normalized;
												audio.clip = bulletSound;
												audio.Play ();
												timer = 0;
										}
										break;
								case EquipItem.BOMB:
										if (bombCount <= 0) {
												break;
										}
										if (timer >= BombDelay) {
												BombAI bai2 = ((GameObject)Instantiate (bomb, transform.position, transform.rotation)).GetComponent<BombAI> ();
												bai2.targetDirection = new Vector3 (x, y, 0f).normalized;
												audio.clip = bombSound;
												audio.Play ();
												bombCount -= 1;
												Debug.Log ("Number of bombs = " + bombCount);
												timer = 0;
										}
										break;
								default: 
										break;
								}
						}
				}

				//player death
				if (transform.position.y < -30) {
						//FALSE is sent because the player is NOT being killed by the boss
						KillPlayer (false);
				}

	}

	void OnTriggerEnter(Collider collision) {
		CharacterController controller = GetComponent<CharacterController> ();
		if (collision.transform.parent != null && collision.transform.parent.gameObject.tag == "Dweller" &&
		    !controller.isGrounded) {
			collision.transform.parent.GetComponent<CityDweller>().SetTrampled();
			collision.gameObject.GetComponentInChildren<Animator>().SetTrigger("Trampled");
			moveVelocity.y = jumpSpeed * 2.8f;
			audio.clip = jumpSound;
			audio.Play ();
			jumpTimer = JUMP_PUSH_TIMER;
		}
	}

	void OnTriggerStay(Collider collision){
		if (Input.GetButtonDown("HealP" + myPlayer + mac)) {
			if(collision.gameObject.name == "City" && collision.gameObject.GetComponent<PedastalScript>().destroyed){
				collision.gameObject.GetComponent<PedastalScript>().RebuildCity();
				return;
			}
		}
	}
	public void KillPlayer(bool isBoss)
	{
//		var invincibleString = "invincibleTimer"+myPlayer;
		Debug.Log ("Invincibility Timer = " + invincibleTimer + ". Did BOSS kill the player? " + isBoss);
		if(isBoss == false || (invincibleTimer < 0f && isBoss == true))
		{
			//play death sound
			audio.clip = deathSound;
			audio.Play();

			CreatePlayer pl = Camera.main.GetComponent<CreatePlayer>();
			switch(myPlayer){
				
			case 1:
				pl.player1IsActive = CreatePlayer.SummonStatus.SUMMONED;
				pl.SummonFireTruck();
				pl.spawntimer1 = 3.0;
				break;
				
			case 2:
				pl.player2IsActive  = CreatePlayer.SummonStatus.SUMMONED;
				pl.SummonFireTruck();
				pl.spawntimer2 = 3.0;
				break;
				
			case 3:
				pl.player3IsActive  = CreatePlayer.SummonStatus.SUMMONED;
				pl.SummonFireTruck();
				pl.spawntimer3 = 3.0;
				break;
				
			case 4:
				pl.player4IsActive  = CreatePlayer.SummonStatus.SUMMONED;
				pl.SummonFireTruck();
				pl.spawntimer4 = 3.0;
				break;
				
			}

			deathCountdown = 3.0f;

			playerAnim.PlayDeath();
		}
	}

	void OnDestroy() 
	{
		//set variable not active.


	}

	public void weaponSelection()
	{
		if (Input.GetButtonDown("BumperP"+myPlayer + mac))
		{
			int cw = (int)currentWeapon;
			currentWeapon = (EquipItem)(int)((cw + 1)%2);
		}


	}


	public void MakeInvincible() 
	{
		//TODO: Add logic that makes the player Invincible for 10 seconds. THIS FUNCTION DOES NOT YET EXIST
		invincibleTimer = 10f;
		invincibilityEffect.SetActive (true);
	}

	public void IncrementBombCounter()
	{
		bombCount += 1;
		Debug.Log ("Number of Bombs = "+bombCount);
	}

}


