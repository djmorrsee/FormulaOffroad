using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {

	public float force, topSpeed;
	public Vector3 direction;
	public Rigidbody[] wheels;
	public BetterInput betterInput;
	void Start () {
		foreach (Rigidbody wheel in wheels) {
			wheel.maxAngularVelocity = topSpeed;
		}
	
	}

	void Update () {
		foreach (Rigidbody wheel in wheels) {
			wheel.AddRelativeTorque(direction*(force*betterInput.GetAxis ("Vertical")));
		}
	}
}
