using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public bool iscollected;
    public GameObject player;
    public Playercontroller playercontroller;
    public float index;
    int number;
    

    
    private void Update() {

        if (transform.parent!=null&&iscollected)
        {
            transform.localPosition=new Vector3(0,-index,0);
        }
        
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
            
            StartCoroutine(playercontroller.Score(number));
            player.GetComponent<Playercontroller>().height++;           
            other.gameObject.GetComponent<StackManager>().iscollected=true;
            other.gameObject.GetComponent<StackManager>().Setindex( player.GetComponent<Playercontroller>().height);
            other.gameObject.transform.parent=player.transform;
            other.gameObject.tag="Collected";
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
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Water"))
        {
            StartCoroutine(DecreaseStack());
        }
    }

    IEnumerator DecreaseStack()
    {
        yield return new WaitForSeconds(8*Time.deltaTime);
        player.GetComponent<Playercontroller>().height--;
        Destroy(gameObject);
    }



}
