using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class CameraController : MonoBehaviour
{
    public Transform cameraRoot;
    public Camera mainCamera;
    //public CameraInfo mainCameraInfo;
    [SerializeField] Transform minCamTrs;
    [SerializeField] Vector3 minCameraPos = new Vector3(-10f, 0f, -10f);
    [SerializeField] Transform maxCamTrs;
    [SerializeField] Vector3 maxCameraPos = new Vector3(10f, 0f, 10f);

    [Header("==========Camera controller==========")]
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    bool lastPointNull;
    Vector3 vectorDir;
    Vector3 newPosition;
    Vector3 lastPoint;
    bool controlTakeover;

    [SerializeField] float cameraZoomSpeed;
    Touch firstFinger;
    Touch secondFinger;
    float lastZoomDistance;
    float newZoomDistance;
    float zoomValue;
    [SerializeField] float maxZoomValue = 30;
    [SerializeField] float minZoomValue = 5;
    [SerializeField] float zoomOnElement = 15;
    [SerializeField] float zoomDefalut = 25;


    private void Start()
    {
        cameraRoot.position = Vector3.zero;
        if (minCamTrs != null)
            minCameraPos = minCamTrs.position;
        if (maxCamTrs != null)
            maxCameraPos = maxCamTrs.position;
    }

    private void Update()
    {
        ControlCamera();
    }

    void ControlCamera()
    {
        if (UIManager.Instance.isHasPopupOnScene) return;
        if (Input.touchCount == 2)
        {
            firstFinger = Input.GetTouch(0);
            secondFinger = Input.GetTouch(1);
            newZoomDistance = Vector3.Distance(firstFinger.position, secondFinger.position);
            if (newZoomDistance != lastZoomDistance)
                zoomValue = newZoomDistance > lastZoomDistance ? 1 : -1;
            else
                zoomValue = 0;
            lastZoomDistance = newZoomDistance;
            mainCamera.orthographicSize -= Time.deltaTime * cameraZoomSpeed * zoomValue;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoomValue, maxZoomValue);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPoint = Input.mousePosition;
                lastPointNull = false;
            }
            if (Input.GetMouseButton(0) && !lastPointNull && lastPoint != Input.mousePosition)
            {
                vectorDir = Input.mousePosition - lastPoint;
                vectorDir.Normalize();

                vectorDir *= -1;
                vectorDir = cameraRoot.forward * vectorDir.y + cameraRoot.right * vectorDir.x;

                lastPoint = Input.mousePosition;

                newPosition += vectorDir * movementSpeed;


            }
            if (Input.GetMouseButtonUp(0))
            {
                lastPoint = Vector3.zero;
                lastPointNull = true;
            }

            cameraRoot.position = Vector3.Lerp(cameraRoot.position, newPosition, Time.deltaTime * movementTime);
            //LimitCameraOffset();

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (mainCamera.fieldOfView > minZoomValue)
                mainCamera.fieldOfView -= Time.deltaTime * cameraZoomSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (mainCamera.fieldOfView < maxZoomValue)
                mainCamera.fieldOfView += Time.deltaTime * cameraZoomSpeed;
        }
    }

    public void OnRoomMode(Transform trsPointTarget) {
        newPosition = trsPointTarget.position;
        DOVirtual.Float(mainCamera.fieldOfView, zoomOnElement, 0.25f, (value) => {
            mainCamera.fieldOfView = value;
        });
        StartCoroutine(CoroutineMoveCamera());
    }

    IEnumerator CoroutineMoveCamera()
    {
        while (Time.deltaTime * movementTime < 1f)
        {
            yield return new WaitForEndOfFrame();
            cameraRoot.position = Vector3.Lerp(cameraRoot.position, newPosition, Time.deltaTime * movementTime);
        }
    }

    public void OnOutRoomMode() {
        StopCoroutine(CoroutineMoveCamera());
        DOVirtual.Float(mainCamera.fieldOfView, zoomDefalut, 0.25f, (value) => {
            mainCamera.fieldOfView = value;
        });
    }

    void LimitCameraOffset()
    {
        if (cameraRoot.position.x > maxCameraPos.x)
        {
            cameraRoot.position = new Vector3(maxCameraPos.x, cameraRoot.position.y, cameraRoot.position.z);
        }
        if (cameraRoot.position.x < minCameraPos.x)
        {
            cameraRoot.position = new Vector3(minCameraPos.x, cameraRoot.position.y, cameraRoot.position.z);
        }
        if (cameraRoot.position.z > maxCameraPos.z)
        {
            cameraRoot.position = new Vector3(cameraRoot.position.x, cameraRoot.position.y, maxCameraPos.z);
        }
        if (cameraRoot.position.z < minCameraPos.z)
        {
            cameraRoot.position = new Vector3(cameraRoot.position.x, cameraRoot.position.y, minCameraPos.z);
        }
    }
}
