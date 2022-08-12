using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public float distance;
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = mousePos;

        this.transform.position = targetPos;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }
}
