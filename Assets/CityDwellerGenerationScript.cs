using UnityEngine;
using System.Collections;

public class CityDwellerGenerationScript : MonoBehaviour {

	float deployDwellerTimer;
	public GameObject dweller;

	// Use this for initialization
	void Start () {
		deployDwellerTimer = Random.Range (0f, 2.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (deployDwellerTimer < 0) {
			deployDwellerTimer = Random.Range (1f - GetComponent<PedastalScript>().damage, 4.1f) /(1 + ( GetComponent<PedastalScript>().damage * 3f));

			Vector3 dwellerPosition = Random.insideUnitSphere * 4f;

			Instantiate(dweller, this.transform.position + 
			            new Vector3(dwellerPosition.x, -10f, dwellerPosition.z), Quaternion.identity);
		}
		deployDwellerTimer -= Time.deltaTime;
	}
}
