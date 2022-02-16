using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private int hits;

    [SerializeField] GameObject spawnPanel;

    [Header("Text References")]
    [SerializeField] Text prefabsCount;
    [SerializeField] Text spawnText;
    [SerializeField] Text hitsText;

    [Header("Spawn commands")]
    [SerializeField] char Spawn;
    [SerializeField] char despawn;

    private void OnEnable() => FindNearestNeighbour.OnCubesChange += GetPrefabsCount;

    private void OnDisable() => FindNearestNeighbour.OnCubesChange -= GetPrefabsCount;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReleaseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GetPrefabsCount()
    {
        prefabsCount.text = $"Prefabs: {FindNearestNeighbour.GetNeighbours().Count}";
    }

    public void UpdateHitsCount(int qtd)
    {
        hits += qtd;
        hitsText.text = $"Hits: {hits}";
    }

    void SpawnCubes()
    {
        if (Input.GetKeyDown((KeyCode)Spawn) && spawnPanel.activeInHierarchy)
        {
            int qtd;
            int.TryParse(spawnText.text, out qtd);
            qtd = Mathf.Abs(qtd);
            Pooling.Instance.SpawnFromPool(qtd);
            LockCursor();
        }
    }

    void DespawnCubes()
    {
        if (Input.GetKeyDown((KeyCode)despawn) && spawnPanel.activeInHierarchy)
        {
            int qtd;
            int.TryParse(spawnText.text, out qtd);
            qtd = Mathf.Abs(qtd);
            Pooling.Instance.ReturnToPool(qtd);
            LockCursor();
        }
    }

    public void CheckCubesList()
    {
        if (FindNearestNeighbour.GetNeighbours().Count == 0)
        {
            ReleaseCursor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCubes();
        DespawnCubes();
    }
}
