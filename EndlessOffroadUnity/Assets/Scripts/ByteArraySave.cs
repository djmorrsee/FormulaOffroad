using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Binary Shtuff
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class truckUpgrades {

	[Serializable()]
	public struct myColor {
		public float r;
		public float g;
		public float b;
		public myColor (float r, float g, float b) {
			this.r = r;
			this.g = g;
			this.b = b;
			
		}
	}
	//Color[] colors = new Color[Color.grey, Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.white, Color.yellow];
	public int vehicleType, suspension, tires, rims, engine, bodyKit;
	public string pSuspension, pTires, pRims, pEngine, pBody;
	public myColor vehicleColor = new myColor(UnityEngine.Random.Range(0.05f, 1),UnityEngine.Random.Range(0.05f, 1),UnityEngine.Random.Range(0.05f, 1));
	public myColor rimColor = new myColor(1,1,1);
}

public class ByteArraySave : MonoBehaviour {

	public truckUpgrades[] upgrades;

	public void saveStuff () {
		//Get a binary formatter
		BinaryFormatter binFor = new BinaryFormatter();
		//Create an in memory stream
		MemoryStream mem = new MemoryStream();
		//Save it
		binFor.Serialize(mem, upgrades);
		//Add it to player prefs
		PlayerPrefs.SetString("Stuff", Convert.ToBase64String(mem.GetBuffer()));
	}

	public void loadStuff () {
		//Get the data
		String data = PlayerPrefs.GetString("Stuff");
		//If not blank then load it
		if(!string.IsNullOrEmpty(data))
		{
			//Binary formatter for loading back
			BinaryFormatter binFor = new BinaryFormatter();
			//Create a memory stream with the data
			var mem = new MemoryStream(Convert.FromBase64String(data));
			//Load it back to array
			upgrades = (List<TruckUpgrades>)binFor.Deserialize(mem);
		}
	}
}