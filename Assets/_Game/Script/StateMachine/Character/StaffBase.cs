using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBase : Character
{
    public bool isFree;
    public float timeDoActionSetting;
    public float timeDoAction;
    public PointDoAction currentPointFree;

    public void SetCurrentPointFree(PointDoAction pointFree) {
        currentPointFree = pointFree;
    }

    public virtual void ResetStaffBase() {
        isFree = true;
    }

    public virtual void CallBackFreePoint() {
        SetupMove(currentPointFree.pointStay, null);
    }
}
