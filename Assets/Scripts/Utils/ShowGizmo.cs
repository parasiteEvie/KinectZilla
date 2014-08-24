using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class ShowGizmo:MonoBehaviour {
	// Draw Gizmo
	public void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.position, 2f);
	}
}
