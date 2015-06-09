using UnityEngine;
using System.Collections;

public class CenterOfMass : MonoBehaviour {

	public Vector3 centerOfMass;
	public Rigidbody rigidBody;
	void Start () {
		rigidBody.centerOfMass = centerOfMass;
	}
}
