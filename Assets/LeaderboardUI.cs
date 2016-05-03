using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour {

	public Text loadingText;

	public GameObject leaderboardTextObject;

	public GameObject scoresContainer;

	public GameObject scrollViewport;

	public GameObject scoresContentContainer;

	public LeaderboardAPI api;

	void Start(){
		scoresContainer.SetActive (false);
		loadingText.enabled = true;
	}

	public void myDropdownValueChangedHandler(Dropdown target) {
		switch (target.value) {
		case 0: 
			fetchAndDisplayScores (LeaderboardAPI.MatchType.OneMinuteAR);
			break;
		case 1: 
			fetchAndDisplayScores (LeaderboardAPI.MatchType.ThreeMinuteAR);
			break;
		case 2: 
			fetchAndDisplayScores (LeaderboardAPI.MatchType.FiveMinuteAR);
			break;
		case 3: 
			fetchAndDisplayScores (LeaderboardAPI.MatchType.OneMinuteNonAR);
			break;
		case 4: 
			fetchAndDisplayScores (LeaderboardAPI.MatchType.ThreeMinuteNonAR);
			break;
		case 5: 
			fetchAndDisplayScores (LeaderboardAPI.MatchType.FiveMinuteNonAR);
			break;
		}
		Debug.Log("selected: "+target.value);
	}

	public void fetchAndDisplayScores(LeaderboardAPI.MatchType matchType){
		foreach(Transform child in scoresContentContainer.transform){
			Destroy(child.gameObject);
		}
		scoresContainer.SetActive (false);
		loadingText.enabled = true;
		StartCoroutine(api.getLeaderboard (1, matchType, (scores) => {
			if(scores.Count == 0){
				loadingText.text = "There are no scores for this match type!";
				return;
			}
			loadingText.enabled = false;
			scoresContainer.SetActive (true);
			RectTransform scoreContainerTransform = scrollViewport.GetComponent<RectTransform>();
			RectTransform contentContainerTransform = scoresContentContainer.GetComponent<RectTransform>();
			float containerWidth = scoreContainerTransform.rect.width;
			float containerHeight = scoreContainerTransform.rect.height;
			float textHeight = containerHeight / 10;
			float contentHeight = textHeight * scores.Count;
			contentContainerTransform.sizeDelta = new Vector2(1,contentHeight);
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
