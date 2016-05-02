using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool beginMatchOnSceneLoad = false;

	void Start(){
		if (beginMatchOnSceneLoad) {
			startMatch ();
		}
	}

	void startMatch(){
		// Sets the timer for the round
		CountDownScript countDown = GameObject.FindObjectOfType<CountDownScript> ();
		float timeForRound = 30f;
		if (GameObject.FindObjectOfType<TimeBetweenScenes> () != null) {
			timeForRound = GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound;
		} 
		countDown.timeLeft = timeForRound;
		countDown.runScript = true;

		// Starts spawning coins
		CoinSpawningSystem css = GameObject.FindObjectOfType<CoinSpawningSystem> ();
		css.setCoinSpawnEnabled (true);

		// Play the in game music
		GameObject.FindObjectOfType<SoundController>().playInGameMusic();
	}
}
