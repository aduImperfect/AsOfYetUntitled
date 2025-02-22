using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachineManager
{
    private Queue<StateData> stateChangeQueue;
    private StateData currentStateData;

    public Action<StateData> OnCurrentStateUpdated;

    public CharacterStateMachineManager()
    {
        stateChangeQueue = new Queue<StateData>();
    }

    //Add function that adds into queue
    public void AddToStateChangeQueue(StateData stateData)
    {
        stateChangeQueue.Enqueue(stateData);
    }

    public void ResetStateQueue()
    {
        stateChangeQueue.Clear();
    }

    public Queue<StateData> GetCurrentStateChangeQueue()
    {
        return stateChangeQueue;
    }
    
    public StateData GetCurrentStateData()
    {
        return currentStateData;
    }

    public void RequestStateChange()
    {
        //Primary State Checks
        StateData toStateChangeData = stateChangeQueue.Dequeue();

        if (toStateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_INVALID && toStateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_COUNT || toStateChangeData.GetSecondaryState() == SecondaryState.SECONDARYSTATE_INVALID || toStateChangeData.GetSecondaryState() == SecondaryState.SECONDARYSTATE_COUNT)
        {
            return;
        }

        bool canProceedToNextState = false;
        CanProceedToState(ref canProceedToNextState, toStateChangeData);

        if (canProceedToNextState)
        {
            SwitchStateChangeData(toStateChangeData);
        }
    }

    private void CanProceedToState(ref bool canProceed, StateData stateChangeData)
    {
        canProceed = false;

        if (stateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_IDLE)
        {
            canProceed = false;
            switch (stateChangeData.GetSecondaryState())
            {
                case SecondaryState.SECONDARYSTATE_NONE:
                    canProceed = true;
                    break;

                case SecondaryState.SECONDARYSTATE_PAUSEMENU:
                    if (stateChangeData.GetPauseMenuSubstate() != PauseMenuSubstate.PAUSEMENU_INVALID)
                    {
                        canProceed = true;
                    }
                    break;

                default:
                    break;
            }
        }
        else if (stateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_LOCKED)
        {
            canProceed = false;

            switch (stateChangeData.GetSecondaryState())
            {
                case SecondaryState.SECONDARYSTATE_INTERACTING:
                    if(stateChangeData.GetInteractingSubstate() == InteractingSubstate.INTERACTINGSUBSTATE_NONE)
                    {
                        canProceed = true;
                    }
                    break;

                case SecondaryState.SECONDARYSTATE_CINEMATIC:
                    if (stateChangeData.GetCinematicSubstate() == CinematicSubstate.CINEMATICSUBSTATE_START)
                    {
                        canProceed = true;
                    }
                    break;

                case SecondaryState.SECONDARYSTATE_PAUSEMENU:
                    if (stateChangeData.GetPauseMenuSubstate() != PauseMenuSubstate.PAUSEMENU_INVALID)
                    {
                        canProceed = true;
                    }
                    break;

                default:
                    break;
            }
        }
        else if (stateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_WALKING)
        {
            canProceed = false;

            switch (stateChangeData.GetSecondaryState())
            {
                case SecondaryState.SECONDARYSTATE_NONE:
                    canProceed = true;
                    break;

                case SecondaryState.SECONDARYSTATE_DASHING:
                    if (stateChangeData.GetDashingSubstate() == DashingSubstate.DASHINGSUBSTATE_START)
                    {
                        canProceed = true;
                    }
                    break;

                case SecondaryState.SECONDARYSTATE_THROWING:
                    if (stateChangeData.GetThrowingSubstate() != ThrowingSubstate.THROWINGSUBSTATE_NONE)
                    {
                        canProceed = true;
                    }
                    break;

                case SecondaryState.SECONDARYSTATE_PAUSEMENU:
                    if (stateChangeData.GetPauseMenuSubstate() != PauseMenuSubstate.PAUSEMENU_INVALID)
                    {
                        canProceed = true;
                    }
                    break;

                default:
                    break;
            }
        }
        
        else if (stateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_LOITERING)
        {
            canProceed = false;

            switch (stateChangeData.GetSecondaryState())
            {
                case SecondaryState.SECONDARYSTATE_NONE:
                    canProceed = true;
                    break;

                case SecondaryState.SECONDARYSTATE_DASHING:
                    if (stateChangeData.GetDashingSubstate() != DashingSubstate.DASHINGSUBSTATE_NONE)
                    {
                        canProceed = true;
                    }
                    break;

                case SecondaryState.SECONDARYSTATE_THROWING:
                    break;

                case SecondaryState.SECONDARYSTATE_PAUSEMENU:
                    if (stateChangeData.GetPauseMenuSubstate() != PauseMenuSubstate.PAUSEMENU_NONE)
                    {
                        canProceed = true;
                    }
                    break;

                default:
                    break;
            }
        }
        
        else if (stateChangeData.GetPrimaryState() == PrimaryState.PRIMARYSTATE_DEAD)
        {
            canProceed = false;

            switch (stateChangeData.GetSecondaryState())
            {
                case SecondaryState.SECONDARYSTATE_NONE:
                    canProceed = true;
                    break;

                case SecondaryState.SECONDARYSTATE_PAUSEMENU:
                    if (stateChangeData.GetPauseMenuSubstate() != PauseMenuSubstate.PAUSEMENU_INVALID)
                    {
                        canProceed = true;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    private void SwitchStateChangeData(StateData stateData)
    {
        bool anyStateUpdated = (currentStateData != stateData);
        
        //Switch currentState
        
        if(anyStateUpdated)
        {
            currentStateData = stateData;
            OnCurrentStateUpdated?.Invoke(stateData);
        }
    }
}