using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenerateHill : MonoBehaviour {

	public GameObject hillBase;
	public float minLength, maxLength, minRot, maxRot, minPieces, maxPieces;
	public Vector3 startPos, nextPos;
	int currentLevel;
	bool canHit = true;
	public Text levelText;

	void Start () {
		nextPos = startPos;
		Generate();
	}

	void Update () {
	
	}

	void Generate () {

		GameObject hill = Instantiate(hillBase, nextPos, Quaternion.identity) as GameObject;
		Hill hillScript = hill.GetComponent<Hill>();
		hillScript.hill.localScale = new Vector3 (1,1,Random.Range(minLength, maxLength));

		float rot = minRot+(currentLevel+Random.Range(currentLevel-5, currentLevel+5));
		rot = Mathf.Clamp(rot, minRot, maxRot);

		int numberPiece = Mathf.RoundToInt(Random.Range(minPieces, maxPieces));
		for (int i = 0; i < numberPiece; i++) {
			int whichPiece = Random.Range(0, hillScript.hillPieces.Length);
			float randPos = Random.Range(0, hillScript.hill.localScale.z);
			float randHeight = Random.Range (hillScript.pMinH, hillScript.pMaxH);
			GameObject piece = Instantiate(hillScript.hillPieces[whichPiece], Vector3.zero, Quaternion.identity) as GameObject;
			piece.transform.parent = hillScript.hillRot;
			piece.transform.localPosition = new Vector3(0, randHeight, randPos);
			float randRot1 = Random.Range(-hillScript.pMaxRot, hillScript.pMaxRot);
			float randRot2 = Random.Range(-hillScript.pMaxRot, hillScript.pMaxRot);
			piece.transform.localEulerAngles = new Vector3(0, randRot1, randRot2);
		}

		hillScript.hillRot.localEulerAngles = Vector3.left*rot;

		nextPos = hillScript.endPos.position;
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
