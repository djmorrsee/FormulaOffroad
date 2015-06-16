using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	public GameObject[] Menus;
	public GameObject nextButton, backButton;
	int currentMenu;
	public Camera UICam;

	void Start () {
	
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = UICam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject == nextButton) {
					changeMenu(1);
				}
				else if (hit.collider.gameObject == backButton) {
					changeMenu(-1);
				}
			}
		}
	}

	public void changeMenu (int change) {
		Menus[currentMenu].SetActive(false);
		currentMenu += change;
		Menus[currentMenu].SetActive(true);
		if (currentMenu == 0 || currentMenu == Menus.Length) {
			backButton.SetActive(false);
			nextButton.SetActive(false);
		}
		else {
			backButton.SetActive(true);
			nextButton.SetActive(true);
		}
	}
}
