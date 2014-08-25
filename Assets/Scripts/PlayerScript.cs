using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class PlayerScript : MonoBehaviour {

	public float speed = 1.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	
	public GameObject bullet;
	public float ShotDelay = 1.0f;
	private float timer = 0.0f;
	public int myPlayer;

	public AudioClip jumpSound;
	public AudioClip shotSound;
	public AudioClip deathSound;

	public float deathCountdown = 0.0f;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("HorizontalP"+myPlayer),0, Input.GetAxis("VerticalP"+myPlayer));
			//moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			Debug.Log("jumping"+Input.GetAxis("JumpP"+myPlayer));
			if (Input.GetButton("JumpP"+myPlayer) || Input.GetAxis("JumpP"+myPlayer) > 0 || Input.GetAxis("JumpP"+myPlayer+"alt") > 0 || Input.GetAxis("JumpP"+myPlayer) < 0 || Input.GetAxis("JumpP"+myPlayer+"alt") < 0)
			{

				moveDirection.y = jumpSpeed;
				audio.clip = jumpSound;
				audio.Play();
				
			}
			
		}
		//Vector3 newPosition = transform.position;
		//newPosition.x += Input.GetAxis("HorizontalP"+myPlayer) * speed * Time.deltaTime;
		//Debug.Log ("I am" +"HorizontalP"+myPlayer);
		//transform.position = newPosition;

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		//shoot
		if (timer >= ShotDelay)
		{
			// Place your code to shoot
			float x = Input.GetAxis ("Horizontal2P"+myPlayer);
			float y = Input.GetAxis ("Vertical2P"+myPlayer);
			if (x != 0f || y != 0f) {
				timer = 0;
				Debug.Log(x + " " + y);
				BulletAI bai = ((GameObject)Instantiate (bullet, transform.position, transform.rotation)).GetComponent<BulletAI>();
				bai.targetDirection = new Vector3(x, y, 0f).normalized;
				audio.clip = shotSound;
				audio.Play();

			}
		}

		//player death
		if(transform.position.y < -30){
			KillPlayer();
		}
	}

	public void KillPlayer(){
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

	void OnDestroy() {
		//set variable not active.


	}
}


