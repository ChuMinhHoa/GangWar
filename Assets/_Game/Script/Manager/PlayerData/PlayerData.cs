using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public RoomDataSave roomDataSave;

    public void LoadData() {
        roomDataSave.LoadData(currentLevel);
    }
}
