using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour {

	Text countDownText;
	public bool runScript = false;
	public float timeLeft;

	// Use this for initialization
	void Start () {
		countDownText = this.gameObject.GetComponent<Text> ();
	}

	void Update() {
		if (runScript) {
			timeLeft -= Time.deltaTime;

			int minutes = Mathf.FloorToInt(timeLeft / 60F);
			int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
			int milliseconds = (int)((timeLeft * 1000) % 1000) % 100;

			string niceTime;
			if (minutes < 1) {
				niceTime = string.Format ("{0:0}:{1:00}", seconds, milliseconds);
			} else {
				niceTime = string.Format ("{0:0}:{1:00}", minutes, seconds);
			}


			countDownText.text = niceTime;

			if (timeLeft < 0) {
				countDownText.text = 0f.ToString();
				runScript = false;
				//GameOver();
			}
		}
	}


}
