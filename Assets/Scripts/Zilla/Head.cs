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

	public void Update(){
		if(lifePoints <= 0){
			Debug.Log("GAME OVER! THE BOSS IS DEAD!");
			myState = BossState.DYING;
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

	public void DealDamage()
	{
		switch (myState)
		{
		case BossState.NORMAL:
			lifePoints -= 1;
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
