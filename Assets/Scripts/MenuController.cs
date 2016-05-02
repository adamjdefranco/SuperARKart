using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject LoadingScene;
	public Image LoadingBar;
	public GameObject mainMenu;
	public GameObject ARMatchTimes;
	public GameObject sounds;
	public GameObject GameOverScreen;
	public GameObject MainMenuCanvas;
	public GameObject finalScoreField;
	public GameObject Leaderboards;

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<TimeBetweenScenes> ().IsComingFromEndGame) {
			MainMenuCanvas.SetActive (false);
			GameOverScreen.SetActive (true);
			finalScoreField.GetComponent<Text> ().text = GameObject.FindObjectOfType<ScoreBetweenScenes> ().score.ToString();
		} 
	}

	public void navigateToCertainCanvas(bool gameOverScreen, bool leaderboards, bool mainMenuCanvas, bool arMatchTimes, bool mainMenuButtons) {
		GameOverScreen.SetActive (gameOverScreen);
		MainMenuCanvas.SetActive (mainMenuCanvas);
		Leaderboards.SetActive (leaderboards);
		ARMatchTimes.SetActive (arMatchTimes);
		mainMenu.SetActive (mainMenuButtons);
	}

	public void backToMainMenu() {
		GameObject.Find ("Sounds").GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goToLeaderBoards() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, true, false, false, false);
	}

	public void onPlayARClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, true, false);
	}

	public void onBackClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, true);
	}

	public void onOneMinuteClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 60f;
		moveToARScene ();
	}

	public void onThreeMinuteClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 180f;
		moveToARScene ();
	}

	public void onFiveMinuteClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 300f;
		moveToARScene ();
	}

	public void moveToARScene() {
		StartCoroutine (LevelCoroutine ("ARGame"));
	}

	public void navigateToMenu() {
		StartCoroutine (LevelCoroutine ("MainMenu"));
		navigateToCertainCanvas (false, false, true, false, true);
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
