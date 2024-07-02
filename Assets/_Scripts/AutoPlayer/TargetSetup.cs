using UnityEngine;
using System.Collections.Generic;

public class TargetSetup : MonoBehaviour
{
    public PlayerMovement playerMovement;

    void Start()
    {
        List<Transform> targetList = new List<Transform>();

        // 'Water' 태그가 달린 물체 찾기
        GameObject[] waterObjects = GameObject.FindGameObjectsWithTag("Water");
        foreach (GameObject obj in waterObjects)
        {
            targetList.Add(obj.transform);
        }

        // 'Tree' 태그가 달린 물체 찾기
        GameObject[] treeObjects = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject obj in treeObjects)
        {
            targetList.Add(obj.transform);
        }

        // PlayerMovement 스크립트에 목표 지점 설정
        playerMovement.targets = targetList.ToArray();
    }
}
