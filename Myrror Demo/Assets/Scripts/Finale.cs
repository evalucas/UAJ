using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour {
    public int speed = 1;
    public int rotation;
    bool move = false, owo = true;
    Animator animator;
	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("Kabumba");
	}
	
	// Update is called once per frame
	void Update () {

        /*if (Mathf.Abs(transform.position.x) > 3 && move)
        {
            move = false;
            animator.Play("Smoke"); //Destroy(this.gameObject);
        }
        else*/ if ((transform.position.x - transform.lossyScale.x/2)*transform.lossyScale.x > 0 || move && owo)
        {
            transform.Translate(new Vector2(-transform.lossyScale.x * speed * Time.deltaTime, 0));
            animator.Play("PlayerWalking");
        }
        else if(owo)
        {
            animator.Play("Idle");
            Invoke("StartMoving", 1.5f);
        }
        Debug.Log(Time.time);

            
    }
    void StartMoving()
    {
        move = true;
    }
    IEnumerator Kabumba()
    {
        yield return new WaitForSeconds(6.5f);
        owo = false;
        animator.Play("Smoke");
        transform.Rotate(new Vector3(0, 0, 90 * rotation));
        yield return new WaitForSeconds(0.8f);
        GameManager.instance.ChangeScene("Menu");
        Destroy(this.gameObject);
    }
}
