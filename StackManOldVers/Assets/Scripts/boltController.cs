using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private bool isPar = false;
    public Sprite blot1, blot2;
    private int RandomSprite = 0;

    public int _platformIndex;
    void Start()
    {
        RandomSprite = UnityEngine.Random.Range(0, 2);
        switch (RandomSprite)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = blot1;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = blot2;
                gameObject.transform.localScale = new Vector3(0.2f / 18, 0.2f / 18, 0.2f / 18);
                break;
        }
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (!isPar)
            _rb.AddForce(0, 0, 3);
        else
        {
            if (_platformIndex == 0) //circle
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -9.16f + 0.4f);
            else // spikeCircle, square, squareCircle
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -0.87f + 0.4f);
        }
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "detail" && !isPar)
        {
            gameObject.transform.SetParent(collision.gameObject.transform);
            isPar = true;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            _rb.isKinematic = false;
        }
    }
}
