﻿// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

/*
    This file contains code for replaying ghost data.
*/

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GhostPlayer : MonoBehaviour
{

    GhostController.GhostData ghost;
    bool started = false;
    int thisSample = GhostController.sampleRate;
    int thisTick = 0;

    Vector3 lastPos, nextPos;
    Quaternion lastRot, nextRot;

    public void PlayGhostFromFile(string filename)
    {
        try
        {
            ghost = GhostLoad.LoadGhostFromFile(filename);
            started = true;
        }
        catch (IOException e)
        {
            print("No such file: " + filename + ": " + e.ToString());
        }
    }

    public void StopPlayingGhost()
    {
        started = false;
    }

    void SetForTick(int i)
    {
        if (i < ghost.ticks)
        {
            lastPos = nextPos;
            lastRot = nextRot;

            nextPos = ghost.GetPointPos(i);
            nextRot = ghost.GetPointRot(i);
        }
    }


    void Update ()
    {
        if (started)
        {
            gameObject.transform.position = Vector3.Slerp(lastPos, nextPos, (float)(thisSample) / (float)(GhostController.sampleRate) + Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Lerp(lastRot, nextRot, (float)thisSample / (float)(GhostController.sampleRate) + Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (started)
        {
            if (thisSample++ >= GhostController.sampleRate)
            {
                SetForTick(thisTick++);
                thisSample = 0;
            }
   //         gameObject.transform.position = Vector3.Slerp(lastPos, nextPos, (float)(thisSample) / (float)(GhostController.sampleRate));
			//gameObject.transform.rotation = Quaternion.Lerp(lastRot, nextRot, (float)thisSample / (float)(GhostController.sampleRate));
        }
    }
}
