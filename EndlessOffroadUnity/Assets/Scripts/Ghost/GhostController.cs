// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

/*
    This file contains common code for ghost files.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour
{

    // Internal Representation of a Ghost
    public class GhostData
    {
        public int ticks;
        List<Vector3> positions;
        //List<Vector3> eulerAngles;
		List<Quaternion> rotations;

        public GhostData()
        {
            ticks = 0;
            positions = new List<Vector3>();
            //eulerAngles = new List<Vector3>();
			rotations = new List<Quaternion>();
        }

        public void AddPoint(Vector3 pos, Quaternion rot)
        {
            positions.Add(pos);
            //eulerAngles.Add(rot);
			rotations.Add(rot);
            ++ticks;
        }

        public Vector3 GetPointPos(int i)
        {
            return positions[i];
        }

        public Quaternion GetPointRot(int i)
        {
           // return eulerAngles[i];
			return rotations[i];
        }

        public float[] GetData(int tickNumber)
        {
            if (tickNumber >= ticks)
            {
                return null;
            }
            return new float[7] {
                positions[tickNumber].x, positions[tickNumber].y, positions[tickNumber].z,
                //eulerAngles[tickNumber].x, eulerAngles[tickNumber].y, eulerAngles[tickNumber].z
				rotations[tickNumber].x, rotations[tickNumber].y, rotations[tickNumber].z, rotations[tickNumber].w
            };
        }
    }

    public static int sampleRate = 6; // Take a sample every n frames

}
