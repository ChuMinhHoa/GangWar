using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface RoomInterface {
    public Transform GetElementPosition(RoomElementType roomElement);
    public List<RoomElementData> GetRoomElementDatas();
    public RoomType GetRoomType();
    public void OnUpgradeElement(RoomElementType rElementType, int id, int level);

    public Transform GetElementTransform(RoomElementType roomElement, int id);
    public int GetRoomID();
    public float GetTotalRoomUpgrade();
    public float GetCurrentProgressUpgrade();
}
public class RoomBase : MonoBehaviour, RoomInterface
{
    public RoomType roomType;
    public int roomId;
    public int totalStaff;
    public List<RoomElementBase> roomElements = new List<RoomElementBase>();
    public List<FreePosition> freePositions = new List<FreePosition>();
    public List<StaffBase> staffs = new List<StaffBase>();
    public RoomElementBase roomElementTemp;
    public RoomData roomData;
    public void InitData(RoomData roomData) { 
        this.roomData = roomData;
        if (!ProfileManager.Instance.playerData.roomDataSave.IsHaveRoomDataSave(roomData.rType, roomId))
        {
            ProfileManager.Instance.playerData.roomDataSave.SaveFirstRoomData(roomData);
        }
        for (int i = 0; i < roomElements.Count; i++)
        {
            //roomElements[i].roomElementID = i;
            roomElements[i].rData = roomData.roomElementData[i];
            ClearObject(roomElements[i]);
        }
        SpawnStaff();
    }

    public virtual void SpawnStaff() {

    }

    void ClearObject(RoomElementBase roomElementBase) {
        if (roomElementBase.pointSpawn.childCount > 0)
        {
            Destroy(roomElementBase.pointSpawn.GetChild(0).gameObject);
        }
        int elementLevel = ProfileManager.Instance.playerData.roomDataSave.GetLevelRoomElementOnRoomType(roomType, roomElementBase.rData.rElementID, roomId);
        UpdatePrefOnPosition(roomElementBase, elementLevel);
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

    public Transform GetElementPosition(RoomElementType roomElement)
    {
        roomElementTemp = roomElements.Find(e => e.rType == roomElement);
        if (roomElementTemp != null)
        {
            return roomElementTemp.pointSpawn;
        }
        return null;
    }

    public List<RoomElementData> GetRoomElementDatas()
    {
        return roomData.roomElementData;
    }

    public RoomType GetRoomType()
    {
        return roomData.rType;
    }

    public void OnUpgradeElement(RoomElementType rElementType, int id, int level)
    {
        for (int i = 0;i < roomElements.Count;i++)
        {
            if (roomElements[i].rType == rElementType && roomElements[i].rData.rElementID == id)
            {
                UpdatePrefOnPosition(roomElements[i], level);
                break;
            }
        }
    }
    GameObject objPref;
    Transform trsTemp;
    void UpdatePrefOnPosition(RoomElementBase roomElement, int level) {
        objPref = GameManager.Instance.GetModelPref(roomElement.rType, level);
        if (objPref != null) {
            if (roomElement.pointSpawn.childCount > 0)
                Destroy(roomElement.pointSpawn.GetChild(0).gameObject);
            trsTemp = Instantiate(objPref, roomElement.pointSpawn).transform;
            trsTemp.transform.localPosition = Vector3.zero;
            trsTemp.transform.rotation = roomElement.pointSpawn.rotation;
        }
    }

    public Transform GetElementTransform(RoomElementType roomElement, int id)
    {
       return roomElements.Find(e => e.rType == roomElement && e.rData.rElementID == id).pointSpawn.transform;
    }

    public int GetRoomID()
    {
        return roomId;
    }

    float totalUpgradeReturn;
    float currentUpgradeProgress;
    public float GetTotalRoomUpgrade()
    {
        totalUpgradeReturn = 0;
        for (int i = 0; i < roomElements.Count; i++)
        {
            totalUpgradeReturn += roomElements[i].rData.prices.Count;
        }
        return totalUpgradeReturn;
    }

    public float GetCurrentProgressUpgrade()
    {
        currentUpgradeProgress = 0;
        for (int i = 0; i < roomElements.Count; i++)
        {
            currentUpgradeProgress += ProfileManager.Instance.playerData.roomDataSave.GetLevelRoomElementOnRoomType(roomType, roomElements[i].rData.rElementID, roomId);
        }
        return currentUpgradeProgress;
    }
}

[System.Serializable]
public class FreePosition {
    public Transform pointStay;
    public bool isAtive;
    public FreeActionType actionType;
}
