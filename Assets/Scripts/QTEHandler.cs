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

    private int numberOfSequence;

    //sequence check (when the sequence is ready to be created this will be called to trigger it
    public bool createSequence = false;

    //number of elements(buttons) in the sequence
    private int numbOfSequence;

    public bool QTEMode = false;

    public int successCounter;

    public float FinishedTime;

    private int sequenceSize;



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
            SequenceGeneretor(5);
            createSequence = false;
            startTimer = true;
        }

        //check if the timer has started
        if (startTimer)
        {
            ChallengeTimer();
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
            successCounter++;
            if(orderOfSequence.Count != 0)
            {
                currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
            }
            else
            {
                FinishedTime = remainingTime;
                ChallengeCompleted();
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
            else
            {
                FinishedTime = remainingTime;
                ChallengeCompleted();
            }
        }
    }

    //to calculate the success / failure and half success
    private void ResultsCalculation()
    {
        if(successCounter == sequenceSize)
        {
            if (this.GetComponent<Player>().playerType == PlayerType.Player)
            {
                RoundManagerLocal.Instance.player1QTERating = CastRating.Full;
            }
            else
            {
                RoundManagerLocal.Instance.compQTERating = CastRating.Full;
            }
        }
        else if(successCounter < sequenceSize / 2)
        {
            if (this.GetComponent<Player>().playerType == PlayerType.Player)
            {
                RoundManagerLocal.Instance.player1QTERating = CastRating.Fail;
            }
            else
            {
                RoundManagerLocal.Instance.compQTERating = CastRating.Fail;
            }
        }
        else
        {
            if (RoundManagerLocal.Instance.GetNextSpell(this.GetComponent<Player>().playerType).CardInSlot.CardData.ThreeOutcomes)
            {
                if (this.GetComponent<Player>().playerType == PlayerType.Player)
                {
                    RoundManagerLocal.Instance.player1QTERating = CastRating.Half;
                }
                else
                {
                    RoundManagerLocal.Instance.compQTERating = CastRating.Half;
                }
            }
            else
            {
                if (this.GetComponent<Player>().playerType == PlayerType.Player)
                {
                    RoundManagerLocal.Instance.player1QTERating = CastRating.Fail;
                }
                else
                {
                    RoundManagerLocal.Instance.compQTERating = CastRating.Fail;
                }
            }
        }
    }

    private void SequenceGeneretor(int startingPos)
    {
        sequenceSize = RoundManagerLocal.Instance.GetNextSpell(this.GetComponent<Player>().playerType).CardInSlot.Difficulty;

        for (int i = 0; i < sequenceSize; i++)
        {
            orderOfSequence.Push(QTEButtons[startingPos]);
            QTEButtons[startingPos].SetActive(true);
            startingPos++;
        }
        currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
        QTEMode = true;
    }

    private bool startTimer = false;
    [SerializeField] private float remainingTime;
    private bool ChallengeHasStarted = false;

    private void ChallengeTimer()
    {
        //if the timer is running then tick down based on Time.deltaTime
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        //if the timer is done then set the remaining time to 0, the timer check to false and remove the QTEButtons that where present
        else if (remainingTime < 0)
        {
            ChallengeCompleted();
        }
    }

    private void ChallengeCompleted()
    {
        remainingTime = 0;
        startTimer = false;
        ChallengeHasStarted = false;
        foreach (GameObject button in QTEButtons)
        {
            if (button.activeSelf == true)
            {
                button.SetActive(false);
            }
        }

        ResultsCalculation();
        if (this.GetComponent<Player>().playerType == PlayerType.Player)
        {
            RoundManagerLocal.Instance.SetCurrentPlayerOneQTESpeed(FinishedTime);
            RoundManagerLocal.Instance.player1HasDoneQTE = true;
        }
        else
        {
            RoundManagerLocal.Instance.SetCurrentPlayerTwoQTESpeed(FinishedTime);
            RoundManagerLocal.Instance.compHasDoneQTE = true;
        }
    }
}
