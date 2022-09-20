using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private LayerMask _drawAreaLayer;
    [SerializeField] private Camera _uiCamera;
    [Header("DrawLine")]
    [SerializeField] [Range(0f, 1f)] private float _drawLineFrequency = 0.1f;
    [SerializeField] [Range(0f, 2f)] private float _minLineDistance = 0.2f;

    private LineRenderer _lineRenderer;
    private Coroutine _onDrawCoroutine;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>(true);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDrawStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnDrawEnd();
        }
    }
    private void OnDrawStart()
    {
        if (_onDrawCoroutine != null) return;

        Vector3 mouseWorldPos = _uiCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 100f, _drawAreaLayer);
        if (rayHit)
        {
            Vector3 linePos = rayHit.point;
            linePos.z = 10f;
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, linePos);

            _lineRenderer.gameObject.SetActive(true);
            _onDrawCoroutine = StartCoroutine(OnDraw());
        }
    }

    private IEnumerator OnDraw()
    {
        while (true)
        {
            yield return new WaitForSeconds(_drawLineFrequency);

            Vector3 mouseWorldPos = _uiCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 100f, _drawAreaLayer);
            if (rayHit)
            {
                Vector3 linePos = rayHit.point;
                linePos.z = 10f;

                if (((linePos - _lineRenderer.GetPosition(_lineRenderer.positionCount - 1)).magnitude) < _minLineDistance) continue;

                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, linePos);
            }
            else
            {
                OnDrawEnd();
            }
        }
    }

    private void OnDrawEnd()
    {
        if (_onDrawCoroutine == null) return;

        StopCoroutine(_onDrawCoroutine);
        _onDrawCoroutine = null;

        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        DrawEvents.onDrawingEnded?.Invoke(positions);

        _lineRenderer.gameObject.SetActive(false);
        _lineRenderer.positionCount = 0;
    }
}
