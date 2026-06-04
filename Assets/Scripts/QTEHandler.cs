using System;
using System.Collections.Generic;
using UnityEngine;

public class QTEHandler : MonoBehaviour
{
    [SerializeField] QTEButton currentBtn;

    //in order to keep track of items I can make a store current button variable and a stack that keeps all buttons store, then pops the current button out of the list and into the current button variable and that is evaluated for input, after its input its confirm it can then repeat until the stack is empty
    Stack<GameObject> orderOfSequence = new Stack<GameObject>();
    //.push() will add elements, the more you add the more it ill be deeper in the stack so the first objecti you add is the one at the top
    //.pop() will remove and return the top element

    //sequence check (when the sequence is ready to be created this will be called to trigger it
    public bool createSequence = false;

    //number of elements(buttons) in the sequence
    private int numbOfSequence;

    public bool QTEMode = false;


    //list of sprites default
    public List<Sprite> defaultSprite = new List<Sprite>();
    //list of sprites success
    public List<Sprite> successSprite = new List<Sprite>();
    //list of sprites mistake
    public List<Sprite> failureSprite = new List<Sprite>();
    //list of all buttons
    public List<GameObject> QTEButtons = new List<GameObject>();

    private void Update()
    {
        if(createSequence)
        {
            SequenceGeneretor(12, 0);
            createSequence = false;
        }
    }

    public void BtnCheckTest(PlayerInputHandler inputReference)
    {
        if(inputReference.challengeInputA.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.A || inputReference.challengeInputB.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.B ||
           inputReference.challengeInputY.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.Y || inputReference.challengeInputX.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.X ||
           inputReference.challengeInputUp.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.UpArrow || inputReference.challengeInputDown.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.DownArrow ||
           inputReference.challengeInputRight.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.RightArrow || inputReference.challengeInputLeft.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.LeftArrow)
        {
            Debug.Log("shit worked homie");
            currentBtn.currentState = QTEButtonState.Success;
            if(orderOfSequence.Count != 0)
            {
                currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
            }
        }
        else if (inputReference.challengeInputA.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.A || inputReference.challengeInputB.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.B ||
           inputReference.challengeInputY.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.Y || inputReference.challengeInputX.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.X ||
           inputReference.challengeInputUp.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.UpArrow || inputReference.challengeInputDown.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.DownArrow ||
           inputReference.challengeInputRight.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.RightArrow || inputReference.challengeInputLeft.WasCompletedThisFrame() && currentBtn.buttonValue != KeyCode.LeftArrow)
        {
            Debug.Log("shit worked homie");
            currentBtn.currentState = QTEButtonState.Failure;
            if (orderOfSequence.Count != 0)
            {
                currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
            }
        }
    }

    private void SequenceGeneretor(int sequenceSize, int startingPos)
    {
        for (int i = 0; i < sequenceSize; i++)
        {
            orderOfSequence.Push(QTEButtons[startingPos]);
            startingPos++;
        }
        currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
        QTEMode = true;
    }
}
