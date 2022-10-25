using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableWaypointManager : MonoBehaviour
{
    private static readonly string _waypointTag = "DraggableWaypoint";
    private static readonly string _waypointLayerName = "Waypoints";

    public Transform _currWaypoint;

    private RaycastHit _hit;
    private Vector3 _lastWaypointPosition;
    private int _layerMask;
    private bool _isDragging;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask(_waypointLayerName);
    }

    // Update is called once per frame
    void Update()
    {
        HandleDragFunctionality();
    }

    private void HandleDragFunctionality()
    {
        if (!_isDragging)
            DragStartCheck();
        else
            HandleWaypointDragging();
    }

    private void DragStartCheck()
    {
        if (Input.GetMouseButtonDown(0) && UnityHelper.GetRaycastHitFromCamera(out _hit, _waypointTag))
        {
            _currWaypoint = _hit.transform;
            _isDragging = true;
        }
    }

    private void HandleWaypointDragging()
    {
        if (!Input.GetMouseButton(0))
        {
            _isDragging = false;
            return;
        }

        SetCurrentWaypointPosition();
    }

    private void SetCurrentWaypointPosition()
    {
        if (UnityHelper.GetRaycastHitFromCamera(out _hit, layerMask: _layerMask))
        {
            if (_hit.point == _lastWaypointPosition || _hit.normal != Vector3.up)
                return;

            Debug.Log(_hit.transform.name);
            _lastWaypointPosition = _currWaypoint.position;

            _currWaypoint.position = _hit.point;
        }
    }
}
