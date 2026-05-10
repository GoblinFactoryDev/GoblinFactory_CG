using UnityEngine;

/* //============================================================================
 * Author: Cooper
 * Title: Main Menu
 * Date: 04/26/2026
 * Purpose: Handles and controls the main menu through the game manager
*/ //============================================================================

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
