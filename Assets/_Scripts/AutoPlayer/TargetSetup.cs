using UnityEngine;
using System.Collections.Generic;

public class TargetSetup : MonoBehaviour
{
    public PlayerMovement playerMovement;

    void Start()
    {
        List<Transform> targetList = new List<Transform>();

        // 'Water' �±װ� �޸� ��ü ã��
        GameObject[] waterObjects = GameObject.FindGameObjectsWithTag("Water");
        foreach (GameObject obj in waterObjects)
        {
            targetList.Add(obj.transform);
        }

        // 'Tree' �±װ� �޸� ��ü ã��
        GameObject[] treeObjects = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject obj in treeObjects)
        {
            targetList.Add(obj.transform);
        }

        // PlayerMovement ��ũ��Ʈ�� ��ǥ ���� ����
        playerMovement.targets = targetList.ToArray();
    }
}
