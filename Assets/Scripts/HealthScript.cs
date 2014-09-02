using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	float moveAnimation = 1.75f;
	bool upDirection = true;
	float speed = .60f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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


	void OnCollisionEnter (Collision col) 
	{
		if(col.gameObject.tag == "Player")
		{
			//Run player invincibility function under PlayerScript
			PlayerScript ps = col.gameObject.GetComponent<PlayerScript>();
			ps.MakeInvincible();
			//TODO: Add sound effect for item collect
//			AudioSource.Play(healthPackCollectedSE);
			
			//this gameObject can go away
			Destroy(this.gameObject);
		}
	}

}
