using UnityEngine;
using System.Collections;
using Vuforia;

public class StopExpanding : MonoBehaviour {

	private SmartTerrainTracker mTracker;
	private ReconstructionBehaviour mReconstructionBehaviour;
	public GameObject greenKart;
	public GameObject newCar;
	public GameObject bottomPanel;

	public GameObject goButton;
	public GameObject resetButton;
	public GameObject topPanel;
	public GameObject menuButton;

	public GameObject thisSurface;

	// Use this for initialization
	void Start () {
		mReconstructionBehaviour = GetComponent<ReconstructionBehaviour>();
		mTracker = TrackerManager.Instance.GetTracker<SmartTerrainTracker>();


		//StartCoroutine(WaitForTenSeconds());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			stopReconstruction ();
		}
	}

	/*IEnumerator WaitForTenSeconds() {
		Debug.Log("In the CoRoutine");
		yield return new WaitForSeconds(10);
		Debug.Log("Ten Seconds Over");
		if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null))
		{
			Debug.Log("Stopping the reconstruction");
			mReconstructionBehaviour.Reconstruction.Stop();
		}
	}*/

	void stopReconstruction() {
		if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null))
		{
			Debug.Log("Stopping the reconstruction");
			mReconstructionBehaviour.Reconstruction.Stop();
		}

		topPanel.SetActive (!topPanel.activeInHierarchy);
		//menuButton.SetActive (!menuButton.activeInHierarchy);

		Animator meshToClearAnimator = this.GetComponentInChildren<Animator> ();
		meshToClearAnimator.Play ("FadeMeshToClear");

		greenKart.SetActive (true);
		//newCar.SetActive(true);

		CountDownScript countDown = GameObject.FindObjectOfType<CountDownScript> ();

		float timeForRound = 30f;
		if (GameObject.FindObjectOfType<TimeBetweenScenes> () != null) {
			timeForRound = GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound;
		} 
		countDown.timeLeft = timeForRound;
		countDown.runScript = true;

		CoinSpawningSystem css = GameObject.FindObjectOfType<CoinSpawningSystem> ();
		css.setCoinSpawnEnabled (true);

		//GameObject.Find ("GoButton").SetActive (false);

		goButton.SetActive (!goButton.activeInHierarchy);
		resetButton.SetActive (!resetButton.activeInHierarchy);

		bottomPanel.SetActive (true);
	}

	public void resetReconstruction() {
		if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null))
		{
			Debug.Log ("In If");

			bool trackerWasActive = mTracker.IsActive;
			// first stop the tracker
			if (trackerWasActive)
				mTracker.Stop();
			// now you can reset...
			mReconstructionBehaviour.Reconstruction.Reset();
			// ... and restart the tracker
			if (trackerWasActive)
			{
				mTracker.Start();
				mReconstructionBehaviour.Reconstruction.Start();
			}
		}
	}

	public void iosButtonWorking() {
		Debug.Log ("iOS button was clicked!");
		stopReconstruction ();
	}
}
