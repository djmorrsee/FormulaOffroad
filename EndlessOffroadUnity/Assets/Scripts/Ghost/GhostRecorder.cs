// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

/*
    This file contains code for recording ghost data.
*/

using UnityEngine;
using System.Collections;

public class GhostRecorder : MonoBehaviour
{

    public bool started = false;
    int thisTick = GhostController.sampleRate;

    GhostController.GhostData ghostData = new GhostController.GhostData();

    public void StartRecordingGhost()
    {
        started = true;
    }

    public GhostController.GhostData StopRecordingGhost()
    {
        started = false;
        return ghostData;
    }

    void FixedUpdate()
    {
        if (started)
        {
            if (thisTick++ >= GhostController.sampleRate)
            {
                SaveTick();
                thisTick = 0;
            }
        }
    }

    void SaveTick()
    {
        print(gameObject.transform.position);
        ghostData.AddPoint(gameObject.transform.position, gameObject.transform.rotation);
    }
}
