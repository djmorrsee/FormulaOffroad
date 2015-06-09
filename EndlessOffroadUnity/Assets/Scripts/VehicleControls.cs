using UnityEngine;
using System.Collections;

public class VehicleControls : MonoBehaviour {

	public float force, topSpeed, airControlForce, spring, damp, maxForce;
	public Rigidbody mainBody;
	public Rigidbody[] wheels;
	public Vector3 direction;
	public BetterInput betterInput;

	void Start () {
		SetStats();
	}

	public void SetStats () {
		JointDrive driveSettings = new JointDrive();
		driveSettings.mode = JointDriveMode.PositionAndVelocity;
		driveSettings.positionSpring = spring;
		driveSettings.positionDamper = damp;
		driveSettings.maximumForce = maxForce;
		foreach (Rigidbody wheel in wheels) {
			wheel.maxAngularVelocity = topSpeed;
			ConfigurableJoint joint = wheel.GetComponent<ConfigurableJoint>();
			joint.yDrive = driveSettings;
		}
		
	}

	void FixedUpdate () {
		foreach (Rigidbody wheel in wheels) {
			wheel.AddRelativeTorque(direction*(force*betterInput.GetAxis ("Vertical")));
		}
		mainBody.AddRelativeTorque(Vector3.right*(betterInput.GetAxis ("Vertical")*airControlForce));
	}
}
