
using UnityEngine.SceneManagement;

public interface ISceneManager
{
    void OpenScene(string sceneName,LoadSceneMode mode);
    void CloseScene(string sceneName);
}
