using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Playercontroller : MonoBehaviour
{
    public GameObject[] scores;
    public Animator anim;
    
    public  float height;
    bool isGameover;

    float step;
    Vector2 actionPosition;

    public bool isRun, isFight;
    public float speed, swipeSpeed;


   

    // Update is called once per frame
    void Update()
    {
        if (isGameover)
            return;

        Move();

        
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            actionPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            isRun = true;
           
        }

        if (isRun)
            
            
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);

        if (Input.GetMouseButton(0))
        {
           

            step = (Input.mousePosition.x - actionPosition.x);

            transform.position += new Vector3(step * swipeSpeed, 0, 0) * Time.deltaTime;


            actionPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

             Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -3.4f, 3.4f);
            transform.position = pos;
        }

       
    }

    public IEnumerator LowerHeight()
    {
        yield return new WaitForSeconds(0.3f);
        height--;
    }
    
     private void OnTriggerEnter(Collider other) 
    {
        
        if (other.gameObject.CompareTag("Stack"))
        {
            StartCoroutine(Score(0));
            height++;
            SetHeight();
            anim.SetTrigger("Jump");
            other.gameObject.GetComponent<StackManager>().iscollected=true;
            other.gameObject.GetComponent<StackManager>().Setindex( height);
            other.gameObject.transform.parent=this.transform;
            other.gameObject.tag="Collected";
            other.transform.localPosition = new Vector3(0, -other.GetComponent<StackManager>().index, 0);

        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
           isGameover=true;
           anim.SetTrigger("Die");
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            if (GetComponent<Playercontroller>().height == 0)
            {
                anim.SetTrigger("Die");
                this.enabled = false;
            }
        }
    }

    public IEnumerator Score(int number)
    {
        
        scores[number].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        scores[number].SetActive(false);
    }

    public void SetHeight()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

}
