using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string pveSceneName = "PvE";
    [SerializeField] private string pvpSceneName = "PvP";
    public void ReloadScene() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void LoadPvE()
    {
        SceneManager.LoadSceneAsync(pveSceneName, LoadSceneMode.Single);
    }
    public void LoadPvP()
    {
        SceneManager.LoadSceneAsync(pvpSceneName, LoadSceneMode.Single);
    }
}
