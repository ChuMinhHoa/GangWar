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
    [SerializeField] Button btnNext;
    [SerializeField] TextMeshProUGUI txtPriceUpgrade;
    [SerializeField] TextMeshProUGUI txtElementName;
    [SerializeField] TextMeshProUGUI txtDescription;
    [SerializeField] RectTransform rectPrice;
    [SerializeField] GameObject objNext;
    [SerializeField] ScrollRect scroll;
    [SerializeField] Slider sUpgrade;
    [SerializeField] GridLayoutCustom layoutCustom;

    RoomInterface currentRoomInterface;
    float currentProgressUpgrade;
    int slotAmount;
    int totalSlot;

    public override void Awake()
    {
        uIPanelType = UIPanelType.PanelUpgrade;
        base.Awake();
        btnClose.onClick.AddListener(ClosePanel);
        btnUpgrade.onClick.AddListener(UpgradeRoomElement);
        btnNext.onClick.AddListener(NextElement);
        upgradeSheet.SetActionCallBack(ActionCallBackOnUpgradeSlot);
        slotAmount = layoutCustom.columnCount;
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
        ChangeSliderMaxValue(roomInterface.GetTotalRoomUpgrade());
        currentProgressUpgrade = roomInterface.GetCurrentProgressUpgrade();
        ChangeSliderUpgrade(currentProgressUpgrade);
    }

    void ActionCallBackOnUpgradeSlot(SlotBase<RoomElementData> slot) {
        totalSlot = upgradeSheet.listSlots.Count;
        int currentLevel = (slot as UpgradeSlot).currentLevel;
        objNext.SetActive((slot as UpgradeSlot).isMaxlevel);
        btnUpgrade.gameObject.SetActive(!objNext.activeSelf);

        if (!objNext.activeSelf)
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
        currentProgressUpgrade++;
        ChangeSliderUpgrade(currentProgressUpgrade);
        UIAnimationController.BtnAnimZoomBasic(btnUpgrade.transform, 0.25f);
    }

    void NextElement() {
        UIAnimationController.BtnAnimZoomBasic(btnNext.transform, 0.25f, () => {
            upgradeSheet.GetNextSlot();
        });
    }

    void ClosePanel() {
        uIPanelAnimOpenAndClose.OnClose(() => {
            GameManager.Instance.cameraController.OnOutRoomMode();
            upgradeSheet.currentSlot = null;
            UIManager.Instance.ClosePanelUpgrade();
        });
    }

    IEnumerator ResetPriceRect() {
        yield return new WaitForNextFrameUnit();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectPrice);
    }

    void ChangeSliderMaxValue(float valueMax) { sUpgrade.maxValue = valueMax; }

    void ChangeSliderUpgrade(float value) { sUpgrade.value = value; }
}
