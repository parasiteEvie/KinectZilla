using UnityEngine;
using System.Collections;

public enum EquipItem 
{
	BULLET,
	BOMB
}

[RequireComponent(typeof(AudioSource))]

public class PlayerScript : MonoBehaviour 
{

	public float speed = 1.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	//gameobjects
	public GameObject bullet;
	public GameObject bomb;

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

	private Vector3 moveDirection = Vector3.zero;

	public GameObject invincibilityEffect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		weaponSelection ();
		//update timers 
		timer += Time.deltaTime;
		invincibleTimer -= Time.deltaTime;

		if (invincibleTimer < 0f) {
			invincibilityEffect.SetActive (false);
		}

		if(myPlayer == 0)return;

		if(deathCountdown > 0f){
			deathCountdown -= Time.deltaTime;
			if(deathCountdown <= 0f){
				Destroy(this.gameObject);
			}
			return;
		}
		CharacterController controller = GetComponent<CharacterController>();

		//update timer 
		timer += Time.deltaTime;

		moveDirection.x = Input.GetAxis ("HorizontalP" + myPlayer) * speed;
		moveDirection.z = Input.GetAxis("VerticalP"+myPlayer) * speed;


		if (controller.isGrounded) 
		{
			moveDirection.y = 0;
			if ( Application.platform == RuntimePlatform.WindowsEditor ||
			    Application.platform == RuntimePlatform.WindowsPlayer ||
			    Application.platform == RuntimePlatform.WindowsWebPlayer) 
			{

				if (Input.GetButton("JumpP"+myPlayer) || Input.GetAxis("JumpP"+myPlayer) > 0 || Input.GetAxis("JumpP"+myPlayer+"alt") > 0 || Input.GetAxis("JumpP"+myPlayer) < 0 || Input.GetAxis("JumpP"+myPlayer+"alt") < 0)
				{
					moveDirection.y = jumpSpeed;
					audio.clip = jumpSound;
					audio.Play();
				}
				
			else
			{
				if (Input.GetButton("JumpP"+myPlayer+"Mac"))
				{
					moveDirection.y = jumpSpeed;
					audio.clip = jumpSound;
					audio.Play();
				}
				
			}
			
			}
		}



		//Vector3 newPosition = transform.position;
		//newPosition.x += Input.GetAxis("HorizontalP"+myPlayer) * speed * Time.deltaTime;
		//Debug.Log ("I am" +"HorizontalP"+myPlayer);
		//transform.position = newPosition;

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		//shoot
		if(Time.timeScale != 0)  //Fixes pause issue. Without this IF statement the player is able to fire 1 bullet when the game is paused.
			{
		// Place your code to shoot
			float x;

			if ( Application.platform == RuntimePlatform.WindowsEditor ||
			    Application.platform == RuntimePlatform.WindowsPlayer ||
			    Application.platform == RuntimePlatform.WindowsWebPlayer) 
			{
				x = Input.GetAxis ("Horizontal2P"+myPlayer);
			}
			else
			{
				x = Input.GetAxis ("JumpP"+myPlayer+"alt");
			}
		float y;

			if ( Application.platform == RuntimePlatform.WindowsEditor ||
			    Application.platform == RuntimePlatform.WindowsPlayer ||
			    Application.platform == RuntimePlatform.WindowsWebPlayer) 
			{
				y = Input.GetAxis ("Vertical2P"+myPlayer);
			}
			else
			{
				y = -Input.GetAxis ("Horizontal2P"+myPlayer);
			}
		if (x != 0f || y != 0f) 
			{
			//Debug.Log(x + " " + y);
			switch (currentWeapon)
				{
				case EquipItem.BULLET:
					if (timer >= BulletDelay)
					{
						BulletAI bai1 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation)).GetComponent<BulletAI>();
						bai1.targetDirection = new Vector3(x, y, 0f).normalized;
						audio.clip = bulletSound;
						audio.Play();
						timer = 0;
					}
					break;
				case EquipItem.BOMB:
					if(bombCount <= 0){break;}
					if (timer >= BombDelay)
					{
						BombAI bai2 = ((GameObject)Instantiate (bomb, transform.position, transform.rotation)).GetComponent<BombAI>();
						bai2.targetDirection = new Vector3(x, y, 0f).normalized;
						audio.clip = bombSound;
						audio.Play();
						bombCount -= 1;
						Debug.Log ("Number of bombs = "+bombCount);
						timer = 0;
					}
					break;
				default: 
					break;
				}
			}
		}

		//player death
		if(transform.position.y < -30)
		{
			//FALSE is sent because the player is NOT being killed by the boss
			KillPlayer(false);
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
				pl.player1IsActive = false;
				break;
				
			case 2:
				pl.player2IsActive = false;
				break;
				
			case 3:
				pl.player3IsActive = false;
				break;
				
			case 4:
				pl.player4IsActive = false;
				break;
				
			}

			deathCountdown = 3.0f;

			PlayerAnimationScript p = this.gameObject.GetComponent<PlayerAnimationScript>();
			p.PlayDeath();
		}
	}

	void OnDestroy() 
	{
		//set variable not active.


	}

	public void weaponSelection()
	{
		if (Input.GetButtonDown("BumperP"+myPlayer))
		{
			int cw = (int)currentWeapon;
			currentWeapon = (EquipItem)(int)((cw + 1)%2);
			Debug.Log ("Equiped Weapon = "+currentWeapon);
		}


	}


	public void MakeInvincible() 
	{
		//TODO: Add logic that makes the player Invincible for 10 seconds. THIS FUNCTION DOES NOT YET EXIST
		Debug.Log ("Invincible!");
		invincibleTimer = 10f;
		invincibilityEffect.SetActive (true);
	}

	public void IncrementBombCounter()
	{
		bombCount += 1;
		Debug.Log ("Number of Bombs = "+bombCount);
	}
}


