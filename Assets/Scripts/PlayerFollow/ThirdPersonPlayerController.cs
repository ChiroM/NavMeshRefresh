using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    public delegate void ThirdPersonPlayerControllerEventHandler(ThirdPersonPlayerController playerController);
    public event ThirdPersonPlayerControllerEventHandler OnPlayer_Moved;

    public CharacterController characterController;
    private Vector3 movementVector;

    public float speed = 5;
    public float offsetHeight = .5f;
    private RaycastHit _hit;
    private float _previousHeight;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = 0;
        movementVector.z = Input.GetAxisRaw("Vertical");

        if (movementVector.x == 0 && movementVector.z == 0)
            return;

        characterController.Move(movementVector.normalized * speed * Time.deltaTime);
        AdjustHeight();

        OnPlayer_Moved?.Invoke(this);
    }

    private void AdjustHeight()
    {
        UnityHelper.GetRaycastHitFromPoint(transform.position, Vector3.down, out _hit);

        if (transform.position.y - _hit.point.y > offsetHeight)
        {
            movementVector = transform.position;
            movementVector.y = _hit.point.y + offsetHeight;
            transform.position = movementVector;

            _previousHeight = _hit.point.y;
        }
    }
}
