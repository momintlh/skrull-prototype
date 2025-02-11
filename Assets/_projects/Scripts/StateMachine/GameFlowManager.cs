﻿using System;
using UnityEngine;
using UnityHFSM;

public class GameFlowManager : MonoBehaviour
{
    public StateMachine GameFlowMachine { get; private set; }

    public States CurrentState { get; private set; }

//
    private void Start()
    {
        // Create the state machine
        GameFlowMachine = new StateMachine();

        // Set the current state
        CurrentState = States.CardPlacement;

        AddStatesToMachine();
        AddTransitions();

        // Set the start state
        GameFlowMachine.SetStartState(States.CardPlacement.ToString());
        // Initialize the state machine
        GameFlowMachine.Init();
    }


    private void AddStatesToMachine()
    {
        GameFlowMachine.AddState(States.CardPlacement.ToString(), new CardPlacementState(GameFlowMachine));
        GameFlowMachine.AddState(States.Bid.ToString(), new BiddingState(GameFlowMachine));
        GameFlowMachine.AddState(States.Reveal.ToString(), new RevealState(GameFlowMachine));
        GameFlowMachine.AddState(States.WinByPoints.ToString(), new WinByPointState(GameFlowMachine));
        GameFlowMachine.AddState(States.Discard.ToString() , new DiscardState(GameFlowMachine));
    }

    private void AddTransitions()
    {
        GameFlowMachine.AddTriggerTransition(TriggerEvents.BiddingStarted.ToString(), States.CardPlacement.ToString(),
            States.Bid.ToString());
    }

    private void Update()
    {
        GameFlowMachine.OnLogic();

        Debug.Log(GameFlowMachine.GetActiveHierarchyPath());
    }
}