using UnityEngine;
using System.Collections;

public class ProjectileDestroyed : MonoBehaviour {

	private Animator anim;
	public GameObject trackingobject;
	public Vector3 deltaPosition;

	public void Awake() 
	{
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (trackingobject != null) {
			this.gameObject.transform.position = trackingobject.transform.position + deltaPosition;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		trackingobject = other.gameObject;
		deltaPosition = this.gameObject.transform.position - other.gameObject.transform.position;
		BulletAI bai = this.gameObject.GetComponent<BulletAI> ();
		bai.hascollided = true;
		transform.localScale += new Vector3 (2f, 2f, 0);
		anim.Play ("SmallExplosionAnim");

		//this gameObject can go away
		Invoke("killYourself", 0.3f);
	}

	void killYourself()
	{
		Destroy (this.gameObject);
	}






}
