using UnityEngine;
using System.Collections;

public class TestLeaderboardCode : MonoBehaviour
{

	LeaderboardAPI api;

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.DeleteAll ();
		api = FindObjectOfType<LeaderboardAPI> ();
		StartCoroutine(doLogin(api));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	IEnumerator doLogin(LeaderboardAPI api){
		bool success = false;
		yield return api.logInPlayer ("james", "password1", () => {
			Debug.Log("Success! We logged in as james.");
			success = true;
		}, logFailure);
		if (!success) {
			yield break;
		}
		yield return api.submitScore (6000, LeaderboardAPI.MatchType.OneMinuteAR, () => {
			Debug.Log("Score submitted successfully.");
		}, logFailure);
		yield return api.getLeaderboard(0,(scores)=>{
			foreach(LeaderboardAPI.LeaderboardScore score in scores){
				Debug.Log("Retrieved a new score: "+score.score+" for match type "+score.type.ToString());
			}
		},logFailure);
	}

	void logFailure(string error, JSONObject errorData){
		Debug.Log ("Error while performing an API request! " + error + ", Data: " + errorData.ToString ());
	}


}
