using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private int maxJumps = 2;
    private int jumpsLeft;

    private enum MovementState { idle, running, jumping, falling }
    // private MovementState state = MovementState.idle;
    // int wholeNumber = 16;
    // float decimalNumber = 4.54f;
    // string text = "bla bla";
    // bool boolean = false;

    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource runSFX;

    private bool isRunningSoundPlaying = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // =
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    private void Update()
    {
        // Kode bergerak lari
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Memeriksa apakah pemain bergerak dan berada di tanah untuk memainkan suara berlari
        if (dirX != 0f && IsGrounded() && !isRunningSoundPlaying) {
            runSFX.Play();
            isRunningSoundPlaying = true;
        } else if (dirX == 0f || !IsGrounded()) {
            runSFX.Stop();
            isRunningSoundPlaying = false;
        }

        // Kode bergerak loncat
        // if (Input.GetKeyDown("space")) {
        // if (Input.GetButtonDown("Jump") && IsGrounded()) {
        //     // rb.velocity = new Vector3(0, 14f, 0);
        //     jumpSFX.Play();
        //     rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        // }
        // =============
        if (Input.GetButtonDown("Jump")) {
            if (IsGrounded()) {
                jumpsLeft = maxJumps - 1; // Reset jumps left if grounded
                jumpSFX.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            } else if (jumpsLeft > 0) {
                jumpSFX.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft--;
            }
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // Kode Animasi lari

        MovementState state;

        if (dirX > 0f) {
            // anim.SetBool("running", true);
            state = MovementState.running;
            // runSFX.Play();
            sprite.flipX = false; // perubah sudut pandang antara depan/belakang
        } else if (dirX < 0f) {
            // anim.SetBool("running", true);
            state = MovementState.running;
            // runSFX.Play();
            sprite.flipX = true; // perubah sudut pandang antara depan/belakang
        } else {
            state = MovementState.idle;
        }

        // Jump kode

        if (rb.velocity.y > .1f) {
                state = MovementState.jumping;
            } else if (rb.velocity.y < -.1f ) {
                state = MovementState.falling;
            }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
