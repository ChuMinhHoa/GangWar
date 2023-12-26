using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
public interface CharacterInterface {
    public void SetupMove(Transform trsTarget);
}
public class Character : MonoBehaviour, CharacterInterface
{
    public StateMachine<Character> StateMachine { get { return m_StateMachine; } }
    protected StateMachine<Character> m_StateMachine;
    public NavMeshAgent agent;
    [Header("======TRANSFORM======")]
    public Transform target;

    public virtual void Awake() {
        InitStateMachine();
    }

    protected virtual void InitStateMachine()
    {
        m_StateMachine = new StateMachine<Character>(this);
        StateMachine.SetCurrentState(CIdleState.Instance);
        StateMachine.SetGlobalState(CGlobalState.Instance);
    }

    public virtual void IdleEnter() { }
    public virtual void IdleExecute() { }
    public virtual void IdleEnd() { }

    public virtual void MoveEnter() { }
    public virtual void MoveExecute() { }
    public virtual void MoveEnd() { }

    public virtual bool IsFinishMoveOnNavemesh()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SetupMove(Transform trsTarget)
    {
        StateMachine.ChangeState(CMoveState.Instance);
    }
}
