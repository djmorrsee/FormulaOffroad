// Script created by Daniel Morrissey
// Copyrights may apply, see project LICENSE
//
// If changes are made to this code, please email djmorrsee@gmail.com

using UnityEngine;
using System.Collections;

public class WWW_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public delegate void WWW_Callback(string message);
    public static IEnumerator PostHighScoreForSeed(int score, int seed, WWW_Callback callback)
    {
        string test = "test";
        byte[] testb = System.Text.Encoding.Unicode.GetBytes(test);
        WWW post = new WWW("10.0.0.10:5555", testb);
        yield return new WaitForSeconds(1.0f);
        yield return post;
        if(post.error != null)
        {
            callback(post.error);
        } else
        {
            callback(post.text);
        }
    }

    public static IEnumerator GetHighScoresForSeed(int score)
    {
        yield return null;
    }
}
