using UnityEngine;
using UnityEngine.InputSystem;

public class QTEButton : MonoBehaviour
{
    //after its randomized I will assign its sprites to the script
    public Sprite baseSprite;
    public Sprite successSprite;
    public Sprite failureSprite;

    [SerializeField] private PlayerInput inputRef;

    //store button value
    public KeyCode buttonValue;

    //button state variables
    public QTEButtonState currentState = QTEButtonState.Default;

    private void Update()
    {
        deactivateScript();
    }

    //create a deactivate and finish function
    private void deactivateScript()
    {
        if (currentState == QTEButtonState.Success)
        {
            this.GetComponent<SpriteRenderer>().sprite = successSprite;
            this.enabled = false;
        }
        else if (currentState == QTEButtonState.Failure)
        {
            this.GetComponent<SpriteRenderer>().sprite = failureSprite;
            this.enabled = false;
        }
    }
}
