using UnityEngine;
using System.Collections;

public class BetterInput : MonoBehaviour {
	
	public GameObject upButton, downButton, leftButton, rightButton, upButton2, downButton2, leftButton2, rightButton2, tempRestart;
	public Camera UICam;
	bool useTouch;
	bool mouseTouching;
	float axis1, axis2, axis3, axis4;
	public float fakeAnalogSpeed;
	
	void Start () {
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			useTouch = true;
	}
	
	void Update () {
		if (Input.GetKeyDown("escape")) {
			Application.Quit();
		}
		if (Input.GetKeyDown("r")) {
			Application.LoadLevel(Application.loadedLevel);
		}
		mouseTouching = false;
		if (useTouch) {
			foreach (Touch touch in Input.touches) {
				Ray ray = UICam.ScreenPointToRay (touch.position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					if (hit.collider.gameObject == upButton) {
						axis1 = Mathf.Lerp(axis1, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == downButton) {
						axis1 = Mathf.Lerp(axis1, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else {
						axis1 = Mathf.Lerp(axis1, 0, Time.deltaTime*fakeAnalogSpeed);
					}
					if (hit.collider.gameObject == leftButton) {
						axis2 = Mathf.Lerp(axis2, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == rightButton) {
						axis2 = Mathf.Lerp(axis2, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else {
						axis2 = Mathf.Lerp(axis2, 0, Time.deltaTime*fakeAnalogSpeed);
					}
					if (hit.collider.gameObject == upButton2) {
						axis3 = Mathf.Lerp(axis3, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == downButton2) {
						axis3 = Mathf.Lerp(axis3, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else {
						axis3 = Mathf.Lerp(axis3, 0, Time.deltaTime*fakeAnalogSpeed);
					}
					if (hit.collider.gameObject == leftButton2) {
						axis4 = Mathf.Lerp(axis4, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == rightButton2) {
						axis4 = Mathf.Lerp(axis4, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else {
						axis4 = Mathf.Lerp(axis4, 0, Time.deltaTime*fakeAnalogSpeed);
					}
					if (hit.collider.gameObject == tempRestart) {
						Application.LoadLevel(Application.loadedLevel);
					}
				}
			}
			if (Input.touchCount == 0) {
				axis1 = Mathf.Lerp(axis1, 0, Time.deltaTime*fakeAnalogSpeed);
				axis2 = Mathf.Lerp(axis2, 0, Time.deltaTime*fakeAnalogSpeed);
				axis3 = Mathf.Lerp(axis3, 0, Time.deltaTime*fakeAnalogSpeed);
				axis4 = Mathf.Lerp(axis4, 0, Time.deltaTime*fakeAnalogSpeed);
			}
		}
		else {
			Ray ray = UICam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Input.GetMouseButton(0)) {
				mouseTouching = true;
				if (Physics.Raycast (ray, out hit, 100)) {
					if (hit.collider.gameObject == upButton) {
						axis1 = Mathf.Lerp(axis1, 1, Time.deltaTime*fakeAnalogSpeed);
					}
					else if (hit.collider.gameObject == downButton) {
						axis1 = Mathf.Lerp(axis1, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == leftButton) {
						axis2 = Mathf.Lerp(axis2, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == rightButton) {
						axis2 = Mathf.Lerp(axis2, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					if (hit.collider.gameObject == upButton2) {
						axis3 = Mathf.Lerp(axis3, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == downButton2) {
						axis3 = Mathf.Lerp(axis3, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == leftButton2) {
						axis4 = Mathf.Lerp(axis4, -1, Time.deltaTime*fakeAnalogSpeed);	
					}
					else if (hit.collider.gameObject == rightButton2) {
						axis4 = Mathf.Lerp(axis4, 1, Time.deltaTime*fakeAnalogSpeed);	
					}
					if (hit.collider.gameObject == tempRestart) {
						Application.LoadLevel(Application.loadedLevel);
					}
				}
			}
			else {
					axis1 = Mathf.Lerp(axis1, 0, Time.deltaTime*fakeAnalogSpeed);
					axis2 = Mathf.Lerp(axis2, 0, Time.deltaTime*fakeAnalogSpeed);
					axis3 = Mathf.Lerp(axis3, 0, Time.deltaTime*fakeAnalogSpeed);
					axis4 = Mathf.Lerp(axis4, 0, Time.deltaTime*fakeAnalogSpeed);
				}
		}
		//print (axis1);
	}
	public float GetAxis(string axisName) {
		if (axisName == "Vertical") {
			if (useTouch || mouseTouching)
			return axis1;
			else
			return Input.GetAxis("Vertical");
		}
		if (axisName == "Horizontal"){
			if (useTouch || mouseTouching)
			return axis2;
			else
			return Input.GetAxis("Horizontal");
		}
		if (axisName == "Vertical2") {
			if (useTouch || mouseTouching)
			return axis3;
			else
			return Input.GetAxis("Vertical2");
		}
		if (axisName == "Horizontal2"){
			if (useTouch || mouseTouching)
			return axis4;
			else
			return Input.GetAxis("Horizontal2");
		}
		else {
			return 0;
		}
	}
}
