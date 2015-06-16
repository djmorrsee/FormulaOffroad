using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float lerpSpeed, lowerCamAmount, minHeight;
	public GenerateHill hillGen;
	Vector3 wantedOffset;

	void Update () {
		Vector3 wantedPos = target.position+wantedOffset;
		transform.position = Vector3.Lerp(transform.position, wantedPos, Time.deltaTime*lerpSpeed);
		transform.LookAt (target);
		if (wantedOffset.y > minHeight);
		wantedOffset = offset-(Vector3.forward*(hillGen.currentLevel*lowerCamAmount));
	}
}
