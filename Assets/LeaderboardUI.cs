using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(LeaderboardAPI))]
public class LeaderboardUI : MonoBehaviour {

	public Text loadingText;

	public GameObject leaderboardTextObject;

	public GameObject scoresContainer;

	public GameObject scoresContentContainer;

	private LeaderboardAPI api;

	void Start(){
		scoresContainer.SetActive (false);
		loadingText.enabled = true;
		api = GetComponent<LeaderboardAPI> ();
	}

	public void fetchAndDisplayScores(){
		if (api == null) {
			api = GetComponent<LeaderboardAPI> ();
		}
		StartCoroutine(api.getLeaderboard (1, (scores) => {
			loadingText.enabled = false;
			scoresContainer.SetActive (true);
			RectTransform scoreContainerTransform = scoresContentContainer.GetComponent<RectTransform>();
			float containerWidth = scoreContainerTransform.rect.width;
			float containerHeight = scoreContainerTransform.rect.height;
			float textHeight = containerHeight / 10;
			float contentHeight = textHeight * scores.Count;
			for(int i=0; i<scores.Count; i++){
				LeaderboardAPI.LeaderboardScore score = scores[i];
				GameObject text = Instantiate(leaderboardTextObject) as GameObject;
				text.transform.SetParent(scoresContentContainer.transform,false);
				Text txt = text.GetComponent<Text>();
				txt.text = (i+1) + ". " + score.username + " : " + score.score;
			}
		}, (error, errorData) => {
			Debug.LogError("Leaderboard score retrieval failed");
			loadingText.text = "An error occurred trying to retrieve the scores.";
		}));
	}
}
