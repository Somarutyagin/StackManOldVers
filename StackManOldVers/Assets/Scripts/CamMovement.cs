using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smooth = 5f;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _smooth);
    }

}
