using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.SceneView;

public class GameManager : Singleton<GameManager>
{
    public ObjectPooling pooling;
    public CharactorManager charactorManager;
    public RoomManager roomManager;

    bool isTouching = false;
    Vector3 touchUp, touchDown;
    RoomInterface selectedRoom;
    private void Update()
    {
        if (Input.touchCount >0) {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
          
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            touchDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isTouching = true;
        }
        if (isTouching)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isTouching = false;
                touchUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector3.Distance(touchDown, touchUp) >= 0.1f) return;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.collider != null)
                {
                    selectedRoom = hit.collider.GetComponent<RoomInterface>();
                    if (selectedRoom != null)
                    {
                        //CameraMove.instance.ZoomOutCamera();
                        UIManager.Instance.ShowPanelUpgrade(selectedRoom);
                    }
                }
            }
        }
    }
}
