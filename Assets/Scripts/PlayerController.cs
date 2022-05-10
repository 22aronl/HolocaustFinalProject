using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float velocity = 6.5f;
    public float distance = 50.0f;
    float change = 0.0f;
    float startScale;
    public float endScale = 0.8f;
    int day = 1;
    int deathDay;
    Vector2 startLoc;
    public Text dayCounter;
    public Text selectionCounter;
    public Image black;
    bool pause = false;
    bool compassion = false;
    int compassionDay = 0;
    float compassionGain = 0.0f;
    bool bribe = false;
    int bribeDay = 0;
    float bribeGain = 0.0f;
    bool heart = false;
    // Start is called before the first frame update
    void Start()
    {
        deathDay = -1;
        startLoc = transform.position;
        startScale = transform.localScale.x;
        deathDay = Random.Range(3, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
            transform.position = new Vector2(transform.position.x + Mathf.Abs(Input.GetAxis("Horizontal"))*velocity*Time.deltaTime, transform.position.y);
            if(Input.GetAxis("Horizontal")!=0)
            {
                transform.localScale += new Vector3(change*velocity*Time.deltaTime, 0, 0);
            }
            float col = (transform.localScale.x-endScale)/(startScale-endScale);
            GetComponent<SpriteRenderer>().color = new Color(col, col, col, 1);

            if(deathDay == day)
            {
                selectionCounter.text = "Selektion";
            }
            else
            {
                selectionCounter.text = "";
            }

            if(!heart && Input.GetKeyDown(KeyCode.Alpha1))
            {
                heart = true;
            }
            if(!bribe && Input.GetKeyDown(KeyCode.Alpha2))
            {
                bribe = true;
            }
            if(!compassion && Input.GetKeyDown(KeyCode.Alpha3))
            {
                compassion = true;
            }


            if(deathDay == day && transform.position.x >= 0 &&transform.localScale.x < 1.3f)
            {
                die();
            }


            if(transform.localScale.x <= 0.8f)
                die();

            //Debug.Log(GetComponent<SpriteRenderer>().color);
        }
    }

    public void newDay()
    {
        change = loss()/distance;
        Debug.Log("BRING DAU" + heart + bribe + compassion);
        if(heart)
            heart1();
        if(bribe)
            bribe1();
        if(compassion)
            compassion1();
        //select random number
        Debug.Log("BRING DAU" + heart + bribe + compassion);
        Debug.Log("BRING DAU" + bribeDay + compassionDay);
        if(compassionDay != 0)
        {
            transform.localScale += new Vector3(compassionGain--, 0, 0);
            Debug.Log("I DID SOMETHING");
        }
        if(bribeDay != 0)
        {
            transform.localScale += new Vector3(bribeGain--, 0, 0);
        }


        if(deathDay < day)
        {
            deathDay = Random.Range(2, 6) + day;
        }
        if(!pause)
            dayCounter.text = "Day " + ++day;
    }

    public IEnumerator fade(float time)
    {
        pause = true;
        float t = 0;
        while(t < time)
        {
            t += Time.deltaTime;
            //Debug.Log(t);
            black.color = new Color(0, 0, 0, t/time);
            yield return null;
        }
        
    }

    public IEnumerator finish()
    {
        yield return new WaitForSeconds(6);
        pause = false;
        black.color = new Color(0, 0, 0, 0);
        transform.localScale = new Vector3(startScale, transform.localScale.y, transform.localScale.z);
        transform.position = startLoc;
        newDay();
    }

    private void die()
    {
        day = 0;
        deathDay = -1;
        

        StartCoroutine(fade(5));
        StartCoroutine(finish());
        
    }

    private void heart1()
    {  
        Debug.Log(transform.localScale);
        transform.localScale += new Vector3(0.04f, 0, 0);
        heart = false;
        Debug.Log("HEART" + transform.localScale);
    }

    private void compassion1()
    {
        if(compassionDay == 0)
        {
            transform.localScale -= new Vector3(0.04f, 0, 0);
        
            compassionDay = Random.Range(1, 3);
            compassionGain = Random.Range(0.02f, 0.06f);
        }
        compassion = false;
    }

    private void bribe1()
    {
        if(bribeDay == 0)
        {
            transform.localScale -= new Vector3(0.2f, 0, 0);
        
            bribeDay = Random.Range(2, 5);
            bribeGain = Random.Range(0.03f, 0.07f);
        }
        bribe = false;
    }
 
    private float loss()
    {
        return Random.Range(-0.3f, 0.08f);
    }
}
