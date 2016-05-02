using UnityEngine;
using System.Collections;

public class ScoreBetweenScenes : MonoBehaviour {

	public int score;

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
