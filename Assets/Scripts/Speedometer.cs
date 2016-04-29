using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

	public Text speedText;
	public Rigidbody car;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		speedText.text = "Speed: " + car.velocity;
	}
}
