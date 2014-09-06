using UnityEngine;
using System.Collections;

public class BombAI : MonoBehaviour {
	
	public float speed;

	public Vector3 targetDirection;


	// Update is called once per frame
	void Update () 
	{
		if (targetDirection == Vector3.zero)
		{
			speed = speed/5f;
			targetDirection = Vector3.up;
		}
		Vector3 newPosition = transform.position;
		newPosition = newPosition + targetDirection * speed * Time.deltaTime;
		transform.position = newPosition;
		transform.Rotate(0,0, 500 * Time.deltaTime);
		
	}
	
	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
