using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotArm : MonoBehaviour
{

    private Vector3 mouse;
    [SerializeField] GameObject bullet;
    public Transform Crosshair;
    public bool canFire;
    private float timer;
    public float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mouse - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!canFire){
            timer += Time.deltaTime;
            if(timer > attackDelay){
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButton(0) && canFire){
            canFire = false;
            Instantiate(bullet, Crosshair.position, Quaternion.identity);
        }

    }
}
