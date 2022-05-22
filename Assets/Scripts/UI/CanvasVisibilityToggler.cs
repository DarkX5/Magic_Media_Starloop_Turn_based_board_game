using UnityEngine;

public class CanvasVisibilityToggler : MonoBehaviour
{
    private Canvas canvas = null;
    private void Start() {
        if (canvas == null) {
            canvas = GetComponent<Canvas>();
        }
    }
    public void ToggleCanvas() {
        if(canvas.enabled == true) {
            canvas.enabled = false;
        } else {
            canvas.enabled = true;
        }
    }
}
