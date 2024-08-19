using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] BoxCollider2D bounds; 
    Camera mainCamera; // ���� ī�޶�

    Vector3 minBounds; 
    Vector3 maxBounds; 

    float halfHeight; 
    float halfWidth; 

    void Start()
    {
        mainCamera = Camera.main;


        minBounds = bounds.bounds.min;
        maxBounds = bounds.bounds.max;

        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * mainCamera.aspect;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // ī�޶��� ���ο� ��ġ ���
            float clampedX = Mathf.Clamp(target.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(target.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

            // ���ο� ��ġ�� ī�޶� �̵�
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
