using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject playButton, dailyButton, optionsButton, infoButton, moreGamesButton;
	public MenuHandler menuScript;
	public Camera UICam;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			Ray ray = UICam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject == playButton) {
					PlayerPrefs.SetInt("GameMode", 0);
					menuScript.changeMenu(1);
				}
				else if (hit.collider.gameObject == dailyButton) {
					PlayerPrefs.SetInt("GameMode", 1);
					menuScript.changeMenu(1);
				}
				else if (hit.collider.gameObject == optionsButton) {

				}
				else if (hit.collider.gameObject == infoButton) {
					
				}
				else if (hit.collider.gameObject == moreGamesButton) {
					Application.OpenURL("hondune.com");
				}
			}
		}
	}
}
