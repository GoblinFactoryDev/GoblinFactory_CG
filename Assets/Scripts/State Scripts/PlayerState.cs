using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: PlayerState
//  Date Created: 08/03/2025
//  Purpose: This class is the controller for the Player's state machine.
//  Instance: no
//-----------------------------------------------------------------

public class PlayerState : AdvancedFSM
{
    [SerializeField]
    public TMP_Text test_text;

    [SerializeField]
    public Player player;

    [SerializeField]
    private ControllerInputs playerController;

    private string GetStateString()
    {
        string state = "NONE";
        if (CurrentState.ID == FSMStateID.Dead)
        {
            state = "Dead";
        }
        else if (CurrentState.ID == FSMStateID.RoundEffects)
        {
            state = "RoundEffects";
        }
        else if (CurrentState.ID == FSMStateID.DealStats)
        {
            state = "DealStats";
        }
        else if (CurrentState.ID == FSMStateID.ChooseSpells)
        {
            state = "ChooseSpells";
        }
        else if (CurrentState.ID == FSMStateID.Configure)
        {
            state = "Configure";
        }
        else if (CurrentState.ID == FSMStateID.CastSpell)
        {
            state = "CastSpell";
        }
        else if (CurrentState.ID == FSMStateID.Qte)
        {
            state = "Qte";
        }
        else if (CurrentState.ID == FSMStateID.Defualt)
        {
            state = "Defualt";
        }
        return state;
    }

    protected override void Initialize()
    {
        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        if (CurrentState == null)
        {
            return;
        }

        CurrentState.Reason();
        CurrentState.Act();

        if (player.playerType == PlayerType.Player)
        test_text.text = "State: " + GetStateString();
    }

    private void ConstructFSM()
    {
        DefualtState defualt = new DefualtState(this);
        defualt.AddTransition(Transition.StartGame, FSMStateID.DealStats);

        DealStatsState dealStats = new DealStatsState(this);
        dealStats.AddTransition(Transition.StatsDealt, FSMStateID.RoundEffects);

        RoundEffectsState roundEffects = new RoundEffectsState(this);
        roundEffects.AddTransition(Transition.RoundEffectsDone, FSMStateID.ChooseSpells);
        roundEffects.AddTransition(Transition.Died, FSMStateID.Dead);

        ChooseSpellState chooseSpells = new ChooseSpellState(this);
        chooseSpells.AddTransition(Transition.SpellsChosen, FSMStateID.Configure);
        chooseSpells.AddTransition(Transition.Died, FSMStateID.Dead);

        ConfigureState configure = new ConfigureState(this);
        configure.AddTransition(Transition.CastingSpell, FSMStateID.CastSpell);
        configure.AddTransition(Transition.dealingStats, FSMStateID.DealStats);
        configure.AddTransition(Transition.Died, FSMStateID.Dead);

        CastSpellState castSpell = new CastSpellState(this);
        castSpell.AddTransition(Transition.QteStart, FSMStateID.Qte);

        QteState qte = new QteState(this);
        qte.AddTransition(Transition.QteEnd, FSMStateID.Configure);

        DeadState dead = new DeadState(this);

        //Add States to the state list
        AddFSMState(defualt);
        AddFSMState(dealStats);
        AddFSMState(roundEffects);
        AddFSMState(chooseSpells);
        AddFSMState(configure);
        AddFSMState(castSpell);
        AddFSMState(qte);
        AddFSMState(dead);

    }
}