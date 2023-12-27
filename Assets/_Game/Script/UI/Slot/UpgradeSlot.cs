using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : SlotBase<RoomElementData>
{
    public int currentLevel;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] Image imgBackground;
    [SerializeField] Sprite bgDeselect;
    [SerializeField] Sprite bgSelect;
    [SerializeField] Sprite bgLock;
    public bool isMaxlevel;
    RoomType roomType;

    public void SetCurrentRoomType(RoomType roomType) { this.roomType = roomType; } 
    public override void InitData(RoomElementData data)
    {
        base.InitData(data);
        currentLevel = ProfileManager.Instance.playerData.roomDataSave.GetLevelRoomElementOnRoomType(roomType, data.rElementID);
        txtLevel.text = currentLevel.ToString();
        imgIcon.sprite = ProfileManager.Instance.dataConfig.spriteDataConfig.GetSpriteRoomElement(data.rType);
        isMaxlevel = data.prices.Count == currentLevel;
        DeSelectedSwitchMode();
    }

    public void DeSelectedSwitchMode() {
        if (currentLevel == 0)
            LockMode();
        else
            DeselectMode();
    }

    public void LockMode() { imgBackground.sprite = bgLock; }

    public void DeselectMode() { imgBackground.sprite = bgDeselect; }

    public override void OnChoose()
    {
        imgBackground.sprite = bgSelect;
        base.OnChoose();
    }

    public override void ReloadData()
    {
        currentLevel = ProfileManager.Instance.playerData.roomDataSave.GetLevelRoomElementOnRoomType(roomType, data.rElementID);
        txtLevel.text = currentLevel.ToString();
        isMaxlevel = data.prices.Count == currentLevel;
    }
}
