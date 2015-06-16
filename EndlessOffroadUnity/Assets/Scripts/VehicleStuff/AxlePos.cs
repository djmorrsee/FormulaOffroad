using UnityEngine;
using System.Collections;

public class AxlePos : MonoBehaviour {


	public Transform wheel1, wheel2, wheel1Vis, wheel2Vis;
	Vector3 startPos;

	void Start () {
		startPos = transform.localPosition;
	}
	void Update () {
		float posY = (wheel1.localPosition.y+wheel2.localPosition.y)/2;
		transform.localPosition = new Vector3(startPos.x, posY, startPos.z);
		transform.LookAt(wheel1);
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 270, 0);
		wheel1Vis.localRotation = wheel1.localRotation;
		wheel2Vis.localRotation = wheel2.localRotation;
	}
}
 