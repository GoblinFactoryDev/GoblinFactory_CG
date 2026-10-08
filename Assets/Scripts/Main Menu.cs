using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* //============================================================================
 * Author: Cooper
 * Title: Main Menu
 * Date: 04/26/2026
 * Purpose: Handles and controls the main menu through the game manager
*/ //============================================================================

public class MainMenu : MonoBehaviour
{
    // The different menus for the player to interact with
    public enum menuType { TITLE = 0, OPTIONS = 1, CHARACTER = 2, SERVER = 3};
    public enum characterType { DWARF = 0, DRAGON = 1 };

    static private int numberOfCharacters = (System.Enum.GetValues(typeof(characterType)).Length);

    public characterType selectedCharacter;

    // A list of the active canvas groups in the scene to switch between
    [SerializeField] private List<CanvasGroup> menuList;

    private void Start()
    {
        //sets the Menu to the title when the scene loads
        ChangeMenu(0);
    }

    // A function to change the active scene through the game manager
    public void ChangeScene(string sceneToLoad)
    {
        GameManager.Instance.loadScene(sceneToLoad);
    }

    #region Menu Navigation
    // A function to change the active Menu
    public void ChangeMenu(int menuToLoad)
    {
        switch ((menuType)menuToLoad)
        {
            case menuType.TITLE:
                EnableMenu(menuType.TITLE);

                DisableMenu(menuType.OPTIONS);
                DisableMenu(menuType.CHARACTER);
                DisableMenu(menuType.SERVER);
                break;

            case menuType.OPTIONS:
                EnableMenu(menuType.OPTIONS);

                DisableMenu(menuType.TITLE);
                DisableMenu(menuType.CHARACTER);
                DisableMenu(menuType.SERVER);
                break;

            case menuType.CHARACTER:
                EnableMenu(menuType.CHARACTER);

                DisableMenu(menuType.OPTIONS);
                DisableMenu(menuType.TITLE);
                DisableMenu(menuType.SERVER);
                break;

            case menuType.SERVER:
                EnableMenu(menuType.SERVER);

                DisableMenu(menuType.OPTIONS);
                DisableMenu(menuType.CHARACTER);
                DisableMenu(menuType.TITLE);
                break;
        }
    }

    // A function that closes the Game window (Quits Game)
    public void ExitApplication()
    {
        Application.Quit();
    }

    // A function that enables a specific Menu through the Canvas Group
    private void EnableMenu(menuType menuIndex)
    {
        menuList[((int)menuIndex)].alpha = 1;
        menuList[((int)menuIndex)].interactable = true;
        menuList[((int)menuIndex)].blocksRaycasts = true;
    }

    // A function that disables a specific Menu through the Canvas Group
    private void DisableMenu(menuType menuIndex)
    {
        menuList[((int)menuIndex)].alpha = 0;
        menuList[((int)menuIndex)].interactable = false;
        menuList[((int)menuIndex)].blocksRaycasts = false;
    }
    #endregion

    public void ChangeCharacter(bool left)
    {
        if(left)
        {
            selectedCharacter -= 1;
            if (((int)selectedCharacter) < 0)
            {
                selectedCharacter = ((characterType)numberOfCharacters - 1);
            }
        }
        else
        {
            selectedCharacter += 1;
            if (((int)selectedCharacter) > (numberOfCharacters - 1))
            {
                selectedCharacter = 0;
            }
        }
    }
}
