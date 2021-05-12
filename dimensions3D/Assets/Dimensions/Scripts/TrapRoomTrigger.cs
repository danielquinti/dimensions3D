using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomTrigger : MonoBehaviour
{
    virtual public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<TrapRoomManager>().ActivateTrap();
            Destroy(this);
        }
    }
}
