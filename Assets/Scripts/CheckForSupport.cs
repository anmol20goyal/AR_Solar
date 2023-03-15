using System;
using UnityEngine;
// using UnityEngine.UI;
using Vuforia;

public class CheckForSupport : MonoBehaviour
{
    #region GameObjects

    [SerializeField] private GameObject warningMsgBox;
    // [SerializeField] private Button infoBtn;

    #endregion
    
    #region VuforiaVariables

    private PositionalDeviceTracker positionalDeviceTracker;
    private StateManager stateManager;
    private SmartTerrain smartTerrain;

    #endregion

    private void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
        Debug.Log("OnVuforiaStarted() called.");

        stateManager = TrackerManager.Instance.GetStateManager();

        // Check trackers to see if started and start if necessary
        this.positionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
        this.smartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();

        if (this.positionalDeviceTracker != null && this.smartTerrain != null)
        {
            if (!this.positionalDeviceTracker.IsActive)
            {
                Debug.LogError("The Ground Plane feature requires the Device Tracker to be started. " +
                               "Please enable it in the Vuforia Configuration or start it at runtime through the scripting API.");
                return;
            }

            if (this.positionalDeviceTracker.IsActive && !this.smartTerrain.IsActive)
                this.smartTerrain.Start();
        }
        else
        {
            if (this.positionalDeviceTracker == null)
                Debug.Log("PositionalDeviceTracker returned null. GroundPlane not supported on this device.");
            if (this.smartTerrain == null)
                Debug.Log("SmartTerrain returned null. GroundPlane not supported on this device.");

            warningMsgBox.SetActive(true);
            // infoBtn.interactable = false;
            Invoke(nameof(ExitApp), 10f);
        }
    }
    
    public void ExitApp()
    {
        Application.Quit();
    }
}
