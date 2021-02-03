using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour
{
    public bool face = true;
    public Transform heroiT;
    public float vel = 2.5f;
    public float vel2 = 10.0f;
    public float force = 6.5f;
    public Rigidbody2D heroiRB;
    public bool liberaPulo = false;
    public Animator anim;
    public bool vivo = true;

    void Start()
    {
        heroiT = GetComponent<Transform>();
        heroiRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo == true)
        {
            if (Input.GetKey(KeyCode.RightArrow) && !face)
            {
                Flip();
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && face)
            {
                Flip();
            }
        }

        if (vivo == true)
        {
            //Run
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(new Vector2(vel2 * Time.deltaTime,0));
                anim.SetBool("idle",false);
                anim.SetBool("walk",false);
                anim.SetBool("run",true);
            }else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(new Vector2(-vel2 * Time.deltaTime,0));
                anim.SetBool("idle",false);
                anim.SetBool("walk",false);
                anim.SetBool("run",true);
            }


            //Walk
            else if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(new Vector2(vel * Time.deltaTime,0));
                anim.SetBool("idle",false);
                anim.SetBool("walk",true);
                anim.SetBool("run",false);
            }else if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(new Vector2(-vel * Time.deltaTime,0));
                anim.SetBool("idle",false);
                anim.SetBool("walk",true);
                anim.SetBool("run",false);
            }else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("idle",true);
                anim.SetBool("walk",false);
                anim.SetBool("run",false);
            }


            

        }

        if (vivo == true)
        {
            //Jump
            if(Input.GetKeyDown(KeyCode.Space) && liberaPulo == true && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift)){
                heroiRB.AddForce(new Vector2(0,force), ForceMode2D.Impulse);
                anim.SetBool("side_jump", true);
                anim.SetBool("run", false);
                //anim.SetBool("walk", false);

            }else if (Input.GetKeyDown(KeyCode.Space) && liberaPulo == true && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
            {
                heroiRB.AddForce(new Vector2(0,force), ForceMode2D.Impulse);
                anim.SetBool("side_jump", true);
                anim.SetBool("run", false);
            }else if (Input.GetKeyDown(KeyCode.Space) && liberaPulo == true && Input.GetKey(KeyCode.RightArrow))
            {
                heroiRB.AddForce(new Vector2(0,force), ForceMode2D.Impulse);
                anim.SetBool("side_jump", true);
                anim.SetBool("walk", false);
            }else if (Input.GetKeyDown(KeyCode.Space) && liberaPulo == true && Input.GetKey(KeyCode.LeftArrow))
            {
                heroiRB.AddForce(new Vector2(0,force), ForceMode2D.Impulse);
                anim.SetBool("side_jump", true);
                anim.SetBool("walk", false);
            }else if (Input.GetKeyDown(KeyCode.Space) && liberaPulo == true)
            {
                heroiRB.AddForce(new Vector2(0,force), ForceMode2D.Impulse);
                anim.SetBool("side_jump", true);
                anim.SetBool("idle", false);
            }
        }



    }
    void Flip()
    {
        face = !face;
        Vector3 scala = heroiT.localScale;
        scala.x *= -1;
        heroiT.localScale = scala;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            liberaPulo = true;
            anim.SetBool("idle", true);
            anim.SetBool("side_jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            liberaPulo = false;
            
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("bomba"))
        {
            vivo = false;
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("death", true);
        }
    }
}
