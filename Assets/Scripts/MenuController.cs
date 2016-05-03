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
	public GameObject CreditsPage;
	public GameObject dropDown;

	public LeaderboardAPI api;

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<TimeBetweenScenes> ().IsComingFromEndGame) {
			MainMenuCanvas.SetActive (false);
			GameOverScreen.SetActive (true);

			int score = GameObject.FindObjectOfType<ScoreBetweenScenes> ().score;
			LeaderboardAPI.MatchType mType = GameObject.FindObjectOfType<ScoreBetweenScenes> ().matchType;
			api.submitScoreAsync(score, mType, () => {
				Debug.Log ("Submitted score successfully to leaderboard.");
			},
				(error, errorData) => {
					Debug.LogError ("Something went wrong while submitting a score. " + error + ", data: " + errorData.ToString ());
				});


			finalScoreField.GetComponent<Text> ().text = GameObject.FindObjectOfType<ScoreBetweenScenes> ().score.ToString();
		} 
	}

	public void navigateToCertainCanvas(bool gameOverScreen, bool leaderboards, bool mainMenuCanvas, bool arMatchTimes, bool mainMenuButtons, bool standardMatchTimes, bool creditsPage) {
		GameOverScreen.SetActive (gameOverScreen);
		MainMenuCanvas.SetActive (mainMenuCanvas);
		Leaderboards.SetActive (leaderboards);
		ARMatchTimes.SetActive (arMatchTimes);
		mainMenu.SetActive (mainMenuButtons);
		StandardMatchTimes.SetActive (standardMatchTimes);
		CreditsPage.SetActive (creditsPage);
	}

	public void backToMainMenu() {
		GameObject.Find ("Sounds").GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, true, false, false);
	}
		
	// Menu buttons onClick

	public void goToCreditsPage() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, false, false, false, false, true);
	}

	public void goToLeaderBoards() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, true, false, false, false, false, false);
		if (GameObject.FindObjectOfType<ScoreBetweenScenes> () == null) {
			// Brings the user to the default leaderboard (one minute AR) if navigating from main menu
			Leaderboards.GetComponent<LeaderboardUI> ().fetchAndDisplayScores (LeaderboardAPI.MatchType.OneMinuteAR);
		} else {
			// Brings the user to the leaderboard of the match type they were playing if they are coming from a match
			LeaderboardAPI.MatchType mType = GameObject.FindObjectOfType<ScoreBetweenScenes> ().matchType;

			if (mType == LeaderboardAPI.MatchType.OneMinuteAR) { 
				dropDown.GetComponent<Dropdown> ().value = 0;
			} else if (mType == LeaderboardAPI.MatchType.ThreeMinuteAR) {  
				dropDown.GetComponent<Dropdown> ().value = 1;
			} else if (mType == LeaderboardAPI.MatchType.FiveMinuteAR) { 
				dropDown.GetComponent<Dropdown> ().value = 2;
			} else if (mType == LeaderboardAPI.MatchType.OneMinuteNonAR) { 
				dropDown.GetComponent<Dropdown> ().value = 3;
			} else if (mType == LeaderboardAPI.MatchType.ThreeMinuteNonAR) { 
				dropDown.GetComponent<Dropdown> ().value = 4;
			} else if (mType == LeaderboardAPI.MatchType.FiveMinuteNonAR) { 
				dropDown.GetComponent<Dropdown> ().value = 5;
			}
			//Leaderboards.GetComponent<LeaderboardUI> ().fetchAndDisplayScores (GameObject.FindObjectOfType<ScoreBetweenScenes> ().matchType);
		}
	}

	public void onPlayARClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, true, false, false, false);
	}

	public void onPlayStandardClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, false, true, false);
	}

	public void onBackClick() {
		sounds.GetComponent<AudioSource> ().Play ();
		navigateToCertainCanvas (false, false, true, false, true, false, false);
	}

	// Navigate to the AR scene with proper time

	public void onOneMinuteClickAR() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 60f;
		GameObject.FindObjectOfType<TimeBetweenScenes> ().matchType = LeaderboardAPI.MatchType.OneMinuteAR;
		moveToARScene ();
	}

	public void onThreeMinuteClickAR() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 180f;
		GameObject.FindObjectOfType<TimeBetweenScenes> ().matchType = LeaderboardAPI.MatchType.ThreeMinuteAR;
		moveToARScene ();
	}

	public void onFiveMinuteClickAR() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 300f;
		GameObject.FindObjectOfType<TimeBetweenScenes> ().matchType = LeaderboardAPI.MatchType.FiveMinuteAR;
		moveToARScene ();
	}

	// Navigate to standard scene with proper time

	public void onOneMinuteClickStandard() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 60f;
		GameObject.FindObjectOfType<TimeBetweenScenes> ().matchType = LeaderboardAPI.MatchType.OneMinuteNonAR;
		moveToStandardScene ();
	}

	public void onThreeMinuteClickStandard() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 180f;
		GameObject.FindObjectOfType<TimeBetweenScenes> ().matchType = LeaderboardAPI.MatchType.ThreeMinuteNonAR;
		moveToStandardScene ();
	}

	public void onFiveMinuteClickStandard() {
		sounds.GetComponent<AudioSource> ().Play ();
		GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound = 300f;
		GameObject.FindObjectOfType<TimeBetweenScenes> ().matchType = LeaderboardAPI.MatchType.FiveMinuteNonAR;
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
		navigateToCertainCanvas (false, false, true, false, true, false, false);
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
