using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenerateHill : MonoBehaviour {

	public GameObject hillBase;
	public Vector3 startPos, nextPos;
	public int currentLevel;
	bool canHit = true;
	public Text levelText;
	public GameObject currentHill, lastHill, hillToDelete;

	void Start () {
		nextPos = startPos;
		Generate();
	}

	void Update () {
	
	}

	void Generate () {

		GameObject hill = Instantiate(hillBase, nextPos, Quaternion.identity) as GameObject;
		Hill hillScript = hill.GetComponent<Hill>();
		hillScript.hill.localScale = new Vector3 (1,1,Random.Range(hillScript.minLength, hillScript.maxLength));

		float rot = hillScript.minRot+(currentLevel+Random.Range(currentLevel-5, currentLevel+5));
		rot = Mathf.Clamp(rot, hillScript.minRot, hillScript.maxRot);

		int numberPiece;
		if (hillScript.hill.localScale.z < hillScript.maxLength/4)
			numberPiece = Mathf.RoundToInt(Random.Range(hillScript.minPieces, hillScript.maxPieces/2));
		else
			numberPiece = Mathf.RoundToInt(Random.Range(hillScript.minPieces, hillScript.maxPieces));

		for (int i = 0; i < numberPiece; i++) {
			int whichPiece = Random.Range(0, hillScript.obstacles.Length);

			GameObject piece = Instantiate(hillScript.obstacles[whichPiece], Vector3.zero, Quaternion.identity) as GameObject;
			Obstacle pieceInfo = piece.GetComponent<Obstacle>();
			piece.transform.parent = hillScript.hillRot;

			float randZ = Random.Range(0, hillScript.hill.localScale.z);
			float randY = Random.Range (pieceInfo.minHeight, pieceInfo.maxHeight);
			float randX = Random.Range (pieceInfo.minHorizontal, pieceInfo.maxHorizontal);
			piece.transform.localPosition = new Vector3(randX, randY, randZ);

			float randRot1 = Random.Range(pieceInfo.minRotation, pieceInfo.maxRotation);
			float randRot2 = Random.Range(pieceInfo.minRotation, pieceInfo.maxRotation);
			piece.transform.localEulerAngles = new Vector3(0, randRot1, randRot2);
		}
		hillScript.obstaclesParent.SetActive(false);
		hillScript.hillRot.localEulerAngles = Vector3.left*rot;

		nextPos = hillScript.endPos.position;

		DeleteHill(hill);

	}

	void DeleteHill (GameObject hill) {
		if (hillToDelete)
			Destroy(hillToDelete);

		hillToDelete = lastHill;

		lastHill = currentHill;

		currentHill = hill;

	}

	void OnTriggerEnter (Collider other) {
		if (canHit) {
			transform.position = nextPos;
			Generate();
			currentLevel++;
			canHit = false;
			Invoke("setState", 2);
			levelText.text = "Level: "+currentLevel;
		}
	}

	void setState () {
		canHit = true;
	}
}
