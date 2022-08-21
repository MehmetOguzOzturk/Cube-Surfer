using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public GameObject player;
    public Playercontroller playercontroller;
    public ParticleSystem particle;
    public bool iscollected;
    public float index;
    int number;
    bool onGround;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    public void Collect()
    {
        iscollected=true; 
    }

    public void Setindex( float height)
    {
        index=height;
    }

    private void OnTriggerEnter(Collider other) 
    {
        
        if (other.gameObject.CompareTag("Stack")&&transform.parent==player.transform)
        {
            player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Jump");
            StartCoroutine(playercontroller.Score(number));
            player.GetComponent<Playercontroller>().height++;
            player.GetComponent<Playercontroller>().SetHeight();           
            other.gameObject.GetComponent<StackManager>().iscollected=true;
            other.gameObject.GetComponent<StackManager>().Setindex( player.GetComponent<Playercontroller>().height);
            other.gameObject.transform.parent=player.transform;
            other.gameObject.tag="Collected";
            other.transform.localPosition = new Vector3(0, -other.GetComponent<StackManager>().index, 0);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            if (!iscollected)
                return;
           
            StartCoroutine(playercontroller.LowerHeight());
            transform.parent=null;
            GetComponent<BoxCollider>().enabled=false;
            other.gameObject.GetComponent<BoxCollider>().enabled=false;
            
        }

        if (other.CompareTag("Ground"))
        {
            onGround = true;
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = 0;
            rb.velocity = newVelocity;
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = false;

        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Water"))
        {
            if (player.GetComponent<Playercontroller>().height > 0)
            {
                player.GetComponent<Playercontroller>().height--;
            }
            particle.transform.parent = null;
            particle.Play();
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            onGround = true;
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = 0;
            rb.velocity = newVelocity;


        }
    }

    IEnumerator DecreaseStack()
    {
        yield return new WaitForSeconds(0.1f);

        if (player.GetComponent<Playercontroller>().height > 0)
        {
            player.GetComponent<Playercontroller>().height--;
        }

       // player.GetComponent<Playercontroller>().SetHeight();
        Destroy(gameObject);
    }



}
