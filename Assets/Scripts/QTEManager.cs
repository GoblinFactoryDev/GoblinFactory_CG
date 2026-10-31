using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static QTEButton;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<QTEButton> qteButtons;
    private List<QTEButton> activeButtonList = new List<QTEButton>();

    private int difficulty;
    private int succTotal;

    public bool qteStart = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Update()
    {
        //if is checking if the player is in the right section and the bool qteStart is making sure we only generate 1 sequence
        if(this.GetComponent<PlayerInput>().currentActionMap.name == "QTE" && qteStart)
        {
            //run sequence generator
            startQTE();
            //turn off sequence generator after you make it once
            qteStart = false;
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
            activeButtonList[0].SetButtonState(QTEButtonState.Success);
        }
        else
        {
            activeButtonList[0].SetButtonState(QTEButtonState.Failure);
        }

        activeButtonList.RemoveAt(0);
    }
}
