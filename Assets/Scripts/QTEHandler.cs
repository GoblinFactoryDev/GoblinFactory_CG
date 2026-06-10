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

    //this checks for the controll side of the challenge mode making sure it triggers controll inputs until all data is set to start
    public bool QTEMode = false;

    //this value keeps track of how many successfull inputs the player has in the sequence
    public int successCounter;

    //this variable stores the time the player took to finish the challenge
    public float FinishedTime;

    //this value holds the current sequence size
    private int sequenceSize;

    //will start the QTE trigger timer
    private bool startTimer = false;

    //this is the actual timer variable that ticks down
    [SerializeField] private float remainingTime;



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

    public void ChallengeInputTracker(PlayerInputHandler inputReference)
    {
        //check for controller inputs
        if(inputReference.challengeInputA.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.A || inputReference.challengeInputB.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.B ||
           inputReference.challengeInputY.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.Y || inputReference.challengeInputX.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.X ||
           inputReference.challengeInputUp.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.UpArrow || inputReference.challengeInputDown.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.DownArrow ||
           inputReference.challengeInputRight.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.RightArrow || inputReference.challengeInputLeft.WasCompletedThisFrame() && currentBtn.buttonValue == KeyCode.LeftArrow)
        {
            Debug.Log("shit worked homie");
            //set the current button state to success
            currentBtn.currentState = QTEButtonState.Success;
            //up the successful inputs counter
            successCounter++;
            if(orderOfSequence.Count != 0)
            {
                //remove the current button and set the next one 
                currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
            }
            else
            {
                //save the time the challenge took
                FinishedTime = remainingTime;
                //reset values
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
        //check if the number of successful inputs is the same as the sequence size
        if(successCounter == sequenceSize)
        {
            //if so then set the results to a full success
            if (this.GetComponent<Player>().playerType == PlayerType.Player)
            {
                RoundManagerLocal.Instance.player1QTERating = CastRating.Full;
            }
            else
            {
                RoundManagerLocal.Instance.compQTERating = CastRating.Full;
            }
        }
        //if the successful inputs is less than half the sequence size and the card only has 1
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
        //if the card has 3 options and they did not get a success or a failure then mark them as half success
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
        //take in the qte value of the current card
        sequenceSize = RoundManagerLocal.Instance.GetNextSpell(this.GetComponent<Player>().playerType).CardInSlot.Difficulty;

        //loop the number of times based on the sequence size
        for (int i = 0; i < sequenceSize; i++)
        {
            //store buttons on a stack that will be the selected buttons for the challenge
            orderOfSequence.Push(QTEButtons[startingPos]);
            //set that button to active as to start showing the sequence
            QTEButtons[startingPos].SetActive(true);
            //up the list of buttons to store the next one
            startingPos++;
        }
        //set the first button of the sequence with the stack
        currentBtn = orderOfSequence.Pop().GetComponent<QTEButton>();
        //set the mode on to start tracking input
        QTEMode = true;
    }

    

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

    /// <summary>
    /// This function resets any needed variables and finishes any info of the challenge QTE portion
    /// </summary>
    private void ChallengeCompleted()
    {
        //set the timer to 0 again
        remainingTime = 0;
        //reset the timer start check
        startTimer = false;
        //track each button and turn it off on the scene
        foreach (GameObject button in QTEButtons)
        {
            if (button.activeSelf == true)
            {
                button.SetActive(false);
            }
        }

        //set and calculate the results of the challenge
        ResultsCalculation();

        //set the speed values of the challenge for each player and set the round manager round properly
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
