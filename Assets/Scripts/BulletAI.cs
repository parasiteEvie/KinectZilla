using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.y += speed * Time.deltaTime;
		transform.position = newPosition;

	}
}
