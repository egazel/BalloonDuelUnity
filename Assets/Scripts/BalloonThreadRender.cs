using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class BalloonThreadRender : MonoBehaviour
{
    [SerializeField] private Transform connectedObj;
    [SerializeField] private Vector3 _balloonOffset;
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private float _lineThickness;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, base.transform.position + _balloonOffset);
        lineRenderer.SetPosition(1, connectedObj.position + _playerOffset);

        lineRenderer.startWidth = _lineThickness;
        lineRenderer.endWidth = _lineThickness;

        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }
}
