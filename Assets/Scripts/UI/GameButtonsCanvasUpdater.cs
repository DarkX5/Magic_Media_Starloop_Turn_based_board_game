using TurnBased.Core;
using UnityEngine;

public class GameButtonsCanvasUpdater : MonoBehaviour
{
    [Header("Auto-Set - visible 4 debugging")]
    [SerializeField] private Canvas buttonsCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonsCanvas == null)
        {
            buttonsCanvas = GetComponent<Canvas>();
        }
        if (buttonsCanvas == null)
        {
            Debug.LogError("Buttons Canvas not found - may need to be set manually");
        }
        // start with the buttons enabled
        buttonsCanvas.enabled = true;

        // subscribe to new turn event
        GameHandler.onNextTurn += EnableCanvas;
        // subscribe to move start events
        GameHandler.onMoveStateChanged += DisableCanvas;
    }
    private void OnDestroy()
    {
        // unsubscribe to new turn event
        GameHandler.onNextTurn -= EnableCanvas;
        // unsubscribe to move start events
        GameHandler.onMoveStateChanged -= DisableCanvas;
    }

    private void EnableCanvas(int turnNo, bool isHuman)
    {
        if (isHuman)
            buttonsCanvas.enabled = true;
        else
            buttonsCanvas.enabled = false;
    }
    private void DisableCanvas(bool isMovingState)
    {
        if (isMovingState == true)
            buttonsCanvas.enabled = false;
    }
}
