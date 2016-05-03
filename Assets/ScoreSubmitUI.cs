using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSubmitUI : MonoBehaviour
{

	public LeaderboardAPI api;
	public InputField usernameText;
	public Button submitButton;
	public Button mainMenuButton;
	public Button leaderboardButton;

	private string oldUsername;

	void Start ()
	{
		oldUsername = api.SignedInAs;
		//Set textfield to have current logged in username here.
	}

	public void submitScoreAsync(){
		submitButton.enabled = false;
		mainMenuButton.enabled = false;
		leaderboardButton.enabled = false;
		StartCoroutine (submitScore ());
	}

	IEnumerator submitScore ()
	{
		string newUsername = usernameText.text;
		if (newUsername.Equals (null) || newUsername.Equals ("")) {
			mainMenuButton.enabled = true;
			leaderboardButton.enabled = true;
			submitButton.enabled = true;
			yield break;
		}
		if (!oldUsername.Equals (newUsername)) {
			yield return signInAsUsername (newUsername);
		}

		int score = GameObject.FindObjectOfType<ScoreBetweenScenes> ().score;
		LeaderboardAPI.MatchType mType = GameObject.FindObjectOfType<ScoreBetweenScenes> ().matchType;
		yield return api.submitScore(score, mType, () => {
			Debug.Log ("Submitted score successfully to leaderboard.");
			mainMenuButton.enabled = true;
			leaderboardButton.enabled = true;
		},
			(error, errorData) => {
				Debug.LogError ("Something went wrong while submitting a score. " + error + ", data: " + errorData.ToString ());
				mainMenuButton.enabled = true;
				leaderboardButton.enabled = true;
				submitButton.enabled = true;
			});

	}

	IEnumerator signInAsUsername (string username)
	{
		yield return api.signOutPlayer (null, null);
		yield return api.registerPlayer (username, "email@example.com", api.DeviceIdentifier, api.DeviceIdentifier, null, null);
		yield return api.logInPlayer (username, api.DeviceIdentifier, null, showUsernameErrorDialog);
	}

	void showUsernameErrorDialog (string error, JSONObject errorData)
	{

	}
}
