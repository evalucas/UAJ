using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedad : MonoBehaviour {

    MovementPlayer player;
    Rigidbody2D rb;
    public float minSpeedY;
    Vector2 initialSpeed;
    FMODUnity.StudioEventEmitter audio;

    private void Start()
    {
        audio = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        rb = collider.gameObject.GetComponent<Rigidbody2D>();
        initialSpeed = rb.velocity;
        //Debug.Log(initialSpeed);
    }
    

    void OnTriggerExit2D(Collider2D collider)
    {
        player = collider.gameObject.GetComponent<MovementPlayer>();
        rb.velocity = initialSpeed;
        player.ChangeGravity();
        if (Mathf.Abs(rb.velocity.y) < minSpeedY) rb.velocity = new Vector2(rb.velocity.x , minSpeedY * Mathf.Sign(rb.gravityScale));
        Debug.Log(rb.velocity.y);
        audio.Play();
        audio.EventInstance.setParameterByName("Velocidad", Mathf.Abs(rb.velocity.y));
    }
}
