using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanabisRoom : RoomBase
{
    public List<Transform> pointDoActionWater = new List<Transform>();
    public List<CannabisStaff> cannabisStaffs = new List<CannabisStaff>();
    CannabisStaff cannabisStaffTemp;
    FreePosition freePositionTemp;
    public override void SpawnStaff()
    {
        totalStaff = ProfileManager.Instance.playerData.roomDataSave.GetTotalStaff(roomType, roomId);
        for (int i = 0; i < totalStaff; i++) {
            CannabisStaff newCannabisStaff = GameManager.Instance.pooling.GetCannabisStaff();
            freePositionTemp = GetFreePosition();
            newCannabisStaff.transform.position = freePositionTemp.pointStay.position;
            newCannabisStaff.transform.rotation = freePositionTemp.pointStay.rotation;
            freePositionTemp.isAtive = true;
            cannabisStaffs.Add(newCannabisStaff);
        }
    }
}
