using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public ResourceManager resourceManager; // ResourceManager ����

    void Update()
    {
        // ��ġ �Է� ����
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� �Ǵ� ��ġ �Է�
        {
            resourceManager.IncreaseWater(10); // ���� �� ����
        }
    }
}

