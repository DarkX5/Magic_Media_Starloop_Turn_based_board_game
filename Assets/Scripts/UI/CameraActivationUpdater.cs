using System.Collections;
using System.Collections.Generic;
using TurnBased.Core;
using UnityEngine;

public class CameraActivationUpdater : MonoBehaviour
{
    [Header("Auto-Set - visible 4 debugging")]
    [SerializeField] private Camera followCam = null;
    [SerializeField] private AudioListener audioListener = null;
    [SerializeField] private int playerID = -1;

    // Start is called before the first frame update
    void Start()
    {
        followCam = GetComponent<Camera>();
        audioListener = GetComponent<AudioListener>();
        playerID = transform.parent.GetComponent<CharController>().PlayerID;

        GameHandler.onNextTurnCameraUpdate += ToggleCanvas;
    }
    private void OnDestroy() {
        GameHandler.onNextTurnCameraUpdate -= ToggleCanvas;
    }

    private void ToggleCanvas(int currentPlayerID) {
        if(playerID == currentPlayerID) {
            followCam.enabled = true;
            audioListener.enabled = true;
        } else {
            followCam.enabled = false;
            audioListener.enabled = false;
        }
    }
}
