using UnityEngine;
using System.Collections;

public class ProjectileDestroyed : MonoBehaviour {

	private Animator anim;
	private AudioSource asource;
	public AudioClip smackClip;
	public GameObject trackingobject;
	public Vector3 deltaPosition;

	public void Awake() 
	{
		anim = GetComponent<Animator>();
		asource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (trackingobject != null) {
			this.gameObject.transform.position = trackingobject.transform.position + deltaPosition;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		trackingobject = other.gameObject;
		deltaPosition = this.gameObject.transform.position - other.gameObject.transform.position;
		if(this.gameObject.tag == "Fire"){
			if(other.gameObject.tag != "Ground" && other.gameObject.tag != "Player"){ 
				trackingobject = null;
				return; 
			}
			FireSpewAI fs = this.gameObject.GetComponent<FireSpewAI> ();
			fs.hascollided = true;

			if(other.gameObject.tag == "Player"){
				PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();
				ps.KillPlayer(true);
				Debug.Log("this should kill the player");

			}
			//this gameObject can go away
			Invoke("killYourself", 1.0f);
		}
		else if (this.gameObject.tag == "Bomb"){
			if(!BombHandleOpenHand(other.gameObject)){
				BombAI bomb = this.gameObject.GetComponent<BombAI> ();
				transform.localScale += new Vector3 (4f, 4f, 0);
				anim.Play ("largeExplosion");
				asource.Play();
				//this gameObject can go away
				Invoke("killYourself", 0.6f);
			}
		}
		else {

			BulletAI bai = this.gameObject.GetComponent<BulletAI> ();
			bai.hascollided = true;
			transform.localScale += new Vector3 (2.5f, 2.5f, 0);

			anim.Play ("SmallExplosionAnim");
			if(bai.targetDirection.x == 0 && bai.targetDirection.y == 0)
			{
				transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);
			}
			else
			{
				transform.rotation =  Quaternion.AngleAxis((Mathf.Atan2(bai.targetDirection.y, bai.targetDirection.x) *Mathf.Rad2Deg - 30f), Vector3.forward);
			}
			//this gameObject can go away
			Invoke("killYourself", 0.3f);
		}
	}

	bool BombHandleOpenHand(GameObject o){
		if (o.tag == "Hand") {
			Hand hs = o.GetComponent<Hand>();
			if(!hs.bpc.bodyPartClosed){
				trackingobject = null;
				BombAI bomb = this.gameObject.GetComponent<BombAI> ();
				bomb.targetDirection = -bomb.targetDirection;
				transform.localScale += new Vector3 (-.5f, -.5f, 0);
				//anim.Play ("largeExplosion");
				asource.clip = smackClip;
				asource.Play();
				//this gameObject can go away
				Invoke("killYourself", 1.6f);
				return true;
			}
		}
		return false;
	}

	void killYourself()
	{
		Destroy (this.gameObject);
	}






}
