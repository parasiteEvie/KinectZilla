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
			float x = Input.GetAxis ("Horizontal2");
			float y = Input.GetAxis ("Vertical2");
			if (x != 0f || y != 0f) {
				timer = 0;
				Debug.Log(x + " " + y);
				BulletAI bai = ((GameObject)Instantiate (bullet, transform.position, transform.rotation)).GetComponent<BulletAI>();
				bai.targetDirection = new Vector3(x, y, 0f).normalized;
			}
		}



	}
}
