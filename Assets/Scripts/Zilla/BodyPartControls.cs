using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Kinect = Windows.Kinect;

public class BodyPartControls:MonoBehaviour {
	public GameObject bodyManager;
	public Kinect.JointType joinType;

	private BodySourceManager _BodyManager;

	private Vector3 pos;

	public void Awake() {
		_BodyManager = bodyManager.GetComponent<BodySourceManager>();
	}

	public void Update() {
		Kinect.Body[] data = _BodyManager.GetData();
		if (data == null)
		{
			return;
		}

		foreach(Kinect.Body body in data) {
			if(body == null) {
				return;
			}

			if(body.IsTracked) {
				Kinect.Joint joint = body.Joints[joinType];
				pos.x = joint.Position.X * 50f;
				pos.y = joint.Position.Y * 50f;
				pos.z = transform.position.z;

				transform.position = pos;
			}
		}
	}
}
