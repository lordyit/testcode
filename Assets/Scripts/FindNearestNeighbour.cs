using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindNearestNeighbour : MonoBehaviour
{
    private LineRenderer line;

    private static List<FindNearestNeighbour> neighbours = new List<FindNearestNeighbour>();

    private float timer;
    private float waitTime = 0.5f;

    private Transform closestCube;

    public static Action OnCubesChange;

    public static List<FindNearestNeighbour> GetNeighbours()
    {
        return neighbours;
    }

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        neighbours.Add(this);
        FindNearest();
        OnCubesChange?.Invoke();
    }

    private void OnDisable()
    {
        neighbours.Remove(this);
        Pooling.Instance.cubes.Enqueue(this.gameObject);
        OnCubesChange?.Invoke();
    }

    Transform FindNearest()
    {
        closestCube = this.transform;
        float closestNeighbour = Mathf.Infinity;
        foreach (FindNearestNeighbour neighbour in neighbours)
        {
            if (neighbour == this) continue;

            float currentDistance = Vector3.Distance(transform.position, neighbour.transform.position);
            if (currentDistance < closestNeighbour)
            {
                closestCube = neighbour.transform;
                closestNeighbour = currentDistance;
            }
        }
        return closestCube;
    }

    void FindNearestWithTimer()
    {
        timer += Time.deltaTime;
        if (timer < waitTime) return;

        closestCube = FindNearest();
        timer = 0;
    }

    void DrawLine(Vector3 targetTransform)
    {
        Vector3[] linePosition = new Vector3[] { transform.position, targetTransform };
        line.SetPositions(linePosition);
    }

    private void Update()
    {
        DrawLine(closestCube.position);
        FindNearestWithTimer();
    }
}
