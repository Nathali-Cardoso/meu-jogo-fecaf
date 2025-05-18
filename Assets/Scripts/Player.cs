using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    private float move;
    private bool isOnFloor;
    private bool isMoving;
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;
    private AudioSource jumpSound;
    private AudioSource diamondSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
        diamondSound = GameObject.Find("DiamondSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocityY);

        if (Input.GetButtonDown("Jump") && isOnFloor) {
            rb.AddForce(new Vector2(rb.linearVelocityX, jump));
            isOnFloor = false;
            jumpSound.Play();
            diamondSound.Play();
        }
    
        if (move > 0) {
            isMoving = true;
            sprite.flipX = false;
        }

        else if (move < 0) {
            isMoving = true;
            sprite.flipX = true;
        }

        else {
            isMoving = false;
        }


        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isOnFloor", isOnFloor);


    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isOnFloor = true;
        }
    }
}
