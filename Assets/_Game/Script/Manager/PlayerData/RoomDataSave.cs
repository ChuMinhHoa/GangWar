using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomDataSave : SaveBase
{
    public List<RoomSave> cannabisRoomSaves = new List<RoomSave>();
    RoomSave roomSaveTemp;
    public override void LoadData(int level)
    {
        SetStringSave("RoomDataSave" + level);
        string jData = GetJsonData();
        if (!string.IsNullOrEmpty(jData))
        {
            RoomDataSave data = JsonUtility.FromJson<RoomDataSave>(jData);
            cannabisRoomSaves = data.cannabisRoomSaves;
        }
        else {
            IsMarkChangeData();
            SaveData();
        }
    }

    public int GetLevelRoomElementOnRoomType(RoomType rType, int rElementID, int roomID) {
        switch (rType) {
            case RoomType.CannabisRoom:
                return GetRoomSave(cannabisRoomSaves, roomID).GetLevelRoomType(rElementID);
            default:
                return 0;
        }
    }

    RoomSave GetRoomSave(List<RoomSave> roomSaves, int rID) {
        roomSaveTemp = roomSaves.Find(e => e.roomID == rID);
        return roomSaveTemp;
    }

    public void UpgradeLevelElement(RoomType rType, int rElementID) {
        switch (rType)
        {
            case RoomType.CannabisRoom:
                cannabisRoomSaves[0].UpgradeLevelElement(rElementID);
                break;
            default:
                break;
        }
        IsMarkChangeData();
        SaveData();
    }

    public bool IsHaveRoomDataSave(RoomType rType, int roomID) {
        switch (rType)
        {
            case RoomType.CannabisRoom:
                return cannabisRoomSaves.Find(e=>e.roomID==roomID) != null;
            default:
                return false;
        }
    }

    public void SaveFirstRoomData(RoomData roomData) {
        RoomSave roomSave = new RoomSave();
        roomSave.rType = roomData.rType;
        roomSave.InitRoomElementLevel(roomData.roomElementData);
        switch (roomData.rType)
        {
            case RoomType.CannabisRoom:
                cannabisRoomSaves.Add(roomSave);
                break;
            default:
                break;
        }
        IsMarkChangeData();
        SaveData();
    }

    public int GetTotalStaff(RoomType roomType, int roomID)
    {
        switch (roomType)
        {
            case RoomType.CannabisRoom:
                return cannabisRoomSaves.Find(e=>e.roomID == roomID).totalStaff;
            default:
                return 0;
        }
    }
}

[System.Serializable]
public class RoomSave {
    public RoomType rType;
    public int roomID;
    public List<ElementRoomSave> elementRoomSaves = new List<ElementRoomSave>();
    public int totalStaff;
    public int GetLevelRoomType(int rElementID)
    {
        return elementRoomSaves.Find(e => e.rElementID == rElementID).level;
    }

    public void UpgradeLevelElement(int rElementID) {
        elementRoomSaves.Find(e => e.rElementID == rElementID).level++;
    }

    public void InitRoomElementLevel(List<RoomElementData> roomElementDatas) {
        for (int i = 0; i < roomElementDatas.Count; i++)
        {
            ElementRoomSave newElementRoomSave = new ElementRoomSave();
            newElementRoomSave.rElementType = roomElementDatas[i].rType;
            newElementRoomSave.rElementID = i;
            newElementRoomSave.level = roomElementDatas[i].levelDefault;
            elementRoomSaves.Add(newElementRoomSave);
        }
    }
}

[System.Serializable]
public class ElementRoomSave {
    public RoomElementType rElementType;
    public int rElementID;
    public int level;
}
