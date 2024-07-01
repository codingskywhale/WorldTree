using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placementPanel;
    private GameObject currentPreviewObject;
    private Item selectedItem;
    private bool canPlace;

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
        placementPanel.SetActive(true);
    }

    public void InstallObject()
    {
        if (currentPreviewObject == null || !canPlace) return;

        SetObjectTransparency(currentPreviewObject, 1.0f);
        currentPreviewObject = null;
        placementPanel.SetActive(false);
    }

    public void CancelPlacement()
    {
        if (currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
        }
        currentPreviewObject = null;
        placementPanel.SetActive(false);
    }

    void HandlePreviewPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPreviewObject.transform.position = worldPosition;
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
