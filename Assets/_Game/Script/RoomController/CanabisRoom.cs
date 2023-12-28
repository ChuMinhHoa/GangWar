using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanabisRoom : RoomBase
{
    public List<Transform> pointDoActionWater = new List<Transform>();
    FreePosition freePositionTemp;
    public override void SpawnStaff()
    {
        for (int i = 0; i < totalStaff; i++) {
            freePositionTemp = GetFreePosition();
            GameManager.Instance.staffManager.SpawnCannabisStaff(freePositionTemp);
        }
    }
}
