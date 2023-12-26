using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDataConfigLevel", menuName = "ScriptableObject/RoomDataConfig")]
public class RoomDataConfig : ScriptableObject
{
    public List<RoomData> roomDatas = new List<RoomData>();

    public RoomData GetRoomData(RoomType rType)
    {
        return roomDatas.Find(e=>e.rType == rType);
    }
}

[System.Serializable]
public class RoomData {
    public RoomType rType;
    public List<RoomElementData> roomElementData = new List<RoomElementData>();
}
