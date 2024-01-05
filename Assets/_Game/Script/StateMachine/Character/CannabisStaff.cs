using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannabisStaff : StaffBase
{
    public void WateringSetting(Transform target)
    {
        timeDoAction = 0;
        currentActionData = GameManager.Instance.charactorManager.GetActionData(ActionType.Watering);
        timeDoActionSetting = currentActionData.timeDoAction;
        currentPointFree.isActive = false;
        SetupMove(target, DoWatering);
        isFree = false;
    }

    void DoWatering() {
        StateMachine.ChangeState(WateringState.Instance);
    }

    public override void WateringEnter()
    {
        myAnim.SetBool("Watering", true);
    }
    public override void WateringExecute()
    {
        if (timeDoAction < timeDoActionSetting)
        {
            timeDoAction += Time.deltaTime;
        }
        else
        {
            GameManager.Instance.charactorManager.CallCannabisStaffBackFreePoint(this);
        }
    }
    public override void WateringExit() {
        isFree = true;
        myAnim.SetBool("Watering", false);
        
    }

    public override void CallBackFreePoint()
    {
        currentPointFree = GameManager.Instance.roomManager.cannabisManager.GetFreePoint();
        base.CallBackFreePoint();
    }
}
public class WateringState : State<Character>
{
    private static WateringState m_instance;
    public static WateringState Instance
    {
        get
        {
            if (m_instance == null) m_instance = new WateringState();
            return m_instance;
        }
    }

    public override void Enter(Character go)
    {
        go.WateringEnter();
    }

    public override void Execute(Character go)
    {
        go.WateringExecute();
    }

    public override void Exit(Character go)
    {
        go.WateringExit();
    }
}
