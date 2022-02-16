using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private Vector3 movementLimits;

    [SerializeField] float moveSpeed;

    private Vector3 destiny;
    private float minimunDistance = 0.1f;

    private void Start()
    {
        movementLimits = Pooling.Instance.areaLimits;
    }

    Vector3 RandomMovement()
    {
        if (Vector3.Distance(transform.position, destiny) >  minimunDistance) return destiny;

        Vector3 randomDestination = new Vector3(Random.Range(0, Mathf.Abs(movementLimits.x)),
                                                Random.Range(0, Mathf.Abs(movementLimits.y)),
                                                Random.Range(0, Mathf.Abs(movementLimits.z)));
        destiny = randomDestination;
        return destiny;
    }

    void MoveObject(Vector3 destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject(RandomMovement());
    }
}
