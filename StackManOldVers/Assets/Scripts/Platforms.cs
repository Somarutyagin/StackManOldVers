using UnityEngine;

public class Platforms : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(transform.forward * 100 * Time.deltaTime);
    }
}
