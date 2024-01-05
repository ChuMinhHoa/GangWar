using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannabisRoom : RoomBase
{
    [Header("==========CANNABIS ROOM==========")]
    public List<PointDoAction> pointDoActionWater = new List<PointDoAction>();
    PointDoAction freePositionTemp;
    public override void SpawnStaff()
    {
        for (int i = 0; i < totalStaff; i++) {
            freePositionTemp = GetFreePosition();
            GameManager.Instance.staffManager.SpawnCannabisStaff(freePositionTemp);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            freePositionTemp = GetCannabisNeedWater();
            if (freePositionTemp != null)
            {
                freePositionTemp.isActive = true;
                GameManager.Instance.charactorManager.CallCannabisStaffToWaterTree(freePositionTemp.pointStay);
            }
        }
    }

    public PointDoAction GetCannabisNeedWater() {
        for (int i = 0;i < pointDoActionWater.Count; i++) {
            if (!pointDoActionWater[i].isActive)
            {
                return pointDoActionWater[i];
            }
        }
        return null;
    }
}
