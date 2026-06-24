using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChracterControls : MonoBehaviour
{


    Rigidbody2D rb;
    public bool isGrounded = false;


    SpriteRenderer sprite;
    Animator anim;
    Vector2 savePoint;
    [SerializeField] float respawnHeight;
    [SerializeField] float speed = 5f, jumpForce = 7f;


    GameObject buildingPref;


    [SerializeField] LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        savePoint = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        Collider2D col = GetComponent<Collider2D>();

        isGrounded = Physics2D.OverlapCircle(transform.position - transform.up * ((col.bounds.extents.y / transform.localScale.y - col.offset.y) * transform.localScale.y), 0.01f, groundLayer);

        if (horizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            sprite.flipX = true;
        }


        if (horizontalInput != 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);


            anim.SetFloat("y", rb.velocity.y);


            if (isGrounded)
            {
                anim.SetFloat("y", 0);
            }


        }







        if (transform.position.y < respawnHeight)
        {
            transform.position = savePoint;

        }


        if (buildingPref)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildingPref.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);


            if (Input.GetMouseButtonDown(0))
            {
                buildingPref.GetComponent<Collider2D>().enabled = true;
                buildingPref = null;
            }
        }
    }
}


