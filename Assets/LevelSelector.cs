using UnityEngine;
using UnityEngine.SceneManagement; 


public class LevelSelector : MonoBehaviour
{
    public void Select (string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OnQuitApplication()
    {
        #if (UNITY_EDITOR)
            Debug.Log("Quitting...");
        #endif
        Application.Quit();
    }
    
}
