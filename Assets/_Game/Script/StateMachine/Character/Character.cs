using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
public interface CharacterInterface {
    public void SetupMove(Transform trsTarget, UnityAction actionCallBack);
}
public class Character : MonoBehaviour, CharacterInterface
{
    public StateMachine<Character> StateMachine { get { return m_StateMachine; } }
    protected StateMachine<Character> m_StateMachine;
    public NavMeshAgent agent;
    public ActionData currentActionData;
    public Animator myAnim;
    public UnityAction actionCallback;

    [Header("======TRANSFORM======")]
    public Transform target;

    public virtual void Awake() {
        InitStateMachine();
    }

    public virtual void Update() {
        StateMachine.Update();
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

    public virtual void MoveEnter() {
        agent.SetDestination(target.position);
        myAnim.SetBool("Move", true);
    }
    public virtual void MoveExecute() {
        if (IsFinishMoveOnNavemesh())
        {
            StateMachine.ChangeState(CIdleState.Instance);
            if (actionCallback != null)
            {
                actionCallback();
                actionCallback = null;
            }
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
    public virtual void MoveEnd() {
        myAnim.SetBool("Move", false);
    }

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

    public void SetupMove(Transform trsTarget, UnityAction actionCallback)
    {
        this.actionCallback = actionCallback;
        target = trsTarget;
        StateMachine.ChangeState(CMoveState.Instance);
    }

    #region Cannabis Staff
    public virtual void WateringEnter() { 
    
    }

    public virtual void WateringExecute()
    {

    }

    public virtual void WateringExit()
    {

    }
    #endregion
}
