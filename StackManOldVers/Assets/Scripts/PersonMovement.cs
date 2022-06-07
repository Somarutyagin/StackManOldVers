using UnityEngine;

public class PersonMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _personBody;
    public float _speed = 0f;

    private void Update()
    {
        _personBody.velocity = new Vector3(_personBody.velocity.x, _personBody.velocity.y,_speed);
    }
}
