using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
public class StartNewScene : MonoBehaviour {


    private void Start()
    {
        Advertisement.Show("video");
    }
    /// <summary>
    /// Load new Scene
    /// </summary>
    /// <param name="sceneNumber">
    /// number of scene in build Settings
    /// </param>
    public void StartScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
