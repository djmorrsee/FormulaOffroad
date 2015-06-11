// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

/*
    This file contains code for saving a ghost file.
*/

using UnityEngine;

using System.IO;

using System.Collections;
using System.Collections.Generic;

public class GhostSave : MonoBehaviour
{
    public static void SaveGhostData(GhostController.GhostData data, string filename)
    {
        try
        {
            string fullname = Application.persistentDataPath + filename + ".eog";

            string[] lines = new string[data.ticks];
            for (int i = 0; i < data.ticks; ++i)
            {
                float[] tick = data.GetData(i);
                string line = "";
                foreach (float f in tick)
                {
                    line += f + " ";
                }
                lines[i] = line;
            }

            File.WriteAllLines(fullname, lines);
        }
        catch (System.Exception e)
        {
            print("Err " + e.ToString());
        }
    }
}