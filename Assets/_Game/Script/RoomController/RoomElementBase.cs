using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomElementBase
{
    public RoomElementType rType;
    //public int roomElementID;
    public List<Transform> pointSpawn;
    public List<RoomElementData> rData;
    int totalUpgrade = 0;
    int currentUpgrade = 0;

    public void AddRoomData(RoomElementData roomElementDatas) { 
        rData.Add(roomElementDatas);
        totalUpgrade += roomElementDatas.prices.Count;
    }

    public Transform GetElementTransform(int rElementID)
    {
        for (int i = 0; i < rData.Count; i++)
        {
            if (rData[i].rElementID == rElementID)
            {
                return pointSpawn[i];
            }

        }
        return null;
    }

    public int GetPointIndex(int rElementID) {
        for (int i = 0; i < rData.Count; i++)
        {
            if (rData[i].rElementID == rElementID)
            {
                return i;
            }

        }
        return -1;
    }

    public int GetTotalUpgrade() { return totalUpgrade; }
    public int GetCurrentUpgrade(RoomType roomType, int roomID) {
        currentUpgrade = 0;
        for (int i = 0; i < rData.Count; i++)
        {
            currentUpgrade += ProfileManager.Instance.playerData.roomDataSave.GetLevelRoomElementOnRoomType(roomType, rData[i].rElementID, roomID);
        }
        return currentUpgrade;
    }
}
