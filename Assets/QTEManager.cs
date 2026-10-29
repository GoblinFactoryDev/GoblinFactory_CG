using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> qteButtons;
    private int difficulty;

    public bool qteStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void startQTE()
    {
        Debug.Log("QTE Started!");

        difficulty = RoundManagerLocal.Instance.GetNextSpell(this.GetComponent<Player>().playerType).CardInSlot.Difficulty;

        for (int i = 0; i < difficulty; i++)
        {
            qteButtons[i].SetActive(true);
        }
    }


    public bool checkDone()
    {


        return false;
    }
}
