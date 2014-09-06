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

	// Init
	public void Awake() {
		bpc = GetComponent<BodyPartControls>();
	}

	public void Start(){
		myState = BossState.NORMAL;
	}

	public void Update()
	{
		BroadcastMessage ("AdjustCurrentHealth", (lifePoints));
		if(lifePoints <= 0)
		{
			Debug.Log("GAME OVER! THE BOSS IS DEAD!");
			myState = BossState.DYING;
			Application.LoadLevel("PlayersWin");
		}
	}

	public void LateUpdate() {
		pos = transform.position;
		// Check bounds
		if(pos.x > 26f) {
			pos.x = 26f;
		}
		if(pos.x < -26f) {
			pos.x = -26f;
		}
		if(pos.y < -10f) {
			pos.y = -10f;
		}
		if(pos.y > 18f) {
			pos.y = 18f;
		}
		// Adjust
		transform.position = pos;
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
					Destroy (other.gameObject);
					break;

			case "Bomb":
					//TODO: Display the shot landed animation.
					//gun_boss_damage_spark0001
			
					//TODO: Add sound effect for boss damage
					//AudioSource.Play(boss damage);
			
					//remove life from the boss\
					DealDamage ((int)EquipItem.BOMB);
					Destroy (other.gameObject);
					break;
	
			default:
					Debug.Log ("Bad String");
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
			lifePoints -= 2;
			break;
		default:
			break;
		}
	
		Debug.Log("current boss life: "+lifePoints);
	}
}
