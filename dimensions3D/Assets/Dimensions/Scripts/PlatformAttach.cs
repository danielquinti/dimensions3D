using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlatformAttach : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Collider>().transform.parent = transform;
            Debug.Log(other.GetComponent<Collider>().transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Collider>().transform.parent = null;
        }
    }
}
