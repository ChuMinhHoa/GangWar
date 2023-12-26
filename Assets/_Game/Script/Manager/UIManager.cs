using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIPanelType, GameObject> listPanel = new Dictionary<UIPanelType, GameObject>();
    [SerializeField] Transform trsPanelParents;
    public bool isHasPopupOnScene;
    public void RegisterPanel(UIPanelType uIPanelType, GameObject obj) {
        GameObject objTemp = null;
        if (!listPanel.TryGetValue(uIPanelType, out objTemp)) {
            listPanel.Add(uIPanelType, obj);
        }
        obj.SetActive(false);
    }

    public GameObject GetPanel(UIPanelType panelType) {
        GameObject panel = null;
        if (!listPanel.TryGetValue(panelType, out panel))
        {
            switch (panelType)
            {
                case UIPanelType.PanelUpgrade:
                    panel = Instantiate(Resources.Load("UI/PanelUpgrade") as GameObject, trsPanelParents);
                    break;
            }
            if (panel) panel.SetActive(true);
            return panel;
        }
        return listPanel[panelType];
    }

    public void ShowPanelUpgrade(RoomInterface roomInterface) {
        isHasPopupOnScene = true;
        GameObject obj = GetPanel(UIPanelType.PanelUpgrade);
        obj.SetActive(true);
        obj.GetComponent<PanelUpgrade>().ShowElementInfor(roomInterface);
    }
    public void ClosePanelUpgrade() {
        isHasPopupOnScene = false;
        GameObject obj = GetPanel(UIPanelType.PanelUpgrade);
        obj.SetActive(false);
    }
}
