using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rb;
  private Animator anim;
  private enum State{idle,running,jumping,falling}
  private State state = State.idle;
  private Collider2D coll;
[SerializeField] private LayerMask ground;
[SerializeField] private float speed= 5f;
[SerializeField] private float jumpForce= 10f;
  private void Start()
  {
    rb=GetComponent<Rigidbody2D>();
    anim=GetComponent<Animator>();
    coll=GetComponent<Collider2D>();
  }
  private void Update()
  {
      Movement();
      animationState();
      anim.SetInteger("state",(int)state);
  }
  private void Movement()
  {
    float hDirection=Input.GetAxis("Horizontal");
    if(hDirection<0)
    {
      rb.velocity= new Vector2(-speed,rb.velocity.y);
      transform.localScale= new Vector2(-1,1);
    }
    else if(hDirection>0)
    {
      rb.velocity= new Vector2(speed,rb.velocity.y);
      transform.localScale= new Vector2(1,1);
    }
    else
    {

    }
    if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
      {
       rb.velocity= new Vector2(rb.velocity.x,jumpForce);
       state=State.jumping;
      }
  }
  private void animationState()
  {
    if(state==State.jumping)
    {
        if(rb.velocity.y<.1f)
        {
          state=State.falling;
        }
    }
   else if(state==State.falling)
    {
      if(coll.IsTouchingLayers(ground))
      {
        state=State.idle;
      }
    }
    else if(Mathf.Abs(rb.velocity.x)> 2f)
    {
      state = State.running;
    }
    else
    {
      state=State.idle;
    }
  }
}
