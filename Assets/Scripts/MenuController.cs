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
	public GameObject StandardMatchTimes;

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<TimeBetweenScenes> ().IsComingFromEndGame) {
			MainMenuCanvas.SetActive (false);
			GameOverScreen.SetActive (true);
			finalScoreField.GetComponent<Text> ().text = GameObject.FindObjectOfType<ScoreBetweenScenes> ().score.ToString();
		} 
	}

	public void navigateToCertainCanvas(bool gameOverScreen, bool leaderboards, bool mainMenuCanvas, bool arMatchTimes, bool mainMenuButtons, bool standardMatchTimes) {
		GameOverScreen.SetActive (gameOverScreen);
		MainMenuCanvas.SetActive (mainMenuCanvas);
		Leaderboards.SetActive (leaderboards);
		ARMatchTimes.SetActive (arMatchTimes);
		mainMenu.SetActive (mainMenuButtons);
		StandardMatchTimes.SetActive (standardMatchTimes);
	}

	public void backToMainMenu() {
		GameObject.Find ("Sounds").GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, true, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goToLeaderBoards() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, true, false, false, false, false);
	}

	public void onPlayARClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, true, false, false);
	}

	public void onPlayStandardClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, false, true);
	}

	public void onBackClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, true, false);
	}

	// Navigate to the AR scene with proper time

	public void onOneMinuteClickAR() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 60f;
		moveToARScene ();
	}

	public void onThreeMinuteClickAR() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 180f;
		moveToARScene ();
	}

	public void onFiveMinuteClickAR() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 300f;
		moveToARScene ();
	}

	// Navigate to standard scene with proper time

	public void onOneMinuteClickStandard() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 60f;
		moveToStandardScene ();
	}

	public void onThreeMinuteClickStandard() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 180f;
		moveToStandardScene ();
	}

	public void onFiveMinuteClickStandard() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 300f;
		moveToStandardScene ();
	}

	public void moveToARScene() {
		StartCoroutine (LevelCoroutine ("ARGame"));
	}

	public void moveToStandardScene() {
		StartCoroutine (LevelCoroutine ("NonARGame"));
	}

	public void navigateToMenu() {
		StartCoroutine (LevelCoroutine ("MainMenu"));
		navigateToCertainCanvas (false, false, true, false, true, false);
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
