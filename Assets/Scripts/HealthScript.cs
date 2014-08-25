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
}
