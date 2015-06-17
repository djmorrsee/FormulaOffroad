// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

using UnityEngine;
using System.Collections;

public class GhostWWW : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PostGhostForSeed(new GhostController.GhostData(), 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void PostGhostForSeed(GhostController.GhostData ghost, int seed)
    {
        StartCoroutine(WWW_Controller.PostHighScoreForSeed(10, 10, GetGhostCallback));
    }

    void GetGhostsForSeed(int seed)
    {
        
    }

    void GetGhostCallback(string message)
    {
        print(message);
        StartCoroutine(WWW_Controller.PostHighScoreForSeed(10, 10, GetGhostCallback));
    }
}
