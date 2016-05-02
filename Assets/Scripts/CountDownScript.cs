using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownScript : MonoBehaviour {

	Text countDownText;
	public bool runScript = false;
	public float timeLeft;
	private bool triggerClockLowSound;

	// Use this for initialization
	void Start () {
		triggerClockLowSound = true;
		countDownText = this.gameObject.GetComponent<Text> ();
	}

	// Shows how much time is left in the match
	void Update() {
		if (runScript) {
			timeLeft -= Time.deltaTime;

			int minutes = Mathf.FloorToInt(timeLeft / 60F);
			int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
			int milliseconds = (int)((timeLeft * 1000) % 1000) / 10;

			string niceTime;
			if (minutes < 1) {
				niceTime = string.Format ("{0:0}:{1:00}", seconds, milliseconds);
			} else {
				niceTime = string.Format ("{0:0}:{1:00}", minutes, seconds);
			}

			// Displays the time remaining as text
			countDownText.text = niceTime;

			// Plays sound indicating time is running out
			if (timeLeft < 10 && triggerClockLowSound) {
				GameObject.FindObjectOfType<SoundController> ().playClockLowSound ();
				triggerClockLowSound = false;
			}

			// Game Over
			if (timeLeft < 0) {
				GameObject.FindObjectOfType<SoundController> ().StopClockLowSound ();
				countDownText.text = 0f.ToString();
				runScript = false;
				SceneManager.LoadScene ("MainMenu");

				//Persist score to main menu
				GameObject.FindObjectOfType<TimeBetweenScenes>().IsComingFromEndGame = true;
				GameObject.FindObjectOfType<ScoreBetweenScenes> ().score = (int)GameObject.FindObjectOfType<IncrementScore> ().score;
			}
		}
	}
}
