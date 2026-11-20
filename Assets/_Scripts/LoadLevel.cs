using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadLevelByID(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void Quit()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if(UNITY_STANDALONE)
        Application.Quit();
#endif
    }
}
