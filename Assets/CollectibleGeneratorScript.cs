using UnityEngine;
using System.Collections;

public enum CollectibleType 
{
	BOMB,
	HEALTH_PACK
}

public class CollectibleGeneratorScript : MonoBehaviour {

	//float genTime;
	float timer;

	public GameObject healthPack;
	public GameObject bomb;

	// Use this for initialization
	void Start () {
		GetRandomGenTime();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(timer > 0f)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			GetRandomGenTime();
			//generate a collectible
			if (Random.Range(0,2) == 0)
			{
			GameObject.Instantiate(healthPack, new Vector3((float)Random.Range (-30f, 30f), 
				                                               (float)Random.Range (-10.5f, -7.5f),
				                                               (float)Random.Range (15f, 21f)), Quaternion.identity);
			}
			else
			{
				Debug.Log ("Bomb");
				GameObject.Instantiate(bomb, new Vector3((float)Random.Range (-30f, 30f), 
				                                               (float)Random.Range (-10.5f, -7.5f),
				                                               (float)Random.Range (15f, 21f)), Quaternion.identity);
			}
		}
	
	}

	void GetRandomGenTime () {
		timer = (float)Random.Range (10, 20);
	}
}
