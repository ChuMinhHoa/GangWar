using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CannabisManager
{
    public CannabisRoom room;
    public RoomData roomData;
    public void InitData()
    {
        roomData = ProfileManager.Instance.dataConfig.roomDataConfig.GetRoomData(RoomType.CannabisRoom, room.roomId);
        room.InitData(roomData);
    }

    public PointDoAction GetFreePoint()
    {
        return room.GetFreePosition();
    }
}
