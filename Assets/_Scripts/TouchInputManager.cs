using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public ResourceManager resourceManager; // ResourceManager ����
    public int IncreaseAmount = 10; // Ŭ���� ������ ���� �� �ʱⰪ

    void Update()
    {
        // ��ġ �Է� ����
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� �Ǵ� ��ġ �Է�
        {
            resourceManager.IncreaseWater(IncreaseAmount); // ���� �� ����
        }
    }

    // ��ȭ ��ư Ŭ�� �� ȣ��� �޼���
    public void UpgradeIncreaseAmount()
    {
        IncreaseAmount += 10; // �������� 10�� ������Ŵ
    }
}
