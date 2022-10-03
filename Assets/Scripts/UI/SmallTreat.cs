using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTreat : MonoBehaviour
{
    Treats jar;
    BoxCollider2D myCollider;
    Rigidbody2D myRigidbody;
    CurrencyTracker currencyTracker;
    bool canMove = false;
    float randx;
    float randy;
    [SerializeField]float sine = 1.1f;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        currencyTracker = FindObjectOfType<CurrencyTracker>();
        jar = FindObjectOfType<Treats>();
        randx = Random.Range(-100f, 100f);
        randy = Random.Range(-100f, 100f);
        StartCoroutine(WaittoMove());
    }
    IEnumerator WaittoMove()
    {
        yield return new WaitForSecondsRealtime(jar.ReturnWaitTime());
        canMove = true;}
    void Update()
    {
        if(!canMove){float step = 10 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x + randx, transform.position.y + randy), step);}
        if(canMove == true) {
        float step = jar.ReturnSpeed() * Time.deltaTime + sine;
        sine = Time.deltaTime + sine*(55/100);
        transform.position = Vector3.MoveTowards(transform.position, jar.ReturnTransform(), step);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0){
        jar.UpdateTreatsDestroyed();
        currencyTracker.UpdateCurrencyCount(1);
        Destroy(this.gameObject);}
    }
        }
    }
