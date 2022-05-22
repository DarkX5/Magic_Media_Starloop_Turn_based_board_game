using TurnBased.Core;
using UnityEngine;

public class BigDiceButtonVisibilityUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // subscribe to new turn to update the big dice counter
        GameHandler.onBigDiceButtonVisibilityCheck += UpdateVisibility;
    }
    private void OnDestroy()
    {

        // unsubscribe to new turn to update the big dice counter
        GameHandler.onBigDiceButtonVisibilityCheck -= UpdateVisibility;
    }

    private void UpdateVisibility(bool cooldownOver)
    {
        if (gameObject.activeSelf != cooldownOver) {
            gameObject.SetActive(cooldownOver);
        }
    }
}
