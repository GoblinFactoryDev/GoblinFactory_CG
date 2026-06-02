using UnityEngine;

public class QTEHandler : MonoBehaviour
{
    [SerializeField] QTEButton btnRef;

    //in order to keep track of items I can make a store current button variable and a stack that keeps all buttons store, then pops the current button out of the list and into the current button variable and that is evaluated for input, after its input its confirm it can then repeat until the stack is empty


    public void BtnCheckTest(PlayerInputHandler inputReference)
    {
        if(inputReference.challengeInputA.WasCompletedThisFrame() && btnRef.buttonValue == KeyCode.A)
        {
            Debug.Log("shit worked homie");
            btnRef.currentState = QTEButtonState.Success;
        }
        else if (inputReference.challengeInputA.WasCompletedThisFrame() && btnRef.buttonValue != KeyCode.A)
        {
            Debug.Log("shit worked homie");
            btnRef.currentState = QTEButtonState.Failure;
        }
    }
}
