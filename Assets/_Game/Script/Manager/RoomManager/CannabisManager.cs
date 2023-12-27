using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CannabisManager
{
    public CanabisRoom room;
    public RoomData roomData;
    public void InitData()
    {
        roomData = ProfileManager.Instance.dataConfig.roomDataConfig.GetRoomData(RoomType.CannabisRoom, room.roomId);
        room.InitData(roomData);
    }
}
