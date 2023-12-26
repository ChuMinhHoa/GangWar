using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : Singleton<ProfileManager>
{
    public PlayerData playerData;
    public DataConfig dataConfig;

    protected override void Awake()
    {
        base.Awake();
        playerData.LoadData();
        GameManager.Instance.roomManager.InitData();
    }
}
