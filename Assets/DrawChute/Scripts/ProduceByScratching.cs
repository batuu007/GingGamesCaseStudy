using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceByScratching : MonoBehaviour
{
    [SerializeField] private Draw _drawCubePrefab;

    private void OnEnable()
    {
        DrawEvents.onDrawingEnded += OnDrawLineEnded;
    }

    private void OnDisable()
    {
        DrawEvents.onDrawingEnded -= OnDrawLineEnded;
    }

    private void OnDrawLineEnded(Vector3[] positions)
    {
        ClearDraw();
        for (int i = 0; i < positions.Length - 1; i++)
        {
            Vector3 direction = positions[i + 1] - positions[i];
            Draw newDraw = Instantiate(_drawCubePrefab, transform);
            newDraw.Initialize(positions[i] + (direction.magnitude / 2 * direction.normalized), direction, direction.magnitude);
        }
    }
    private void ClearDraw()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
