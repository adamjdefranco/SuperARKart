using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayARScript : MonoBehaviour {

	public GameObject LoadingScene;
	public Image LoadingBar;
	public GameObject mainMenu;
	public GameObject ARMatchTimes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPlayARClick() {
		mainMenu.SetActive (false);
		ARMatchTimes.SetActive (true);
	}

	public void onBackClick() {
		ARMatchTimes.SetActive (false);
		mainMenu.SetActive (true);
	}

	public void onOneMinuteClick() {
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 60f;
		moveToARScene ();
	}

	public void onThreeMinuteClick() {
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 180f;
		moveToARScene ();
	}

	public void onFiveMinuteClick() {
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 300f;
		moveToARScene ();
	}

	public void moveToARScene() {
		StartCoroutine (LevelCoroutine ("ARGame"));
	}

	public void navigateToMenu() {
		Debug.Log ("NavigateToMenuClicked");
		StartCoroutine (LevelCoroutine ("MainMenu"));
	}

	// Loads the next level and shows a loading bar in the meantime
	IEnumerator LevelCoroutine (string scene) {
		LoadingScene.SetActive (true);
		AsyncOperation async = SceneManager.LoadSceneAsync (scene);

		while (!async.isDone) {
			LoadingBar.fillAmount = async.progress / 0.9f;
			yield return null;
		}
	}
}
