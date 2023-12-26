using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelUpgrade : UIPanelBase
{
    [SerializeField] Image imgIconRoomElement;
    [SerializeField] Image imgIconRoom;
    [SerializeField] UpgradeSheet upgradeSheet;
    [SerializeField] Button btnClose;
    [SerializeField] TextMeshProUGUI txtPriceUpgrade;
    public override void Awake()
    {
        uIPanelType = UIPanelType.PanelUpgrade;
        base.Awake();
        btnClose.onClick.AddListener(ClosePanel);
        upgradeSheet.SetActionCallBack(ActionCallBackOnUpgradeSlot);
    }

    public void ShowElementInfor(RoomInterface roomInterface) {
        upgradeSheet.LoadData(roomInterface.GetRoomElementDatas());
        upgradeSheet.listSlots[0].OnChoose();
    }

    void ActionCallBackOnUpgradeSlot(SlotBase<RoomElementData> slot) {
        txtPriceUpgrade.text = slot.data.prices[(slot as UpgradeSlot).currentLevel].ToString();
    }

    void ClosePanel() {
        UIManager.Instance.ClosePanelUpgrade();
    }
}
