using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float delay = 0;
    public float speed = 0;
    public float damage = 0;
    IEnumerator death()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(death());
    }
    void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector2 (0,speed*Time.fixedDeltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}
