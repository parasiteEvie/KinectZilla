using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

	public float speed;

	public Vector3 targetDirection;

	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition = newPosition + targetDirection * speed;
		transform.position = newPosition;

	}
}
