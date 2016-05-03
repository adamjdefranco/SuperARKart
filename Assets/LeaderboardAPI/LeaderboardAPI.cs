using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Globalization;

public class LeaderboardAPI : MonoBehaviour {

	[SerializeField]
	private string APIBaseUrl = "http://ec2-54-200-141-238.us-west-2.compute.amazonaws.com/api/";

	[SerializeField]
	public string appKey = "699287b0-f9bf-3bf3-92f6-558b5e31d519";

	private const string devicePreferenceKey = "device_uuid";
	private const string isSignedInAsKey = "logged_in_as";

	private string deviceID;

	public string DeviceIdentifier {
		get {
			return deviceID;
		}
	}

	public string SignedInAs {
		get {
			return PlayerPrefs.GetString (isSignedInAsKey, null);
		}
	}

	IEnumerator WaitForRequest(string url, JSONObject data, Action<JSONObject> lambda, Action<string, JSONObject> error)
	{
		Dictionary<string,string> headers = new Dictionary<string,string>();
		headers.Add("Content-Type", "application/json");
		headers.Add("Accept", "application/json");
		byte[] body = Encoding.UTF8.GetBytes(data.ToString());
		WWW conn = new WWW(url,body, headers);
		yield return conn;
			// check for errors
		if (conn.error == null)
			{
			JSONObject obj = new JSONObject (conn.text);
				lambda (obj);
			} else {
			error (conn.error, new JSONObject(conn.text));
			}    
	}

	IEnumerator WaitUntilDeviceRegistered(){
		if (deviceID != null) {
			yield break;
		} else if (PlayerPrefs.HasKey (devicePreferenceKey)) {
			Debug.Log ("Pulling device ID from player prefs");
			deviceID = PlayerPrefs.GetString (devicePreferenceKey);
			Debug.Log ("We found the device ID in player prefs");
			yield break;
		} else {
			Debug.Log ("Retrieving new device ID from server.");
			string url = APIBaseUrl + "devices/register";
			JSONObject form = new JSONObject ();
			form.AddField ("app_key", appKey);
			yield return WaitForRequest(url, form, (JSONObject obj)=> {
				deviceID = obj.GetField("device_id").str;
				PlayerPrefs.SetString(devicePreferenceKey,deviceID);
				Debug.Log("Device successfully registered with server.");
			}, (error, data)=>{
				Debug.Log("Error while registering device: "+error);
			});
		}
	}

	void Start(){
		StartCoroutine (WaitUntilDeviceRegistered());
	}

	public IEnumerator registerPlayer(string username, string email, string password, string password_confirm, Action<JSONObject> successFunction, Action<string, JSONObject> errorFunction){
		yield return WaitUntilDeviceRegistered ();
		string url = APIBaseUrl + "register";
		JSONObject form = newAPIForm();
		form.AddField ("username", username);
		form.AddField ("email", email);
		form.AddField ("password", password);
		form.AddField ("password_confirmation", password_confirm);
		yield return WaitForRequest (url,form, successFunction, errorFunction);
	}

	public IEnumerator logInPlayer(string username, string password, Action successFunction, Action<string,JSONObject> errorFunction){
		if(PlayerPrefs.HasKey(isSignedInAsKey)){
			errorFunction("Already logged in! Need to log out first.",new JSONObject());
			yield break;
		}
		yield return WaitUntilDeviceRegistered ();
		string url = APIBaseUrl + "devices/login";
		JSONObject form = newAPIForm ();
		form.AddField ("username", username);
		form.AddField ("password", password);
		yield return WaitForRequest (url,form, (obj)=>{
			PlayerPrefs.SetString(isSignedInAsKey,username);
			successFunction();
		}, errorFunction);
	}

	public IEnumerator signOutPlayer(Action successFunction, Action<string,JSONObject> errorFunction){
		if(!PlayerPrefs.HasKey(isSignedInAsKey)){
			errorFunction("Not signed in.",new JSONObject());
			yield break;
		}
		yield return WaitUntilDeviceRegistered ();
		string url = APIBaseUrl + "devices/logout";
		JSONObject form = newAPIForm ();
		yield return WaitForRequest (url,form, (obj)=>{
			PlayerPrefs.DeleteKey(isSignedInAsKey);
			successFunction();
		}, errorFunction);
	}

	public void submitScoreAsync(int score, MatchType type, Action successFunction, Action<string, JSONObject> errorFunction){
		StartCoroutine(submitScore(score,type,successFunction,errorFunction));
	}

	public IEnumerator submitScore(int score, MatchType type, Action successFunction, Action<string,JSONObject> errorFunction){
		yield return WaitUntilDeviceRegistered ();
		Debug.Log ("Submitting a score to the leaderboards.");
		string url = APIBaseUrl + "leaderboard/submit";
		JSONObject form = newAPIForm ();
		form.AddField ("score", score);
		form.AddField ("match_type", type.ToString ());
		yield return WaitForRequest (url,form, (obj)=>successFunction(), errorFunction);
	}

	public IEnumerator getLeaderboard(int page, Action<List<LeaderboardScore>> successFunction, Action<string,JSONObject> errorFunction){
		yield return WaitUntilDeviceRegistered ();
		Debug.Log ("We are requesting things from the leaderboard");
		string url = APIBaseUrl + "leaderboard?page="+page;
		JSONObject form = newAPIForm();
//		form.AddField ("match_type", type.ToString ());
		yield return WaitForRequest (url,form,(obj)=>{
			Debug.Log("We made it here!");
			List<LeaderboardScore> scores = new List<LeaderboardScore>();
			JSONObject scorePiece = obj.GetField("data");
			foreach(JSONObject s in scorePiece.list){
				int score = (int)s.GetField("score").n;
				MatchType type = MatchType.valueOf(s.GetField("match_type").str);
				string username = s.GetField("username").str;
				DateTime date = DateTime.ParseExact(s.GetField("uploaded_at").str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
				scores.Add(new LeaderboardScore(score,username,type,date));
			}
			successFunction(scores);
		}, errorFunction);
	}

	private JSONObject newAPIForm(){
		JSONObject form = new JSONObject();
		form.AddField ("app_key", appKey);
		form.AddField ("device_id", deviceID);
		return form;
	}

	public class MatchType {
		public static readonly MatchType OneMinuteAR = new MatchType ("1m-AR", true);
		public static readonly MatchType ThreeMinuteAR = new MatchType ("3m-AR", true);
		public static readonly MatchType FiveMinuteAR = new MatchType ("5m-AR",true);
		public static readonly MatchType OneMinuteNonAR = new MatchType ("1m-NAR",false);
		public static readonly MatchType ThreeMinuteNonAR = new MatchType ("3m-NAR",false);
		public static readonly MatchType FiveMinuteNonAR = new MatchType ("5m-NAR",false);
		public static readonly MatchType Unknown = new MatchType ("Unknown", false);

		private readonly string code;
		private readonly bool isAR;

		private MatchType(string code, bool isAR){
			this.code = code;
			this.isAR = isAR;
		}

		public override string ToString(){
			return this.code;
		}

		public bool isARMode(){
			return this.isAR;
		}

		public static MatchType valueOf(string type){
			switch (type) {
				case "1m-AR":
					return OneMinuteAR;
				case "3m-AR":
					return ThreeMinuteAR;
				case "5m-AR":
					return FiveMinuteAR;
				case "1m-NAR":
					return OneMinuteNonAR;
				case "3m-NAR":
					return ThreeMinuteNonAR;
				case "5m-NAR":
					return FiveMinuteNonAR;
				default:
					return Unknown;
			}
		}
	}

	public class LeaderboardScore {
		public readonly MatchType type;
		public readonly string username;
		public readonly DateTime uploadTime;
		public readonly int score;

		public LeaderboardScore(int score, string username, MatchType type, DateTime uploadTime){
			this.type = type;
			this.username = username;
			this.uploadTime = uploadTime;
			this.score = score;
		}
	}

}
