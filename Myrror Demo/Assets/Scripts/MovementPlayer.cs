using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour {

    public float speed = 50, maxSpeedX = 5, maxSpeedY = 20, verticalJumpForce = 20, currentVerticalJumpforce, lateralHorizontalJumpForce = 30, lateralVerticalJumpForce = 30, contactPointY, contactPointX, mass = 5, gravity = 1.5f;
    Rigidbody2D player;
    float inputX;
    public Vector2 movement;
    Animator animator;
    ContactPoint2D[] contacts = new ContactPoint2D[1];
    bool jump = false, flipped = false;

	// Use this for initialization
	void Start ()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameManager.instance.SetPlayer(this.gameObject);
        player.mass = mass;
        player.gravityScale = gravity;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentVerticalJumpforce = verticalJumpForce * player.gravityScale;
        contactPointY = contacts[0].normal.y;
        contactPointX = contacts[0].normal.x;
        inputX = Input.GetAxis("Horizontal");
        HorizontalInput();
        JumpInput();
        Animations();
        if (Mathf.Abs(player.velocity.x) <= 0.1 || Mathf.Abs(player.velocity.y) >= 0.001)
            GetComponents<FMODUnity.StudioEventEmitter>()[2].Stop();
        else
        {
            FMODUnity.StudioEventEmitter audio = GetComponents<FMODUnity.StudioEventEmitter>()[2];
            if (!audio.IsPlaying()) audio.Play();
            audio.EventInstance.setParameterByName("Andando", Mathf.Abs(player.velocity.x) / maxSpeedX);
        }
        // Debug.Log(player.velocity.y);
    }
    void FixedUpdate()
    {
        player.AddForce(movement);
        player.velocity = new Vector2(Mathf.Clamp(player.velocity.x, -maxSpeedX, maxSpeedX), Mathf.Clamp(player.velocity.y, -maxSpeedY, maxSpeedY));
    }

    void HorizontalInput()
    {
        movement = new Vector2(speed * inputX, player.velocity.y);
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();

            if (contacts[0].normal.x > 0.9 && contacts[0].normal.x < 1.1 && player.velocity.y != 0) //Si colisiona por la derecha
            {
                movement = new Vector2(0, 0); //Cancela el movimiento
                player.velocity = new Vector2(0, 0);
                player.AddForce(new Vector2(lateralHorizontalJumpForce, lateralVerticalJumpForce * (Mathf.Abs(transform.localScale.y)/transform.localScale.y)), ForceMode2D.Impulse); //Salta en lateral 
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (contacts[0].normal.x < -0.9 && contacts[0].normal.x > -1.1 && player.velocity.y != 0) //Si colisiona por la izquierda
            {
                movement = new Vector2(0, 0);
                player.velocity = new Vector2(0, 0);
                player.AddForce(new Vector2(-lateralHorizontalJumpForce, lateralVerticalJumpForce * (Mathf.Abs(transform.localScale.y) / transform.localScale.y)), ForceMode2D.Impulse); //Salta en lateral 
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                movement = new Vector2(player.velocity.x, 0);  //Cancela la velocidad en Y
                player.AddForce(new Vector2(0, currentVerticalJumpforce), ForceMode2D.Impulse);  //Hace el salto en vertical             
            }
            jump = false;
        }
    }

    void Animations()
    {
        if (inputX != 0 /*&& jump*/)
        {
            if (!flipped) animator.Play("PlayerWalking");

            else animator.Play("WhitePlayerWalking");

            if (inputX < 0) transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

            else if (inputX > 0) transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        else if (inputX == 0)
        {
            if (!flipped)
            {
                animator.Play("Idle");
            }

            else animator.Play("WhiteIdle");
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        jump = true;
        collision.GetContacts(contacts);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        jump = false;
    }

    public void Respawn(Transform respawn)
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
        transform.position = respawn.position;
        player.velocity = Vector2.zero;
        Debug.Log("Respawned");
        transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.y));
        player.gravityScale = Mathf.Abs(player.gravityScale);
    }

    public void ChangeGravity()
    {
        player.gravityScale *= -1;
        transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);

        if (flipped) flipped = false;
        else flipped = true; 
    }

    public void FlipOnDeath()
    {
        flipped = false;
    }
}
