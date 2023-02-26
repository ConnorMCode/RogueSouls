using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public GameObject staminaBar;
    public Animator animator;
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

    void Start()
    {
        //set initial dashwait
        dashWaitActive = dashWait;
    }

    // Update is called once per frame
    void Update()
    {
        //animate stamina bar
        staminaRatio = dashWaitActive/dashWait;
        staminaBar.transform.localScale = new Vector3(staminaRatio, .25f, 1f);

        //dont allow movement while dashing
        if(isDashing){
            return;
        }

        //movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        //animation
        if(Mathf.Abs(rb.velocity.magnitude) > 0){
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }

        //dash
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
}
