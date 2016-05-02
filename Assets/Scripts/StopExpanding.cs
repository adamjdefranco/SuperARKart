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
	public GameObject controls;

	// Use this for initialization
	void Start () {
		mReconstructionBehaviour = GetComponent<ReconstructionBehaviour>();
		mTracker = TrackerManager.Instance.GetTracker<SmartTerrainTracker>();
	}

	// Handler for when the user clicks "Go!" to start the game. Ends construction of the ground mesh
	public void stopReconstruction() {
		if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null)) {
			Debug.Log("Stopping the reconstruction");
			mReconstructionBehaviour.Reconstruction.Stop();
		}

		// Activate the top panel for in-game play
		topPanel.SetActive (!topPanel.activeInHierarchy);

		// Fades mesh to clear
		Animator meshToClearAnimator = this.GetComponentInChildren<Animator> ();
		meshToClearAnimator.Play ("FadeMeshToClear");

		// Player appears on the screen
		greenKart.SetActive (true);

		// Sets the timer for the round
		CountDownScript countDown = GameObject.FindObjectOfType<CountDownScript> ();
		float timeForRound = 30f;
		if (GameObject.FindObjectOfType<TimeBetweenScenes> () != null) {
			timeForRound = GameObject.FindObjectOfType<TimeBetweenScenes> ().TimeForRound;
		} 
		countDown.timeLeft = timeForRound;
		countDown.runScript = true;

		// Starts spawning coins
		CoinSpawningSystem css = GameObject.FindObjectOfType<CoinSpawningSystem> ();
		css.setCoinSpawnEnabled (true);

		// Set necessary panels active or inactive
		goButton.SetActive (false);
		resetButton.SetActive (false);
		controls.SetActive (true);

		// Play the in game music
		GameObject.FindObjectOfType<SoundController>().playInGameMusic();
	}

	// Handles the user reseting the construction of their table mesh
	public void resetReconstruction() {
		if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null)) {
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
}
