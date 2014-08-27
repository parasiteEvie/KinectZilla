using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

	public float speed;


	public Vector3 targetDirection;

	// Update is called once per frame
	void Update () {
		if (targetDirection == Vector3.zero){
			speed = speed/5f;
			targetDirection = Vector3.up;
		}
		Vector3 newPosition = transform.position;
		newPosition = newPosition + targetDirection * speed * Time.deltaTime;
		transform.position = newPosition;

	}
	
	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
