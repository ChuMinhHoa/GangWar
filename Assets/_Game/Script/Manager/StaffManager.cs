using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public List<CannabisStaff> cannabisStaffs = new List<CannabisStaff> ();
    #region Spawn
    public void SpawnCannabisStaff(FreePosition freePositionTemp) {
        CannabisStaff newCannabisStaff = GameManager.Instance.pooling.GetCannabisStaff();
        newCannabisStaff.transform.position = freePositionTemp.pointStay.position;
        newCannabisStaff.transform.rotation = freePositionTemp.pointStay.rotation;
        freePositionTemp.isAtive = true;
        cannabisStaffs.Add(newCannabisStaff);
    }
    #endregion

    #region Call action 
    public void CallCannabisStaffToWaterTree() { }
    #endregion
}
