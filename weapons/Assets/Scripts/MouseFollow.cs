using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public CameraFollow script;
    public Camera cam;
    public Transform player;
    public float distance;
    void Update()
    {
        Vector3 mousePos =  cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos)/2;
        targetPos.x = Mathf.Clamp(targetPos.x, -distance + player.position.x, distance + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -distance + player.position.y, distance + player.position.y);

        this.transform.position = targetPos;
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, script.offset.z);
    }
}
