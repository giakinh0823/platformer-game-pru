using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    public Vector3 postionOffset;
    [SerializeField]
    public Vector2 xLimit;
    [SerializeField]
    public Vector2 yLimit;

    [Range(0, 1)]
    [SerializeField]
    public float smoothTime;

    public void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void LateUpdate()
    {
        Vector3 targetTransform = target.position + postionOffset;
        targetTransform = new Vector3(Mathf.Clamp(targetTransform.x, xLimit.x, xLimit.y), Mathf.Clamp(targetTransform.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetTransform, ref velocity, smoothTime);
    }
}
