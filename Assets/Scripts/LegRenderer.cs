using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRenderer : MonoBehaviour
{
    public Transform[] points;

    LineRenderer lineRenderer;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = points.Length;
        lineRenderer.numCornerVertices = 10;
        lineRenderer.numCapVertices = 10;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(GetPositions());
    }

    Vector3[] GetPositions() {
        Vector3[] result = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++) {
            result[i] = points[i].position;
        }
        return result;
    }
}
