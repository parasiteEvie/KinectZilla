using UnityEngine;
using System.Collections;

public class KillMePlease : MonoBehaviour {

	// Use this for initialization
	public void PuffOSmoke(){
		particleSystem.Emit (30);
	}

	// Use this for initialization
	public void GrantSweetReleaseOfDeath(){
		Destroy (transform.parent.gameObject);
	}
	
}
