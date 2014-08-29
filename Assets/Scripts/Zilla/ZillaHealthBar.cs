using UnityEngine;
using System.Collections;

public class ZillaHealthBar : MonoBehaviour {
	public int maxHealth = 350;
	private int curHealth;
	private int lastHealth;
	private int changeInHealth;
	private float percentHealth;
	
	private GUIStyle backgroundStyle = null;
	private GUIStyle foregroundStyle = null;
	
	// Use this for initialization
	void Start () {
		// Update is called once per frame
		lastHealth = maxHealth;
	}
	void Update () {

	}

	public void AdjustCurrentHealth(int lifePoints)
	{

		//lasthealth tracks his health prior to the current Adjust cycle.
		changeInHealth = lastHealth - lifePoints;
		curHealth -= changeInHealth;
		lastHealth = curHealth;
		
		if(curHealth <0)
			curHealth = 0;
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth <1)
			maxHealth = 1;


		//Calculate health bar percentage
		percentHealth = ((float)curHealth) / ((float)maxHealth) * (Screen.width / 3);	

	}

	void OnGUI () 
	{
		InitStyles();

		// Draw the background image
		GUI.Box (new Rect (Screen.width/2-(Screen.width/3)/2,10, Screen.width/3,32), "", backgroundStyle);

		
		if (percentHealth > 2)
		// Draw the foreground image
		GUI.Box (new Rect (Screen.width/2-(Screen.width/3)/2,10, percentHealth,32), "", foregroundStyle);

	}


	private void InitStyles()
	{
		if( backgroundStyle == null )
		{
			backgroundStyle = new GUIStyle( GUI.skin.box );
			backgroundStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0f, 0f, 1f ) );
		}
		if( foregroundStyle == null )
		{
			foregroundStyle = new GUIStyle( GUI.skin.box );
			foregroundStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0f, 0f, 1f ) );
		}
	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
}