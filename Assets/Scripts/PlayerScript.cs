using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 1.0f;

	public GameObject bullet;
	public float ShotDelay = 1.0f;
	private float timer = 0.0f;
	public int myPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//update timer 
		timer += Time.deltaTime;


		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxis("HorizontalP"+myPlayer) * speed * Time.deltaTime;
		Debug.Log ("I am" +"HorizontalP"+myPlayer);
		transform.position = newPosition;

		//shoot
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
