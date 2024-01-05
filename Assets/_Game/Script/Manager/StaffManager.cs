using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public List<CannabisStaff> cannabisStaffs = new List<CannabisStaff> ();
    #region Spawn
    public void SpawnCannabisStaff(PointDoAction freePositionTemp) {
        CannabisStaff newCannabisStaff = GameManager.Instance.pooling.GetCannabisStaff();
        newCannabisStaff.transform.position = freePositionTemp.pointStay.position;
        newCannabisStaff.transform.rotation = freePositionTemp.pointStay.rotation;
        newCannabisStaff.SetCurrentPointFree(freePositionTemp);
        newCannabisStaff.ResetStaffBase();
        freePositionTemp.isActive = true;
        cannabisStaffs.Add(newCannabisStaff);
    }
    #endregion

    #region Call action 
    public void CallCannabisStaffToWaterTree(Transform pointCannabis) {
        for (int i = 0; i < cannabisStaffs.Count; i++)
        {
            if (cannabisStaffs[i].isFree)
            {
                cannabisStaffs[i].WateringSetting(pointCannabis);
                return;
            }
        }
    }

    public void CallCannabisStaffBackFreePoint(CannabisStaff cannabisStaff) {
        cannabisStaff.CallBackFreePoint();
    }
    #endregion

    public ActionData GetActionData(ActionType actionType) {
        return ProfileManager.Instance.dataConfig.actionDataConfig.GetActionData(actionType);
    }
}
