using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDataConfigLevel", menuName = "ScriptableObject/RoomDataConfig")]
public class RoomDataConfig : ScriptableObject
{
    public List<RoomData> roomDatas = new List<RoomData>();
    public List<RoomElementDescription> elementDescriptions = new List<RoomElementDescription>();
#if UNITY_EDITOR
    private void OnEnable()
    {
        for (int i = 0; i < roomDatas.Count; i++)
        {
            roomDatas[i].roomID = i;
            roomDatas[i].InitID();
        }
    }
#endif
    public RoomData GetRoomData(RoomType rType, int roomID)
    {
        return roomDatas.Find(e => e.rType == rType && e.roomID == roomID);
    }

    public string GetRoomElementDescription(RoomElementType id) { return elementDescriptions.Find(e => e.rElementType == id).description; }
}

[System.Serializable]
public class RoomData {
    public RoomType rType;
    public int roomID;
    public List<RoomElementData> roomElementData = new List<RoomElementData>();

    public void InitID() {
        for (int i = 0; i < roomElementData.Count; i++)
        {
            roomElementData[i].rElementID = i;
        }
    }
}

[System.Serializable]
public class RoomElementDescription {
    public RoomElementType rElementType;
    public string description;
}
