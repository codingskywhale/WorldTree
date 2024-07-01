using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private GameObject currentPreviewObject;
    private Item selectedItem;
    private bool canPlace;
    private Vector3 placementPosition;

    void Update()
    {
        if (currentPreviewObject != null)
        {
            HandlePreviewPosition();
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
    }

    public void InstallObject()
    {
        if (currentPreviewObject == null || !canPlace) return;

        SetObjectTransparency(currentPreviewObject, 1.0f);
        currentPreviewObject.transform.position = placementPosition; // 마지막 위치로 고정
        currentPreviewObject = null;
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
}