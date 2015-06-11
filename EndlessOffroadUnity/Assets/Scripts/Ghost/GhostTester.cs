// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

/*
    This file contains code for testing the ghost system.
*/


using UnityEngine;
using System.Collections;

public class GhostTester : MonoBehaviour
{

    public GameObject recordingVehicle;
    public GameObject replayVehicle;

    // Use this for initialization
    void Start()
    {
        StartGhosts();
    }

    void StartGhosts()
    {
        GhostPlayer p = replayVehicle.AddComponent<GhostPlayer>();
        p.PlayGhostFromFile("GhostTest-01");

        GhostRecorder r = recordingVehicle.AddComponent<GhostRecorder>();
        r.StartRecordingGhost();
    }

    void StopGhosts()
    {
        replayVehicle.GetComponent<GhostPlayer>().StopPlayingGhost();
        GhostController.GhostData ghost = recordingVehicle.GetComponent<GhostRecorder>().StopRecordingGhost();
        GhostSave.SaveGhostData(ghost, "GhostTest-01");


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 15)
        {
            StopGhosts();
            print("Done!");
            Destroy(this);
        }
    }
}
