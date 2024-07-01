using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    private GameObject currentPreviewObject;
    private Item selectedItem;
    private bool canPlace;
    private Vector3 placementPosition;
    public Inventory inventory;
    public UIPlacementManager uiPlacementManager;

    void Update()
    {
        if (currentPreviewObject != null)
        {
            // 마우스가 UI 위에 있지 않을 때만 이동 처리
            if (!IsPointerOverUIObject())
            {
                HandlePreviewPosition();
            }
            CheckPlacementValidity();
        }
    }

    public void SelectItem(Item item)
    {
        selectedItem = item;
        if (currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
        }

        currentPreviewObject = Instantiate(selectedItem.prefab);
        SetObjectTransparency(currentPreviewObject, 0.5f);
        placementPosition = currentPreviewObject.transform.position; // 초기 위치 설정
    }

    public bool InstallObject()
    {
        if (currentPreviewObject == null || !canPlace)
        {
            return false;
        }

        SetObjectTransparency(currentPreviewObject, 1.0f);
        currentPreviewObject = null;
        inventory.RemoveItem(selectedItem);
        return true;
    }

    public void CancelPlacement()
    {
        if (currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
        }
        currentPreviewObject = null;
    }

    void HandlePreviewPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPreviewObject.transform.position = worldPosition;
            placementPosition = worldPosition; // 설치할 위치 저장
        }
    }

    void CheckPlacementValidity()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(currentPreviewObject.transform.position, currentPreviewObject.GetComponent<Collider2D>().bounds.size, 0);
        canPlace = true;

        foreach (var collider in colliders)
        {
            if (collider.gameObject != currentPreviewObject)
            {
                canPlace = false;
                break;
            }
        }

        SetObjectColor(currentPreviewObject, canPlace ? Color.white : Color.red);
    }

    void SetObjectTransparency(GameObject obj, float alpha)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            foreach (var material in renderer.materials)
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
        }
    }

    void SetObjectColor(GameObject obj, Color color)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            foreach (var material in renderer.materials)
            {
                material.color = color;
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}