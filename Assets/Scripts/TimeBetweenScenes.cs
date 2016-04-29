using UnityEngine;
using System.Collections;

public class TimeBetweenScenes : MonoBehaviour {

	public float TimeForRound;

	void Awake() {
		Object.DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
