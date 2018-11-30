using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class StoneAgeController : MonoBehaviour {
    [SerializeField]
    float force;
    [SerializeField]
    float slideTime;
    [SerializeField]
    float atackTime;
    [SerializeField]
    LayerMask layer;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator anim;
   

    //animations run, jump, doubleJump, slide, run&atack
    float runColSizeX = 0.22f;
    float runColSizeY = 0.97f;
    bool slide = false;
    bool atack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    //Controls in update
    private void Update()
    {
        if (isGrounded() && !slide && !atack)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(Slide());
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(Atack());
            }
        }

    }
    bool isGrounded()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - boxCollider.size.y * transform.localScale.y / 2);
        if (Physics2D.Raycast(origin, Vector2.down, 0.1f, layer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Slide()
    {
        slide = true;
        boxCollider.size = new Vector2(runColSizeY, runColSizeX);
        yield return new WaitForSeconds(slideTime);
        boxCollider.size = new Vector2(runColSizeX, runColSizeY);
        slide = false;
    }
    IEnumerator Atack()
    {
        atack = true;
        boxCollider.size = new Vector2(3f*runColSizeX, runColSizeY);
        yield return new WaitForSeconds(atackTime);
        boxCollider.size = new Vector2(runColSizeX, runColSizeY);
        atack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (atack)
        {
            if (collision.transform.GetComponent<Damageble>() != null)
            {
                collision.transform.GetComponent<Damageble>().TakeDamage();
            }
        }
    }

}
