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

	void OnColliderEnter(Collider other)
	//void OnCollisionEnter(Collision other)
	{
		Debug.Log("collided with a thing" +other.gameObject.ToString());
		if(other.gameObject.tag == "BossHead")
		{
			//TODO: Display the shot landed animation.
			
			//gun_boss_damage_spark0001
			
			//TODO: Add sound effect for boss damage
				//AudioSource.Play(boss damage);
				
			//remove life from the boss\
			Head bs = other.gameObject.GetComponent<Head>();
			bs.DealDamage();
			
			//this gameObject can go away
			Destroy(this.gameObject);
		}
	}
}
