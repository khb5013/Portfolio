using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] float moveSpeed;
    [SerializeField] float waitTime;

    List<Collider2D> onTargetList;

    Vector3 pointA;
    Vector3 pointB;
    bool isReverse;
    float delay;

    void Start()
    {
        onTargetList = new List<Collider2D>();
        pointA = transform.position;
        pointB = pointA + offset;
    }
    void Update()
    {
        if ((delay -= Time.deltaTime) > 0.0f)
            return;

        Movement();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onTargetList.Add(collision.collider);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onTargetList.Remove(collision.collider);
    }

    private void Movement()
    {
        // flag값을 이용해 목적지를 결정한다.
        Vector3 prevPosition = transform.position;
        Vector3 destination = isReverse ? pointB : pointA;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        Vector3 movement = transform.position - prevPosition;
        foreach (Collider2D onTarget in onTargetList)
            onTarget.transform.position += movement;

        if (transform.position == destination)
        {
            isReverse = !isReverse;
            delay = waitTime;
        }
    }
}
