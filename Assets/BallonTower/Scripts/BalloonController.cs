using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _balloon;
    [SerializeField] private float _speed = 1f;

    private Vector3 _targetSize;

    private Rigidbody _rb;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        Components();
        InitialSize();
    }

    private void Components()
    {
        _rb = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void InitialSize()
    {
        _targetSize = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    private void Update()
    {
        LineRendererController();
        Strength();
        SizeChange();
    }

    private void Strength()
    {
        _rb.AddForce(Vector3.up * _speed, ForceMode.Impulse);
    }

    private void LineRendererController()
    {
        _target.position = _balloon.transform.position;

        _lineRenderer.SetPosition(0, new Vector3(0, 0.835f, -0.33f));
        _lineRenderer.SetPosition(1, _target.position);
        _lineRenderer.SetWidth(0.001f, 0.001f);

        _lineRenderer.startWidth = 0.01f;
        _lineRenderer.endWidth = 0.01f;
    }
    private void SizeChange()
    {
        if (gameObject.transform.localScale.magnitude <= _targetSize.magnitude)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
