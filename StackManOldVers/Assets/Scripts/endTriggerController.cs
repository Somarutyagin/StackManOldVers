using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class endTriggerController : MonoBehaviour
{
    public GameObject Finish;
    public int _platformIndex;

    void Start()
    {
        StartCoroutine(EndTriggerMover());
    }

    IEnumerator EndTriggerMover()
    {
        yield return new WaitForSeconds(3);
        if (_platformIndex == 2) // spikeCircle
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 21.06f);
        else if (_platformIndex == 0) // circle
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20.56f);
        else // spike cirlce, square
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20.06f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "detail")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
    }
}
