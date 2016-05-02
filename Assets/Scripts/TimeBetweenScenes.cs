using UnityEngine;
using System.Collections;

public class TimeBetweenScenes : MonoBehaviour {

	public float TimeForRound;
	public bool IsComingFromEndGame;

	void Awake() {
		Object.DontDestroyOnLoad(this);
		IsComingFromEndGame = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
