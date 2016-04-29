using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class CanvasUIHelper : MonoBehaviour {

	public GameObject dollarUI;
	public GameObject goButton;
	public GameObject resetButton;

	//ReconstructionBehaviour mReconstructionBehaviour;
	SmartTerrainTracker      mTracker = TrackerManager.Instance.GetTracker<SmartTerrainTracker>();
	ReconstructionBehaviour  mReconstructionBehaviour = (ReconstructionBehaviour)FindObjectOfType(typeof(ReconstructionBehaviour));

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hideShowDollarFrame() {
		Debug.Log ("ShowHideDollar");
		dollarUI.SetActive(!dollarUI.activeInHierarchy);
	}

	public void hideShowGoResetButtons() {
		goButton.SetActive(!goButton.activeInHierarchy);
		resetButton.SetActive (!resetButton.activeInHierarchy);
	}

	public void onResetClicked() {

		Debug.Log ("On Reset Click");

		Debug.Log (mReconstructionBehaviour);
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

		//mReconstructionBehaviour.Reconstruction.Reset();
	}
}
