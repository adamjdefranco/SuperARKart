using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class CanvasUIHelper : MonoBehaviour {

	public GameObject dollarUI;
	public GameObject goButton;
	public GameObject resetButton;

	//ReconstructionBehaviour mReconstructionBehaviour;
	SmartTerrainTracker mTracker;
	ReconstructionBehaviour mReconstructionBehaviour;

	// Use this for initialization
	void Start () {
		mTracker = TrackerManager.Instance.GetTracker<SmartTerrainTracker>();
		mReconstructionBehaviour = (ReconstructionBehaviour)FindObjectOfType(typeof(ReconstructionBehaviour));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleDollarFrame(bool setActive) {
		dollarUI.SetActive(setActive);
	}

	public void toggleGoAndResetButtons(bool setActive) {
		goButton.SetActive(setActive);
		resetButton.SetActive (setActive);
	}

	public void onResetClicked() {

		Debug.Log ("On Reset Click");

		Debug.Log (mReconstructionBehaviour);

		if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null)) {
			bool trackerWasActive = mTracker.IsActive;
			// first stop the tracker
			if (trackerWasActive)
				mTracker.Stop();
			// now you can reset...
			mReconstructionBehaviour.Reconstruction.Reset();
			// ... and restart the tracker
			if (trackerWasActive) {
				mTracker.Start();
				mReconstructionBehaviour.Reconstruction.Start();
			}
		}
	}
}
