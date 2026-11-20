using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoadManager : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        // Implementation for loading a Scene by index
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(string name)
    {
        // Implementation for loading a Scene by name
        SceneManager.LoadScene(name);
    }

    public void ReloadCurrentLevel()
    {
        // Implementation for reloading the current Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        // Implementation for loading the next Scene in the build settings
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels to load.");
        }
    }

    public void LoadPreviousLevel()
    {
        // Implementation for loading the previous Scene in the build settings
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("No previous level to load.");
        }
    }

    public void LoadLevelAsync(int index)
    {
        // Implementation for loading a Scene asynchronously by index
        StartCoroutine(LoadYourAsyncScene(index));
    }
    private IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        // Optional: prevent scene from activating immediately
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            yield return null;
        }
    }

    public void QuitGame()
    {
        // Implementation for quitting the game
        // In the Unity Editor, this will stop play mode
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // In a standalone build, this will close the application
#if (UNITY_STANDALONE)
        Application.Quit();
#endif
    }
}
