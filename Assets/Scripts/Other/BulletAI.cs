using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

	public float speed;
	public bool hascollided;
	private Animator anim;

	public float lifeDistance = 0f;

	private Vector3 startPosition;

	public Vector3 targetDirection;

	public void Awake() 
	{
		anim = GetComponent<Animator>();
	}

	public void Start(){
		startPosition = this.transform.position;
	}


	// Update is called once per frame
	void Update () 
	{
		if (hascollided) {return;}
			if (targetDirection == Vector3.zero)
			{
				speed = speed/5f;
				targetDirection = Vector3.up;
			}
			Vector3 newPosition = transform.position;
			newPosition = newPosition + targetDirection * speed * Time.deltaTime;
			transform.position = newPosition;

		float distance = Vector3.Distance (this.transform.position, startPosition);

		if (distance > lifeDistance && lifeDistance != 0f) {
			Destroy(gameObject);
		}

		if(targetDirection.x == 0 && targetDirection.y == 0)
		{
			transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
		}
		else
		{
			transform.rotation =  Quaternion.AngleAxis((Mathf.Atan2(targetDirection.y, targetDirection.x) *Mathf.Rad2Deg), Vector3.forward);
		}
	}
	
	void OnBecameInvisible() 
	{
		Destroy(gameObject);
	}
}
