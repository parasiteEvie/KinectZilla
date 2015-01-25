using UnityEngine;
using System.Collections;

public class CityDweller : MonoBehaviour {

	public AudioClip[] screams;

	public float screamTimer;

	public Vector3 direction;
	public float speed = 2.4f;
	public int queueOrder = 3000;
	// Use this for initialization
	void Start () {
		screamTimer = Random.Range (.3f, 1.3f);
		float angle = Random.Range (2.74f, 7.28f);

		GetComponentInChildren<Animator> ().SetInteger ("RandomInt", Random.Range (0, 2));
		direction = new Vector3 (Mathf.Cos (angle), 0f, Mathf.Sin (angle));
		if (direction.x > 0) {
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (screamTimer < 0f) {
			screamTimer = Random.Range (3.1f, 6.3f); 
			audio.clip = screams[Random.Range(0, screams.Length - 1)];
			audio.volume = audio.volume / 2f;
			audio.Play();
		}
		screamTimer -= Time.deltaTime;

		//Move in a direction
		transform.position += (direction * speed * Time.deltaTime);
		GetComponentInChildren<SpriteRenderer> ().sortingOrder = -Mathf.CeilToInt (transform.position.z);

	}
}
