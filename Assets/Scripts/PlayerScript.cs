using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;
    public GameObject staminaBar;
    Vector2 pos;
    Vector2 mouse;
    Vector2 movement;
    public float dashSpeed;
    public float dashTime;
    public float dashWait;
    private float dashWaitActive;
    private float staminaRatio;
    private bool isDashing;

    //justin was here
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        dashWaitActive = dashWait;
    }

    // Update is called once per frame
    void Update()
    {
        staminaRatio = dashWaitActive/dashWait;

        staminaBar.transform.localScale = new Vector3(staminaRatio, .25f, 1f);

        if(isDashing){
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        drawLine();

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        if(Input.GetKeyDown("space") && dashWaitActive >= dashWait){
            StartCoroutine(Dash());
            dashWaitActive = 0;
            staminaBar.GetComponent<SpriteRenderer>().color = new Color(250f, 0f, 0f);
        }
    }

    void FixedUpdate() {
        if(dashWaitActive < dashWait){
            dashWaitActive += Time.fixedDeltaTime; 
        }else{
            staminaBar.GetComponent<SpriteRenderer>().color = new Color(0f, 200f, 0f);
        }
    }

    private IEnumerator Dash(){
        isDashing = true;
        rb.velocity = movement * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    private void drawLine(){
        Vector2 diff;
        float mag;

        pos = rb.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        diff = mouse - pos;
        

        mag = Mathf.Sqrt((diff.x*diff.x) + (diff.y*diff.y));

        //diff.x = diff.x / mag;
        //diff.x += pos.x;
        //diff.y *= diff.y / mag;
        //diff.y += pos.y;

        diff = diff / mag;

        diff = diff * 1.5f;

        diff = diff + pos;
        //take the differences, square em, add em up, divide em by that sum, use that ratio as lengths of a new vector

        lr.SetPosition(0, pos);
        lr.SetPosition(1, diff);
    }
}
