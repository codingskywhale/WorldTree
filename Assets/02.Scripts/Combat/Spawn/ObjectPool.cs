using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 풀 데이터를 정의할 데이터 모음 정의
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary = new Dictionary<string, Queue<GameObject>>();

    public GameObject SpawnFromPool(string tag)
    {
        // 애초에 Pool이 존재하지 않는 경우
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        // 제일 오래된 객체를 재활용
        GameObject obj = PoolDictionary[tag].Dequeue();
        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }

    public void AddtoPool(string tag, GameObject prefab, int poolSize)
    {
        Pool pool = new Pool();

        pool.tag = tag;
        pool.prefab = prefab;
        pool.size = poolSize;

        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < pool.size; i++)
        {
            // Awake하는 순간 오브젝트풀에 들어갈 Instantitate 일어나기 때문에 터무니없는 사이즈 조심
            GameObject obj = Instantiate(pool.prefab, this.transform);
            obj.SetActive(false);
            // 줄의 가장 마지막에 세움.
            objectPool.Enqueue(obj);
        }
        PoolDictionary.Add(pool.tag, objectPool);
    }
}