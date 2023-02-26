using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotArm : MonoBehaviour
{

    private Vector3 mouse;
    [SerializeField] GameObject bullet;
    public Transform Crosshair;
    [SerializeField] GameObject reloadBar;
    public bool canFire;
    private float timer;
    public float attackDelay;
    private float reloadRatio;

    void Start()
    {
        
    }

    void Update()
    {
        //track mouse location for crosshair
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mouse - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //attack delay and animation
        if(!canFire){
            timer += Time.deltaTime;

            //animation
            reloadRatio = timer/attackDelay;
            reloadBar.transform.localScale = new Vector3(reloadRatio, .25f, 1f);

            if(timer > attackDelay){
                canFire = true;
                timer = 0;
                reloadBar.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
            }
        }

        //attack
        if(Input.GetMouseButton(0) && canFire){
            canFire = false;
            Instantiate(bullet, Crosshair.position, Quaternion.identity);
            reloadBar.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);
        }

    }
}
