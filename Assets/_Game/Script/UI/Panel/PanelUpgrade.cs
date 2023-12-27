using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIAnimation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelUpgrade : UIPanelBase
{
    [SerializeField] Image imgIconRoomElement;
    [SerializeField] Image imgIconRoom;
    [SerializeField] UpgradeSheet upgradeSheet;
    [SerializeField] Button btnClose;
    [SerializeField] Button btnUpgrade;
    [SerializeField] TextMeshProUGUI txtPriceUpgrade;
    [SerializeField] TextMeshProUGUI txtElementName;
    [SerializeField] TextMeshProUGUI txtDescription;
    [SerializeField] RectTransform rectPrice;
    [SerializeField] GameObject objMax;
    [SerializeField] ScrollRect scroll;
    RoomInterface currentRoomInterface;
    public override void Awake()
    {
        uIPanelType = UIPanelType.PanelUpgrade;
        base.Awake();
        btnClose.onClick.AddListener(ClosePanel);
        btnUpgrade.onClick.AddListener(UpgradeRoomElement);
        upgradeSheet.SetActionCallBack(ActionCallBackOnUpgradeSlot);
    }

    private void OnEnable()
    {
        scroll.verticalNormalizedPosition = 1;
    }

    public void ShowElementInfor(RoomInterface roomInterface) {
        currentRoomInterface = roomInterface;
        upgradeSheet.LoadData(roomInterface.GetRoomElementDatas());
        upgradeSheet.SetCurrentRoomType(roomInterface.GetRoomType());
        upgradeSheet.listSlots[0].OnChoose();
    }

    void ActionCallBackOnUpgradeSlot(SlotBase<RoomElementData> slot) {
        int currentLevel = (slot as UpgradeSlot).currentLevel;
        objMax.SetActive((slot as UpgradeSlot).isMaxlevel);

        if (!objMax.activeSelf)
            txtPriceUpgrade.text = slot.data.GetPrice(currentLevel).ToString();

        txtDescription.text = ProfileManager.Instance.dataConfig.roomDataConfig.GetRoomElementDescription(slot.data.rType);
        txtElementName.text = slot.data.rType.ToString();

        imgIconRoomElement.sprite = ProfileManager.Instance.dataConfig.spriteDataConfig.GetSpriteRoomElement(slot.data.rType);
       
        if (gameObject.activeSelf)
            StartCoroutine(ResetPriceRect());

        GameManager.Instance.cameraController.OnRoomMode(currentRoomInterface.GetElementTransform(slot.data.rType, slot.data.rElementID));
    }

    void UpgradeRoomElement() {
        ProfileManager.Instance.playerData.roomDataSave.UpgradeLevelElement(currentRoomInterface.GetRoomType(), upgradeSheet.currentSlot.data.rElementID);
        currentRoomInterface.OnUpgradeElement(upgradeSheet.currentSlot.data.rType, upgradeSheet.currentSlot.data.rElementID, (upgradeSheet.currentSlot as UpgradeSlot).currentLevel + 1);
        upgradeSheet.ReloadDataOnSlot();
        ActionCallBackOnUpgradeSlot(upgradeSheet.currentSlot);
        UIAnimationController.BtnAnimZoomBasic(btnUpgrade.transform, 0.25f);
        
    }

    void ClosePanel() {
        uIPanelAnimOpenAndClose.OnClose(() => {
            GameManager.Instance.cameraController.OnOutRoomMode();
            UIManager.Instance.ClosePanelUpgrade();
        });
    }

    IEnumerator ResetPriceRect() {
        yield return new WaitForNextFrameUnit();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectPrice);
    }
}
