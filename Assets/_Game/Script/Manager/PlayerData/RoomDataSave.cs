using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomDataSave : SaveBase
{
    public RoomSave cannabisRoomSave;
    public override void LoadData(int level)
    {
        SetStringSave("RoomDataSave" + level);
        base.LoadData();
        string jData = GetJsonData();
        if (!string.IsNullOrEmpty(jData))
        {

        }
        else {

            IsMarkChangeData();
            SaveData();
        }
    }

    public int GetLevelRoomElementOnRoomType(RoomType rType, RoomElementType rElementType) {
        switch (rType) {
            case RoomType.CannabisRoom:
                return cannabisRoomSave.GetLevelRoomType(rElementType);
            default:
                return 0;
        }
    }

    public void UpgradeLevelElement(RoomType rType, RoomElementType rElementType) {
        switch (rType)
        {
            case RoomType.CannabisRoom:
                cannabisRoomSave.UpgradeLevelElement(rElementType);
                break;
            default:
                break;
        }
    }

    public bool IsHaveRoomDataSave(RoomType rType) {
        switch (rType)
        {
            case RoomType.CannabisRoom:
                return cannabisRoomSave != null;
            default:
                return false;
        }
    }

    public void SaveFirstRoomData(RoomData roomData) {
        switch (roomData.rType)
        {
            case RoomType.CannabisRoom:
                cannabisRoomSave = new RoomSave();
                cannabisRoomSave.rType = roomData.rType;
                cannabisRoomSave.InitRoomElementLevel(roomData.roomElementData);
                break;
            default:
                break;
        }
        IsMarkChangeData();
        SaveData();
    }

    public int GetTotalStaff(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.CannabisRoom:
                return cannabisRoomSave.totalStaff;
            default:
                return 0;
        }
    }
}

[System.Serializable]
public class RoomSave {
    public RoomType rType;
    public List<ElementRoomSave> elementRoomSaves = new List<ElementRoomSave>();
    public int totalStaff;
    public int GetLevelRoomType(RoomElementType rElementType)
    {
        return elementRoomSaves.Find(e => e.rElementType == rElementType).level;
    }

    public void UpgradeLevelElement(RoomElementType rElementType) {
        elementRoomSaves.Find(e => e.rElementType == rElementType).level++;
    }

    public void InitRoomElementLevel(List<RoomElementData> roomElementDatas) {
        for (int i = 0; i < roomElementDatas.Count; i++)
        {
            ElementRoomSave newElementRoomSave = new ElementRoomSave();
            newElementRoomSave.rElementType = roomElementDatas[i].rType;
            newElementRoomSave.level = 0;
            elementRoomSaves.Add(newElementRoomSave);
        }
    }
}

[System.Serializable]
public class ElementRoomSave {
    public RoomElementType rElementType;
    public int level;
}
