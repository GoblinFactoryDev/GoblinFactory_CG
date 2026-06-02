using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class QTEHandler : MonoBehaviour
{
    [SerializeField] QTEButton btnRef;

    //in order to keep track of items I can make a store current button variable and a stack that keeps all buttons store, then pops the current button out of the list and into the current button variable and that is evaluated for input, after its input its confirm it can then repeat until the stack is empty

    //list of sprites default
    public List<Sprite> defaultSprite = new List<Sprite>();
    //list of sprites success
    public List<Sprite> successSprite = new List<Sprite>();
    //list of sprites mistake
    public List<Sprite> failureSprite = new List<Sprite>();
    //list of all buttons
    public List<GameObject> QTEButtons = new List<GameObject>();

    public void BtnCheckTest(PlayerInputHandler inputReference)
    {
        if(inputReference.challengeInputA.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.A || inputReference.challengeInputB.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.B ||
           inputReference.challengeInputY.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.Y || inputReference.challengeInputX.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.X ||
           inputReference.challengeInputUp.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.UpArrow || inputReference.challengeInputDown.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.DownArrow ||
           inputReference.challengeInputRight.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.RightArrow || inputReference.challengeInputLeft.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.LeftArrow)
        {
            Debug.Log("shit worked homie");
            btnRef.currentState = QTEButtonState.Success;
        }
        else if (inputReference.challengeInputA.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.A || inputReference.challengeInputB.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.B ||
           inputReference.challengeInputY.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.Y || inputReference.challengeInputX.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.X ||
           inputReference.challengeInputUp.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.UpArrow || inputReference.challengeInputDown.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.DownArrow ||
           inputReference.challengeInputRight.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.RightArrow || inputReference.challengeInputLeft.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.LeftArrow)
        {
            Debug.Log("shit worked homie");
            btnRef.currentState = QTEButtonState.Failure;
        }
    }
}
