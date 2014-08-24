using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Kinect = Windows.Kinect;

public class BodyPartControls:MonoBehaviour {
	public GameObject bodyManager;
	public Kinect.JointType joinType;

	public bool bodyPartClosed = false;

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

				// Left Hand
				if(joinType == Kinect.JointType.HandLeft) {
					if(body.HandLeftState == Kinect.HandState.Open) {
						bodyPartClosed = false;
					} else {
						bodyPartClosed = true;
					}
				}
				// Right Hand
				if(joinType == Kinect.JointType.HandRight) {
					if(body.HandRightState == Kinect.HandState.Open) {
						bodyPartClosed = false;
					} else {
						bodyPartClosed = true;
					}
				}
				// Head
				if(joinType == Kinect.JointType.Head) {
					if(body.Expressions[Kinect.Expression.Happy] == Kinect.DetectionResult.Yes) {
						bodyPartClosed = false;
					} else {
						bodyPartClosed = true;
					}
				}
			}
		}
	}
}
