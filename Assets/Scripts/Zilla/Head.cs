using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public enum BossState{
	NORMAL,
	RAGE,
	DYING
}
public class Head:MonoBehaviour {
	private Vector3 pos;

	private BodyPartControls bpc;
	private int lifePoints = 350;
	private BossState myState;
	private Animator anim;

	public AudioClip roar;
	public AudioClip spew;
	public BossQuips bossquip;

	public GameObject fireSpew;

	public float fireSpewTimer;

	// Init
	public void Awake() {
		bpc = GetComponent<BodyPartControls>();
		anim = GetComponent<Animator>();
	}

	public void Start(){
		myState = BossState.NORMAL;
	}

	public void UpdatePosition(){
		transform.position = new Vector3(Camera.main.transform.position.x, 
		                                 transform.position.y , 
		                                 transform.position.z);
	}

	public void Update()
	{
		BroadcastMessage ("AdjustCurrentHealth", (lifePoints));
		if(lifePoints <= 0)
		{
			myState = BossState.DYING;
			Application.LoadLevel("PlayersWin");
		}
		fireSpewTimer -= Time.deltaTime;
	}

	public void LateUpdate() {
		pos = transform.position;
		// Check bounds
		if(pos.x > Camera.main.transform.position.x + 26f) {
			pos.x = Camera.main.transform.position.x + 26f;
		}
		if(pos.x < Camera.main.transform.position.x - 26f) {
			pos.x = Camera.main.transform.position.x - 26f;
		}
		if(pos.y < Camera.main.transform.position.y - 10f) {
			pos.y = Camera.main.transform.position.y - 10f;
		}
		if(pos.y > Camera.main.transform.position.y + 18f) {
			pos.y = Camera.main.transform.position.y + 18f;
		}
		// Adjust
		transform.position = pos;

		if (transform.position.y < 4.25f) {
			anim.SetBool("StartSpewCue", true);
			myState = BossState.RAGE;
			if(fireSpewTimer < 0f){
				GameObject w = (GameObject) Instantiate(fireSpew, transform.position + (Vector3.down * 5.5f), Quaternion.identity);
				Vector3 dirSpew = new Vector3(transform.position.x + ( Mathf.PingPong(Time.time*75f, 100f) - 30f), transform.position.y - 25f, transform.position.z-20f);
				w.GetComponent<FireSpewAI>().targetDirection = dirSpew - (w.transform.position + new Vector3(0, 0, 10f));
				w.GetComponent<FireSpewAI>().targetDirection.Normalize();
				fireSpewTimer = .1f;
			}
		} 
		else {
			anim.SetBool("StartSpewCue", false);
			myState = BossState.NORMAL;
		}
	}

	//void OnTriggerEnter(Collider other)
	void OnTriggerEnter(Collider other)
	{
		switch (other.gameObject.tag) 
		{
			case "Bullet":
					//TODO: Display the shot landed animation.
					//gun_boss_damage_spark0001
			
					//TODO: Add sound effect for boss damage
					//AudioSource.Play(boss damage);
					
					//remove life from the boss\
					DealDamage ((int)EquipItem.BULLET);
					break;

			case "Bomb":
					//TODO: Display the shot landed animation.
					//gun_boss_damage_spark0001
			
					//TODO: Add sound effect for boss damage
					//AudioSource.Play(boss damage);
			
					//remove life from the boss\
					DealDamage ((int)EquipItem.BOMB);
					break;
	
			default:
					break;
		}
}

	public void DealDamage(int e)
	{
		switch (myState)
		{
		case BossState.NORMAL:
			if(e == (int)EquipItem.BULLET){lifePoints -= 1;}
			if(e ==(int)EquipItem.BOMB){lifePoints -= 20;}
			break;
		case BossState.RAGE:
			if(e == (int)EquipItem.BULLET){lifePoints -= 3;}
			if(e ==(int)EquipItem.BOMB){lifePoints -= 40;}
			break;
		default:
			break;
		}
	
		Debug.Log("current boss life: "+lifePoints);
	}
	public void Spew(){
		audio.clip = spew;
		audio.Play ();
	}
	public void Roar(){
		audio.clip = roar;
		audio.Play ();
		bossquip.StartQuip ();
	}
}
