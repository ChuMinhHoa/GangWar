using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorManager : MonoBehaviour
{
    #region Spawn
    public void SpawnCannabisStaff(Transform trsSpawn) {
        GameManager.Instance.pooling.GetCannabisStaff();
    }
    #endregion

    #region Call action 
    public void CallCannabisStaffToWaterTree() { }
    #endregion
}
