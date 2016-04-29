using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IncrementScore : MonoBehaviour {

	public int score = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Text scoreText = this.gameObject.GetComponent<Text> ();
		scoreText.text = score.ToString();
	}

	//IEnumerator waitForTime() {
		//yield return new WaitForSeconds(1.0f);
		//Text scoreText = this.gameObject.GetComponent<Text> ();
		//scoreText.text = "Score: " + score.ToString();
		//score++;
	//}

	public void addToScore(int increment) {
		score += increment;
	}
}
