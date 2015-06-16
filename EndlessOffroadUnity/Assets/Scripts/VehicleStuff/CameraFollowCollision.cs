using UnityEngine;
using System.Collections;

public class CameraFollowCollision : MonoBehaviour {

	public Transform target, camFollow, vehicle;
	public Vector3 standardOffset, trailerOffset;
	public Vector3 offset;
	public float resetDistance, tapResetSpeed;
	bool camReset;
	float resetTime;
	Vector3 offsetStart;
	public float followSpeed = 5f, collisionSize = 1;
	public Vector2 dragSensitivity = new Vector2(0.5f, 0.01f);
	[HideInInspector]
	public Vector3 hitPoint, lerpPosition;
	float distance;
	Vector3[] directions = new Vector3[6]{Vector3.back, Vector3.forward, Vector3.up, Vector3.down, Vector3.right, Vector3.left};
	public Camera UICam;
	[HideInInspector]
	public bool editorMode;
	Vector2 mouseLast;
	int fingersTouchingLast;
	public GameObject cameraPlane;
	bool follow = true;
	Transform fpView;
	public float fpLerpSpeed;
	float rotationOffset;
	public LayerMask rayMask;
	public BoxCollider camCollider;

	void Start () {
		offset = standardOffset;
		offsetStart = offset;
		camFollow.position = target.position+target.TransformDirection(offset);
		Invoke("setPos", 0.1f);
		if(PlayerPrefs.GetInt("Cam") == 1) {
			switchMode();
		}
	}

	/*void OldStart () {
		offset = standardOffset;
		offsetStart = offset;
		transform.position = target.position+target.TransformDirection(offset);
		lerpPosition = transform.position;
		Invoke("setPos", 0.1f);
		if(PlayerPrefs.GetInt("Cam") == 1) {
			switchMode();
		}
	}*/

	public void resetCam () {
		this.enabled = false;
		Invoke("setPos", 0.1f);
	}

	/*void oldsetPos () {
		//print ("Setting");

		if(GameObject.Find("Trailer(Clone)")){
			offset = trailerOffset;
		}
		else {
			offset = standardOffset;
		}
		offsetStart = offset;

		lerpPosition = transform.position;
		target.localEulerAngles = Vector3.right*90;
		this.enabled = true;
		transform.position = (target.position)+target.TransformDirection(offsetStart);
		//print("reset");
	}*/

	void Update () {
		if (Input.GetKeyDown("c")) {
			switchMode();
		}

		if (follow) {
			FollowCam();
		}
		else {
			FPCam();
		}
	}
	public void switchMode () {
		follow = !follow;
		if (!follow) {
			fpView = GameObject.Find("FPCam").transform;
			GetComponent<Camera>().nearClipPlane = 0.1f;
		}
		else {
			GetComponent<Camera>().nearClipPlane = 1;
			resetCam();
		}
		if(follow)
			PlayerPrefs.SetInt("Cam", 0);
		else
			PlayerPrefs.SetInt("Cam", 1);
	}

	void FollowCam () {

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject == cameraPlane) {
					if(camReset && Time.time-resetTime < tapResetSpeed) {
						resetCam();
						camReset = false;
					}
					else {
						camReset = true;
						resetTime = Time.time;
					}
				}
			}
		}

		Vector3 towardsCam = camFollow.TransformDirection(Vector3.back);
		RaycastHit hit2;
		float distance = Vector3.Distance(target.position, camFollow.position);
		if (Physics.Raycast(target.position, towardsCam, out hit2, distance, rayMask) && !editorMode) {
			Debug.DrawLine(target.position, hit2.point);
			transform.position = Vector3.Lerp(transform.position, hit2.point, Time.deltaTime*followSpeed);
			//camCollider.enabled = true;
		}
		else {
			transform.position = Vector3.Lerp(transform.position, camFollow.position, Time.deltaTime*followSpeed);
			//camCollider.enabled = false;
		}

		transform.LookAt(target);
		camFollow.LookAt(target);
		NewRotater();
	}

	void NewRotater () {

		int fingersTouching = Input.touchCount;
		
		if (Input.GetKey(KeyCode.LeftControl)) {
			fingersTouching = 2;
		}
		if (Input.GetMouseButtonDown(0)) {
			mouseLast = Input.mousePosition;
		}
		
		if (Input.touchCount != fingersTouchingLast) {
			mouseLast = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(0) && ((editorMode && fingersTouching > 1) || !editorMode)) {
			Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject == cameraPlane) {
					Vector2 mouseCurrent = Input.mousePosition;
					Vector2 mouseDelta = mouseCurrent-mouseLast;
					float r = mouseDelta.x*dragSensitivity.x;
					r = Mathf.Clamp( r, -45.0f, 45.0f );
					rotationOffset += r;

					float h = offset.y;
					h -= mouseDelta.y*dragSensitivity.y;
					if (!editorMode)
					h = Mathf.Clamp(h, -1.0f, 6.0f);

					offset = new Vector3 (offset.x, h, offset.z);
				}
			}
			mouseLast = Input.mousePosition;
		}
		camFollow.position = target.position+target.TransformDirection(offset);
		fingersTouchingLast = Input.touchCount;

		target.eulerAngles = new Vector3(0, (vehicle.localEulerAngles.z-90)+rotationOffset, 0);
	}

	void setPos () {
		//print ("Setting");
		
		if(GameObject.Find("Trailer(Clone)")){
			offset = trailerOffset;
		}
		else {
			offset = standardOffset;
		}
		offsetStart = offset;

		rotationOffset = 0;
		this.enabled = true;
		camFollow.position = (target.position)+target.TransformDirection(offsetStart);
		//print("reset");
	}

	/*void OldFollowCam () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject == cameraPlane) {
					if(camReset && Time.time-resetTime < tapResetSpeed) {
						resetCam();
						camReset = false;
					}
					else {
						camReset = true;
						resetTime = Time.time;
					}
				}
			}
		}

		distance = Vector3.Distance(transform.position, target.position);

		if (distance > resetDistance && !editorMode) {
			resetCam();
		}


		Vector3 wantedPosition = target.position+target.TransformDirection(offset);
		wantedPosition = wantedPosition-lerpPosition;

		Vector3 finalStopMove = Vector3.one;
		Vector3 finalFixPos = Vector3.zero;
		for (int i = 0; i < directions.Length; i++) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, directions[i], out hit, collisionSize+0.1f)) {

				Vector3 fixPos = hit.point-directions[i]*collisionSize;
				float basePos = (fixPos.x*directions[i].x) + (fixPos.y*directions[i].y) + (fixPos.z*directions[i].z);
				fixPos = fixPos-lerpPosition;
				Vector3 pos = lerpPosition+wantedPosition;
				float movePos = (pos.x*directions[i].x) + (pos.y*directions[i].y) + (pos.z*directions[i].z);
				float direction = directions[i].x+directions[i].y+directions[i].z;

				if ((direction < 0 && (basePos-movePos)<=0) || (direction > 0 && (movePos-basePos)>=0)){
					Vector3 stopMovement = new Vector3(Mathf.Abs(directions[i].x), Mathf.Abs(directions[i].y), Mathf.Abs(directions[i].z));
					finalStopMove -= stopMovement;
					fixPos = new Vector3 (fixPos.x*Mathf.Abs(directions[i].x), fixPos.y*Mathf.Abs(directions[i].y), fixPos.z*Mathf.Abs(directions[i].z));
					finalFixPos += fixPos;
				}

			}

		}

		finalStopMove = new Vector3 (Mathf.Clamp01(finalStopMove.x), Mathf.Clamp01(finalStopMove.y), Mathf.Clamp01(finalStopMove.z));
		wantedPosition = new Vector3(wantedPosition.x*finalStopMove.x, wantedPosition.y*finalStopMove.y, wantedPosition.z*finalStopMove.z)+finalFixPos;

		lerpPosition += wantedPosition;

		transform.position = Vector3.Lerp(transform.position, lerpPosition, Time.deltaTime*followSpeed);
		//transform.position = lerpPosition;
		transform.LookAt(target);

		rotater();
	}*/

	/*void rotater () {
		int fingersTouching = Input.touchCount;
		
		if (Input.GetKey(KeyCode.LeftControl)) {
			fingersTouching = 2;
		}
		if (Input.GetMouseButtonDown(0)) {
			mouseLast = Input.mousePosition;
		}

		if (Input.touchCount != fingersTouchingLast) {
			/*Vector2 touchPos = Vector2.zero;
			bool set = false;
			foreach(Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					print ("WEEE");
					touchPos += touch.position;
					set = true;
				}
			}
			if (set)
			mouseLast = new Vector2(touchPos.x/fingersTouching, touchPos.y/fingersTouching);*/
			/*mouseLast = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(0) && ((editorMode && fingersTouching > 1) || !editorMode)) {
			Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject == cameraPlane) {
					Vector2 mouseCurrent = Input.mousePosition;
					Vector2 mouseDelta = mouseCurrent-mouseLast;
					float r = mouseDelta.x*dragSensitivity.x;
					r = Mathf.Clamp( r, -45.0f, 45.0f );
					target.Rotate(0,r,0);
					
					
					float h = offset.y;
					h -= mouseDelta.y*dragSensitivity.y;
					h = Mathf.Clamp(h, -1.0f, 6.0f);
					offset = new Vector3 (offset.x, h, offset.z);
				}
			}
			mouseLast = Input.mousePosition;
		}
		target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
		fingersTouchingLast = Input.touchCount;
	}*/

	void FPRotater () {
		int fingersTouching = Input.touchCount;
		
		if (Input.GetKey(KeyCode.LeftControl)) {
			fingersTouching = 2;
		}
		if (Input.GetMouseButtonDown(0)) {
			mouseLast = Input.mousePosition;
		}
		
		if (Input.touchCount != fingersTouchingLast) {
			mouseLast = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(0) && ((editorMode && fingersTouching > 1) || !editorMode)) {
			Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject == cameraPlane) {
					Vector2 mouseCurrent = Input.mousePosition;
					Vector2 mouseDelta = mouseCurrent-mouseLast;
					float r = mouseDelta.x*dragSensitivity.x;
					r = Mathf.Clamp( r, -45.0f, 45.0f );
					fpView.Rotate(0,-r,0, Space.Self);
					float limitRot = fpView.localEulerAngles.x;
					limitRot = Mathf.Clamp(limitRot, 45, 360);
					fpView.localEulerAngles = new Vector3(limitRot, fpView.localEulerAngles.y, fpView.localEulerAngles.z);
					
					
					//float h = offset.y;
					//h -= mouseDelta.y*dragSensitivity.y;
					//h = Mathf.Clamp(h, -1.0f, 6.0f);
					//offset = new Vector3 (offset.x, h, offset.z);
				}
			}
			mouseLast = Input.mousePosition;
		}
		fingersTouchingLast = Input.touchCount;
	}

	void FPCam () {
		transform.position = fpView.position;
		transform.rotation = Quaternion.Lerp(transform.rotation, fpView.rotation, Time.deltaTime*fpLerpSpeed);
		FPRotater();
	}
}
