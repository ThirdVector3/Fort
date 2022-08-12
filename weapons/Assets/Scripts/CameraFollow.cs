using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0f, 10f)]
    public float smoothFactor;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset,smoothFactor * Time.deltaTime);
    }
}
