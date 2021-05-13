using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{

    [SerializeField]
    float speed;

    [SerializeField]
    Transform startPoint, endPoint;

    [SerializeField]
    float changeDirectionDelay;


    private Transform destinationTarget, departTarget;

    private float startTime;

    private float journeyLength;

    bool isWaiting;



    void Start()
    {
        departTarget = startPoint;
        destinationTarget = endPoint;

        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }

    // robust to variable FPS
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isWaiting)
        {
            // if not close enough
            if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                // time synchronization required by 3D environment
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;
                // interpolate distance by fraction of journey
                transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
            }
            else
            {
                // start a waiting coroutine before changing movement direction
                isWaiting = true;
                StartCoroutine(changeDelay());
            }
        }


    }

    void ChangeDestination()
    {
        /* switch depart and destination to go back and forth between 
         * the two serialized points
         */
        if (departTarget == endPoint && destinationTarget == startPoint)
        {
            departTarget = startPoint;
            destinationTarget = endPoint;
        }
        else
        {
            departTarget = endPoint;
            destinationTarget = startPoint;
        }

    }
    IEnumerator changeDelay()
    {
        // wait a given amount of time
        yield return new WaitForSeconds(changeDirectionDelay);
        ChangeDestination();
        // reset movement
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }



    /*
     * makes the movement of the player relative to 
     * that of the platform
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;

        }
    }

    /*
     * undo the parenting relationship
     */
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

}