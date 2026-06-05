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
        deactivateButton();
    }

    //create a deactivate and finish function
    private void deactivateButton()
    {
        if (currentState == QTEButtonState.Success)
        {
            this.GetComponent<SpriteRenderer>().sprite = successSprite;
        }
        else if (currentState == QTEButtonState.Failure)
        {
            this.GetComponent<SpriteRenderer>().sprite = failureSprite;
        }
    }
}
