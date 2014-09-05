using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class CollectibleScript : MonoBehaviour {

	float moveAnimation = 1.75f;
	bool upDirection = true;
	float speed = .60f;
	public AudioClip itemPickup;
	public CollectibleType myType;
	private Animator anim;

	public void Awake() 
	{
		anim = GetComponent<Animator>();
	}

	public void CollectibleAnimation()
	{
		if (myType == CollectibleType.HEALTH_PACK) 
		{
			anim.Play ("healthanim");
		}
		if (myType == CollectibleType.BOMB) 
		{
			anim.Play ("bombanim");
		}

	}


	// Use this for initialization
	void Start () 
	{
		CollectibleAnimation ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(upDirection)
		{
			this.gameObject.transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
		}
		else{
			this.gameObject.transform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);
		}

		if (moveAnimation <= 0f){
			moveAnimation = 1.75f;
			upDirection = !upDirection;
		}
		else{
			moveAnimation -= Time.deltaTime;
		}


	}
	void killYourself()
	{
		Destroy (this.gameObject);
	}


	void OnTriggerEnter (Collider col) 
	{
		if(col.gameObject.tag == "Player" && myType == CollectibleType.HEALTH_PACK)
		{
			//sound effect for item collect
			audio.clip = itemPickup;
			audio.Play();

			this.gameObject.renderer.enabled = false;

			//Run player invincibility function under PlayerScript
			PlayerScript ps = col.gameObject.GetComponent<PlayerScript>();
			ps.MakeInvincible();
						
			//this gameObject can go away
			Invoke("killYourself", 1f);
		}
		if (col.gameObject.tag == "Player" && myType == CollectibleType.BOMB) 
		{
			Debug.Log ("Bomb");

			//this gameObject can go away
			Invoke("killYourself", 1f);
		}

	}

}
