using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject bullet;
	public float ShotDelay = 1.0f;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		
		if (timer >= ShotDelay)
		{
			// Place your code to shoot
			if (Input.GetButton ("Fire1")) {
				timer = 0;
				Instantiate (bullet, transform.position, transform.rotation);
			}
		}



	}
}
