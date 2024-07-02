using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject uiElement;

    public void Toggle()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(!uiElement.activeSelf);
        }
    }
}
