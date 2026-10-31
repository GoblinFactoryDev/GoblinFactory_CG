using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static QTEButton;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<QTEButton> qteButtons;

    private int difficulty;

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
            qteButtons[i].SetInputType((QTEButton.QTETYPE)Random.Range(0, 7));
        }
    }
}
