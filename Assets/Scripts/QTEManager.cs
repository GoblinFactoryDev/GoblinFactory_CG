using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
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
        //if is checking if the player is in the right section and the bool qteStart is making sure we only generate 1 sequence
        if (this.GetComponent<PlayerInput>().currentActionMap.name == "QTE" && qteStart)
        {
            //run sequence generator
            startQTE();
            //turn off sequence generator after you make it once
            qteStart = false;
        }

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
        inputTotal = 0;
        difficulty = -1;
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

    
}
