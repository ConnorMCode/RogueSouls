using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mouse;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float force;
    public float lifetime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Vector3 direction = mouse - transform.position;
        Vector3 rotation = transform.position - mouse;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!sr.isVisible || timer > lifetime){
            Destroy(gameObject);
        }
    }
}
