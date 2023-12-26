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

    public override void InitData(RoomElementData data)
    {
        base.InitData(data);
        currentLevel = ProfileManager.Instance.playerData.currentLevel;
        txtLevel.text = currentLevel.ToString();
        if (currentLevel == 0)
            LockMode();
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
}
