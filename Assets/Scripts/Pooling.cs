using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    private static Pooling _instance;
    public static Pooling Instance => _instance;

    [SerializeField] GameObject cubePrefab;
    [SerializeField] int startSpawnNumber;

    public Vector3 areaLimits;

    public Queue<GameObject> cubes = new Queue<GameObject>();

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        AddToPool(startSpawnNumber, cubePrefab);
        SpawnFromPool(startSpawnNumber);
    }

    void AddToPool(int qtd, GameObject targetObject)
    {
        for (int i = 0; i < qtd; i++)
        {
            GameObject create = Instantiate(targetObject, this.transform);
            create.gameObject.SetActive(false);
        }
    }

    public void SpawnFromPool(int qtd)
    {
        for (int i = qtd; i > 0; i--)
        {
            GetFromPool(i).SetActive(true);
        }
        UIManager.Instance.GetPrefabsCount();
    }

    public GameObject GetFromPool(int qtd)
    {
        if (cubes.Count < qtd)
        {
            AddToPool(qtd - cubes.Count, cubePrefab);
        }

        return cubes.Dequeue();
    }

    public void ReturnToPool(int qtd)
    {
        if (FindNearestNeighbour.GetNeighbours() == null) return;
        if (qtd > FindNearestNeighbour.GetNeighbours().Count) qtd = FindNearestNeighbour.GetNeighbours().Count;
        for (int i = qtd; i > 0; i--)
        {
            FindNearestNeighbour.GetNeighbours()[i-1].gameObject.SetActive(false);
        }
        UIManager.Instance.GetPrefabsCount();
    }
}
