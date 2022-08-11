using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Playercontroller : MonoBehaviour
{
    public GameObject[] scores;
    public Animator anim;
    public float speedZ;
    public float speedX;
    public float height;
    bool isGameover;
    
    
    
    private void Start() {
      
    }
   

    // Update is called once per frame
    void Update()
    {
        if (isGameover)
            return;

        Move();
    }

    private void Move()
    {
        float horizantal = Input.GetAxis("Horizontal") * speedX * Time.deltaTime;
        transform.Translate(horizantal, 0, speedZ * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    public IEnumerator LowerHeight()
    {
        yield return new WaitForSeconds(15*Time.deltaTime);
        height--;
    }
    
     private void OnTriggerEnter(Collider other) 
    {
        
        if (other.gameObject.CompareTag("Stack"))
        {
            StartCoroutine(Score(0));
            height++;
            other.gameObject.GetComponent<StackManager>().iscollected=true;
            other.gameObject.GetComponent<StackManager>().Setindex( height);
            other.gameObject.transform.parent=this.transform;
            other.gameObject.tag="Collected";
            
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
           isGameover=true;
           anim.SetTrigger("Die");
        }
    }

    public IEnumerator Score(int number)
    {
        
        scores[number].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        scores[number].SetActive(false);
        
        
       
         

    }

}
