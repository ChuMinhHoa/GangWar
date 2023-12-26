using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private StateMachine<Vehicle> m_StateMachine;
    public StateMachine<Vehicle> StateMachine { get { return m_StateMachine; } }

    public virtual void Awake()
    {
        InitStateMachine();
    }

    protected virtual void InitStateMachine()
    {
        m_StateMachine = new StateMachine<Vehicle>(this);
        m_StateMachine.SetGlobalState(VGlobleState.Instance);
        m_StateMachine.ChangeState(VIdleState.Instance);
    }

    public virtual void IdleEnter() { }
    public virtual void IdleExecute() { }
    public virtual void IdleExit() { }

    public virtual void MoveEnter() { }
    public virtual void MoveExecute() { }
    public virtual void MoveExit() { }
}
