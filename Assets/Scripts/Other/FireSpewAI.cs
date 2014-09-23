using UnityEngine;
using System.Collections;

public class FireSpewAI : MonoBehaviour {

	public float speed;
	public bool hascollided;
	private Animator anim;
	
	
	public Vector3 targetDirection;
	
	public void Awake() 
	{
		//anim = GetComponent<Animator>();
		//anim.Play ("bulletAnim");
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if (hascollided) {return;}
		if (targetDirection == Vector3.zero)
		{
			speed = speed/5f;
			targetDirection = Vector3.down;
		}
		Vector3 newPosition = transform.position;
		newPosition = newPosition + targetDirection * speed * Time.deltaTime;
		transform.position = newPosition;
		
	}
	
	void OnBecameInvisible() 
	{
		Destroy(gameObject);
	}
}
