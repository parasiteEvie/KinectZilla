using UnityEngine;
using System.Collections;

public class CityDweller : MonoBehaviour {

	public AudioClip[] screams;

	public float screamTimer;

	public Vector3 direction;
	public float speed = .06f;
	public int queueOrder = 3000;

	private Vector3 startPoint;

	private bool trampled = false;
	// Use this for initialization
	void Start () {
		screamTimer = Random.Range (2.3f, 16.3f);
		float angle = Random.Range (2.74f, 7.28f);
		startPoint = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);

		GetComponentInChildren<Animator> ().SetInteger ("RandomInt", Random.Range (0, 2));
		direction = new Vector3 (Mathf.Cos (angle), 0f, Mathf.Sin (angle));
		if (direction.x > 0) {
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (trampled) {
			return;
		}
		if (screamTimer < 0f) {
			screamTimer = Random.Range (42.1f, 140.3f); 
			audio.clip = screams[Random.Range(0, screams.Length - 1)];
			audio.volume = audio.volume / 2f;
			audio.Play();
		}
		screamTimer -= Time.deltaTime;

		//Move in a direction
		GetComponent<CharacterController>().Move(direction * speed);
		GetComponentInChildren<SpriteRenderer> ().sortingOrder = -Mathf.CeilToInt (transform.position.z);

		if (Vector3.Distance (startPoint, this.transform.position) > 100f) {
			Destroy(this.gameObject);
		}
						
	}

	public void SetTrampled(){
		trampled = true;
	}

}
