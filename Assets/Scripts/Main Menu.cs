using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ChangeScene(string sceneToLoad)
    {
        GameManager.Instance.loadScene(sceneToLoad);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
