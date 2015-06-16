using UnityEngine;
using System.Collections;

public class smoothCamera : MonoBehaviour
{
	public Transform target, originTarget;
	public float distance, followSpeed, movementSens, heightHelp, maxMoveDelta, doubleTapSens;
	float startDistance;
	Vector2 lastMousePos;
	Vector3 startRot, lastTargetPos;
	public GameObject camPlane;

	[HideInInspector]
	public float zoom, currentTime;
	public float minimumZoom, touchZoomMultiplier;
	float touchDif2;
	public Camera UICam;
	[HideInInspector]
	public int mode;
	void Awake () {
		startDistance = distance;
	}


	
	void Update () {

		if (target == null) {
			target = originTarget;
		}

		float touchDelta = 0;
		Vector2 startPos = Vector2.zero;
		Vector2 endPos = Vector2.zero;
		foreach (Touch touch in Input.touches) {
			if (touch.fingerId == 0) {
				startPos = touch.position;
			}
			if (touch.fingerId == 1) {
				endPos = touch.position;
			}
		}
		if (Input.touchCount == 2) {
			float touchDif1 = Vector2.Distance (startPos, endPos);
			if (touchDif2 == 0) {
				touchDif2 = touchDif1;
			}
			touchDelta = (touchDif1 - touchDif2) * touchZoomMultiplier;
			touchDif2 = Vector2.Distance (startPos, endPos);
		}
		else { 
			touchDif2 = 0;
		}
		if (Input.GetMouseButtonDown(0)) {
			lastMousePos = Input.mousePosition;
		}

		Vector2 mouseDelta = Vector2.zero;
		float targetDelta = Vector3.Distance(lastTargetPos, target.position);
		targetDelta = targetDelta*heightHelp;

		if (Input.GetMouseButton(0) && Input.touchCount < 2) {
			Ray ray = UICam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject == camPlane) {
				mouseDelta = new Vector2(lastMousePos.x - Input.mousePosition.x, lastMousePos.y - Input.mousePosition.y);
				}
			}
		}
		if (Mathf.Abs(mouseDelta.x) > maxMoveDelta|| Mathf.Abs(mouseDelta.y) > maxMoveDelta) {
			mouseDelta = Vector2.one*maxMoveDelta;
		}

		float relativeHeight = (transform.position.y - target.position.y)/(distance+zoom);

		float yStop = 1;
		if (relativeHeight > 0.98f && mouseDelta.y > 0) {
			yStop = 0;
		}
		if (relativeHeight < -0.98f && mouseDelta.y < 0) {
			yStop = 0;
		}
		relativeHeight = Mathf.Abs(relativeHeight)*0.9f;

		mouseDelta = new Vector3(mouseDelta.x*(movementSens-(movementSens*relativeHeight)+(Mathf.Abs(zoom*0.01f))), mouseDelta.y*movementSens*yStop);
		//print (mouseDelta.x);

		Vector3 local = transform.TransformDirection(mouseDelta);
		//local = new Vector3(mouseDelta.x*local.x*local.y, mouseDelta.x*local.x);
		transform.position += local;

		if (mode == 0) {
			zoom -= (Input.GetAxis("Mouse ScrollWheel")*2) + touchDelta;
			float clampZoom = minimumZoom-distance;
			zoom = Mathf.Clamp (zoom, clampZoom, -clampZoom*1.5f);
		}

		Vector3 wantedPos = (transform.position - target.transform.position).normalized * (distance+zoom) + target.transform.position;

		transform.position = Vector3.Lerp (transform.position, wantedPos+Vector3.up*targetDelta, Time.deltaTime*followSpeed);
		transform.LookAt(target);

		if (Input.GetMouseButton(0)) {
			lastMousePos = Input.mousePosition;
		}
		lastTargetPos = target.position;

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = UICam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject == camPlane) {
			if (Time.time-currentTime <= doubleTapSens) {
				resetCam();
			}
			else {
				currentTime = Time.time;
			}
		}
			}
		}
	}

	public void camReset () {
		Invoke("resetCam", 0.2f);
	}
	void resetCam () {
		transform.position = target.TransformPoint(0, heightHelp*2, distance+zoom);
	}

}
