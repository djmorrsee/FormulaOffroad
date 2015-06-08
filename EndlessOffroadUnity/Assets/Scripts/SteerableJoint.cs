using UnityEngine;
using System.Collections;

public class SteerableJoint : MonoBehaviour
{

	public ConfigurableJoint joint;
	public float steeringAngle;
	public BetterInput betterInput;
	
	void Update ()
	{
		//print(betterInput.GetAxis ("Horizontal"));
		joint.targetRotation = Quaternion.Euler (Vector3.up * steeringAngle * betterInput.GetAxis ("Horizontal"));
	}
}
