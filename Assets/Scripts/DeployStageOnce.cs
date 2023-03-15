using System;
using UnityEngine;
using Vuforia;

public class DeployStageOnce : MonoBehaviour
{
    public void OnInteractiveHitTest(HitTestResult result)
    {
        var listenerBehaviour = GetComponent<AnchorInputListenerBehaviour>();
        if (listenerBehaviour != null)
        {
            listenerBehaviour.enabled = true;
        }
    }
}
