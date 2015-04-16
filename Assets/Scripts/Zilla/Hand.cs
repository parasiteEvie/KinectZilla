using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

[RequireComponent(typeof(AudioSource))]

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
	public bool isBoss;

	public BodyPartControls bpc;
	private float closedHandTimer;
	public bool isMenu;
	private Animator anim;

	public AudioClip smashSound;

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

		if(!isMenu)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				anim.Play("fistanim");
				HandAttack();
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				followTarget.transform.position += new Vector3(-1,0,0);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				followTarget.transform.position += new Vector3(1,0,0);
			}
			// Attack
			if(pos.y < ATTACK_Y_POS && ! attacking && recovered) {
				if(bpc.bodyPartClosed) {
					HandAttack();
				} else {
					recovered = false;
				}
			} else if(! recovered && pos.y > RECOVER_Y_POS && ! attacking) {
				recovered = true;
			}
		}

		FollowSkeletalTracking ();

		if(!attacking){
			// Hand animation
			if( bpc.bodyPartClosed) {
				anim.Play("fistanim");
			} else {
				anim.Play("handanim");
			}
		}
	}

	public void UpdatePosition(){
		followTarget.position = startPos + new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
		Debug.Log("position: "+followTarget.position);
	}

	// Attack!
	private void HandAttack() {
		collider.enabled = true;
		attacking = true;
		recovered = false;
		targetPos = transform.position;
		targetPos.y = -8f;
		targetPos.z = startPos.z;
		HOTween.To(transform, 1f, new TweenParms().Prop("position", targetPos).Ease(EaseType.EaseOutBounce).OnComplete(DoneHandAttack));
		audio.clip = smashSound;
		audio.Play();
	}

	void FollowSkeletalTracking ()
	{
		// Follow
		if (!attacking) {
			// Move towards follow target
			deltaPos = Vector3.ClampMagnitude (followTarget.position - pos, 10f);
			// Z adjustment
			if (!recovered && pos.z < 30f) {
				deltaPos.z = Time.deltaTime * 300f;
			}
			else
				if (recovered && pos.z > 20f) {
					deltaPos.z = -Time.deltaTime * 300f;
				}
				else {
					deltaPos.z = 0f;
				}
			// Pre-adjust
			if (deltaPos.magnitude > 1f) {
				pos += deltaPos * Time.deltaTime * 5f;
			}
			// Check bounds
			if (pos.x > Camera.main.transform.position.x + 28f) {
				pos.x = Camera.main.transform.position.x + 28f;
			}
			if (pos.x < Camera.main.transform.position.x -28f) {
				pos.x = Camera.main.transform.position.x -28f;
			}
			if (pos.y < Camera.main.transform.position.y-10f) {
				pos.y = Camera.main.transform.position.y-10f;
			}
			if (pos.y > Camera.main.transform.position.y + 18f) {
				pos.y = Camera.main.transform.position.y + 18f;
			}
			// Adjust
			transform.position = pos;
		}
	}

	private void DoneHandAttack() {
		attacking = false;
		collider.enabled = false;
	}

	public bool MenuSelect()
	{
		if(isMenu)
		{
			if(bpc.bodyPartClosed && closedHandTimer > 0f)
			{
				if(closedHandTimer - Time.deltaTime <= 0f)
				{
					audio.clip = smashSound;
					audio.Play();
					return true;

				}
				closedHandTimer -= Time.deltaTime;
				this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.black, Color.white, 1 - (closedHandTimer * 2f));
			}
			else if(bpc.bodyPartClosed && closedHandTimer <= 0f)
			{
				closedHandTimer = 1f;
			}
			else{
				closedHandTimer = 0;
			}

		}
		return false;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			isBoss = true;
			PlayerScript p = collision.gameObject.GetComponent<PlayerScript> ();
			p.KillPlayer (isBoss);
		} else if (collision.transform.parent != null && collision.transform.parent.gameObject.tag == "Dweller") {
			collision.transform.parent.GetComponent<CityDweller>().SetTrampled();
			collision.gameObject.GetComponentInChildren<Animator>().SetTrigger("Trampled");
		}
	}

	void OnTriggerEnter(Collider collider){
	if (collider.gameObject.tag == "LevelItem") {
		PedastalScript p = collider.gameObject.GetComponent<PedastalScript>();
		p.SetDamage(.1f);
	}
	}
}
