using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    private GameObject currentPreviewObject;
    private GameObject currentOverlayObject;
    private Item selectedItem;
    private bool canPlace;    
    public Inventory inventory;
    public UIPlacementManager uiPlacementManager;
    public GameObject overlayPrefab;
    public CircleCollider2D territoryCollider;

    private void Update()
    {
        if (currentPreviewObject != null)
        {
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
        if (currentOverlayObject != null)
        {
            Destroy(currentOverlayObject);
        }

        currentPreviewObject = Instantiate(selectedItem.prefab);                        
        currentOverlayObject = Instantiate(overlayPrefab, currentPreviewObject.transform);
        currentOverlayObject.transform.localPosition = Vector3.zero;

        AdjustOverlaySize();        
    }

    public bool InstallObject()
    {
        if (currentPreviewObject == null || !canPlace)
        {           
            return false;
        }

        currentPreviewObject = null;
        Destroy(currentOverlayObject);
        inventory.RemoveItem(selectedItem);        
        return true;
    }

    public void CancelPlacement()
    {
        if (currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
        }
        if (currentOverlayObject != null)
        {
            Destroy(currentOverlayObject);
        }
        currentPreviewObject = null;
        currentOverlayObject = null;        
    }

    private void HandlePreviewPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                        
            Vector3 clampedPosition = ClampPositionToTerritory(worldPosition);
            currentPreviewObject.transform.position = clampedPosition;
            currentOverlayObject.transform.position = clampedPosition;            
        }
    }

    private Vector3 ClampPositionToTerritory(Vector3 position)
    {
        Vector3 territoryCenter = territoryCollider.transform.position;
        float territoryRadius = territoryCollider.radius * territoryCollider.transform.localScale.x;

        Vector3 direction = position - territoryCenter;
        if (direction.magnitude > territoryRadius)
        {
            direction = direction.normalized * territoryRadius;
        }

        return territoryCenter + direction;
    }

    private void CheckPlacementValidity()
    {
        canPlace = true; 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(currentPreviewObject.transform.position, currentPreviewObject.GetComponent<Collider2D>().bounds.size, 0);

        foreach (var collider in colliders)
        {            
            if (collider.CompareTag("Territory"))
            {
                continue;
            }

            if (collider.gameObject != currentPreviewObject)
            {
                canPlace = false;
                break;
            }
        }

        SetOverlayColor(canPlace);
    }


    private void SetOverlayColor(bool canPlace)
    {
        var overlayRenderer = currentOverlayObject.GetComponent<SpriteRenderer>();
        if (overlayRenderer != null)
        {
            Color color = canPlace ? new Color(0.0f, 1.0f, 0.0f, 0.9f) : new Color(1.0f, 0.0f, 0.0f, 0.9f);
            overlayRenderer.color = color;            
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

    private Bounds GetBounds(GameObject obj)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            return new Bounds();
        }

        Bounds bounds = renderers[0].bounds;
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }

    private void AdjustOverlaySize()
    {
        if (currentOverlayObject != null && currentPreviewObject != null)
        {
            var bounds = GetBounds(currentPreviewObject);
            float offset = 0.15f;

            currentOverlayObject.transform.localScale = new Vector3(
                (bounds.size.x + offset) / currentOverlayObject.GetComponent<SpriteRenderer>().bounds.size.x,
                (bounds.size.y + offset) / currentOverlayObject.GetComponent<SpriteRenderer>().bounds.size.y,
                1
            );
        }
    }
}
