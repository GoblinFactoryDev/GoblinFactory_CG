using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using static QTEButton;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<QTEButton> qteButtons;
    private List<QTEButton> activeButtonList = new List<QTEButton>();

    private int difficulty = -1;
    private int succTotal;
    private int inputTotal;

    public bool qteStart = true;
    public bool qteStop = false;

    private int qteTimerLength = 10;
    private int qteCurrentTime;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Update()
    {
        if (inputTotal == difficulty)
        {
            FinishQTE();
        }
    }

    public void startQTE()
    {
        Debug.Log("QTE Started!");

        difficulty = RoundManagerLocal.Instance.GetNextSpell(this.GetComponent<Player>().playerType).CardInSlot.Difficulty;

        for (int i = 0; i < difficulty; i++)
        {
            qteButtons[i].gameObject.SetActive(true);
            qteButtons[i].SetInputType((QTEType)Random.Range(0, 7));

            activeButtonList.Add(qteButtons[i]);
        }

        StartCoroutine(QTETimer());
        qteCurrentTime = qteTimerLength;
        this.GetComponent<PlayerInput>().SwitchCurrentActionMap("QTE");
    }

    public QTEType GetCurrentInput()
    {
        return activeButtonList[0].getCurrentType;
    }

    public void InputDetected(bool didGood)
    {
        //If they did good, succtotal goes up;
        if(didGood)
        {
            succTotal++;
            inputTotal++;
            activeButtonList[0].SetButtonState(QTEButtonState.Success);
        }
        else
        {
            inputTotal++;
            activeButtonList[0].SetButtonState(QTEButtonState.Failure);
        }

        activeButtonList.RemoveAt(0);
    }

    void FinishQTE()
    {
        Debug.Log("QTE OVER!");
        StopAllCoroutines();
        for (int i = 0; i < difficulty; i++)
        {
            qteButtons[i].SetButtonState(QTEButtonState.Default);
            qteButtons[i].gameObject.SetActive(false);
        }
        GPACalc();
        inputTotal = 0;
        difficulty = -1;
        succTotal = 0;
        //Confirm to RoundManager your QTE state (so just confirm you finished the qte here)
        //this will run at the end of the timer 
        if(this.GetComponent<Player>().playerType == PlayerType.Player)
        {
            RoundManagerLocal.Instance.SetCurrentPlayerOneQTESpeed(qteCurrentTime);
            RoundManagerLocal.Instance.player1HasDoneQTE = true;
        }
        else //pretty sure this never runs until we have PVP since the computer wont ever be "this"
        {
            RoundManagerLocal.Instance.SetCurrentPlayerTwoQTESpeed(qteCurrentTime);
            RoundManagerLocal.Instance.compHasDoneQTE = true;
        }
        this.GetComponent<PlayerInput>().SwitchCurrentActionMap("CardGame");
    }

    IEnumerator QTETimer()
    {
        int startTimeValue = qteTimerLength;

        while (startTimeValue >= 0)
        {
            yield return new WaitForSeconds(1);
            startTimeValue--;
            Debug.Log($"Countdown: " + startTimeValue + "s");
            qteCurrentTime = startTimeValue;
        }

        FinishQTE();
        qteStop = true;
    }

    //Calculate Results to send to RoundManager (also calc is short for calculator, also GPA I guess lol)
    private void GPACalc()
    {
        if(succTotal == difficulty)
        {
            //once PVP is starting/worked on change this to a else if statement changing between P1 and P2, for now it will only represent P1 which is player against machine AI
            RoundManagerLocal.Instance.player1QTERating = CastRating.Full;
        }
        else if(succTotal <  difficulty / 2)
        {
            RoundManagerLocal.Instance.player1QTERating = CastRating.Half;
        }
        else
        {
            RoundManagerLocal.Instance.player1QTERating = CastRating.Fail;
        }
    }



    
}
