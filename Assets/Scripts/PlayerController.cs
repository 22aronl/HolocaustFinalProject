using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float velocity = 5.0f;
    public float distance = 50.0f;
    float change = 0.0f;
    float startScale;
    public float endScale = 0.8f;
    int day = 1;
    public Text dayCounter;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + Mathf.Abs(Input.GetAxis("Horizontal"))*velocity*Time.deltaTime, transform.position.y);
        if(Input.GetAxis("Horizontal")!=0)
        {
            transform.localScale += new Vector3(change*velocity*Time.deltaTime, 0, 0);
        }
        float col = (transform.localScale.x-endScale)/(startScale-endScale);
        GetComponent<SpriteRenderer>().color = new Color(col, col, col, 1);
        Debug.Log(GetComponent<SpriteRenderer>().color);
    }

    public void newDay()
    {
        change = loss()/distance;
        dayCounter.text = "Day " + ++day;
    }

    private float loss()
    {
        return Random.Range(-0.3f, 0.08f);
    }
}
