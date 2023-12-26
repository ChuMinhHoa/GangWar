using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface RoomInterface {
    public Transform GetElementPosition(RoomElementType roomElement, int level);
    public List<RoomElementData> GetRoomElementDatas();
}
public class RoomBase<RoomType> : MonoBehaviour, RoomInterface
{
    public RoomType roomType;
    public int totalStaff;
    public List<RoomElementBase> roomElements = new List<RoomElementBase>();
    public List<FreePosition> freePositions = new List<FreePosition>();
    public List<StaffBase> staffs = new List<StaffBase>();
    public RoomElementBase roomElementTemp;
    public RoomData roomData;
    public void InitData(RoomData roomData) { 
        this.roomData = roomData;
        if (!ProfileManager.Instance.playerData.roomDataSave.IsHaveRoomDataSave(roomData.rType))
            ProfileManager.Instance.playerData.roomDataSave.SaveFirstRoomData(roomData);
        for (int i = 0; i < roomElements.Count; i++)
        {
            InitDataOnRoomElement(roomData.roomElementData[i]);
            ClearObject(roomElements[i]);
        }
        SpawnStaff();
    }

    public virtual void SpawnStaff() {

    }

    public virtual void InitDataOnRoomElement(RoomElementData roomElement) {

        for (int i = 0; i < roomElements.Count; i++)
        {
            roomElements[i].rData = roomElement;
        }
    }

    void ClearObject(RoomElementBase roomElementBase) {
        for (int i = 0; i < roomElementBase.pointSpawns.Count; i++)
        {
            if (roomElementBase.pointSpawns[i].childCount > 0)
            {
                Destroy(roomElementBase.pointSpawns[i].GetChild(0).gameObject);
            }
        }
    }

    public virtual RoomElementBase GetFurniture(RoomElementType furnitureType) {
        return roomElements.Find(e => e.rType == furnitureType);
    }

    public virtual FreePosition GetFreePosition() {
        for (int i = 0; i < freePositions.Count; i++)
        {
            if (!freePositions[i].isAtive)
            {
                return freePositions[i];
            }
        }
        return null;
    }

    public Transform GetElementPosition(RoomElementType roomElement, int level)
    {
        roomElementTemp = roomElements.Find(e => e.rType == roomElement);
        if (roomElementTemp != null)
        {
            return roomElementTemp.pointSpawns[level];
        }
        return null;
    }

    public List<RoomElementData> GetRoomElementDatas()
    {
        return roomData.roomElementData;
    }
}

[System.Serializable]
public class FreePosition {
    public Transform pointStay;
    public bool isAtive;
    public FreeActionType actionType;
}
