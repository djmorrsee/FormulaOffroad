// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

/*
    This file contains code for loading a ghost file.
*/

using UnityEngine;

using System.IO;

using System.Collections;
using System.Collections.Generic;

public class GhostLoad : MonoBehaviour
{
    public static GhostController.GhostData LoadGhostFromFile(string filename)
    {
        GhostController.GhostData newGhost = new GhostController.GhostData();
        string fullname = Application.persistentDataPath + filename + ".eog";


        if (File.Exists(fullname))
        {
            string[] lines = File.ReadAllLines(fullname);

            foreach (string line in lines)
            {
                string[] values = line.Split(' ');
                Vector3 pos = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
                Vector3 rot = new Vector3(float.Parse(values[3]), float.Parse(values[4]), float.Parse(values[5]));

                newGhost.AddPoint(pos, rot);
            }

        }
        else
        {
            throw new IOException("No Such File: " + fullname);
        }
        return newGhost;
    }
}
