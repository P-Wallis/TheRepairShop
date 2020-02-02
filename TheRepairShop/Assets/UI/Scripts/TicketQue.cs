using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketQue : MonoBehaviour
{

    Vector3 currentPos;
    Vector3 targetPos;
    float windowWidth;

    void Start()
    {
        windowWidth = (Screen.width / 6);
        currentPos = this.transform.position;
        targetPos = currentPos;
        SlideTicket();
    }

    public void SlideTicket()
    {
        
        currentPos = transform.position;
        targetPos.x = currentPos.x - windowWidth;
        StartCoroutine(TicketSlider());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SlideTicket();
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator TicketSlider()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPos, targetPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        GetComponent<Collider2D>().enabled = true;
    }
}
