using UnityEngine;

public class PlatformsDestruction : MonoBehaviour
{
    [SerializeField] private GameObject[] _childUnitsUp;
    [SerializeField] private GameObject[] _childUnitsDown;
    [SerializeField] private GameObject[] _childUnitsRight;
    [SerializeField] private GameObject[] _childUnitsLeft;


    private float _jumpStrenght = 30;

    private void Start()
    {
            for (int i = 0; i < _childUnitsUp.Length; i++)
            {
                Rigidbody _cjilds = _childUnitsUp[i].transform.gameObject.AddComponent<Rigidbody>();
                _cjilds.AddForce(_childUnitsUp[i].transform.up * _jumpStrenght, ForceMode.Impulse);
            }
            for (int i = 0; i < _childUnitsDown.Length; i++)
            {
                Rigidbody _cjilds = _childUnitsDown[i].transform.gameObject.AddComponent<Rigidbody>();
                _cjilds.AddForce(-_childUnitsDown[i].transform.up * _jumpStrenght, ForceMode.Impulse);
            }
            for (int i = 0; i < _childUnitsRight.Length; i++)
            {
                Rigidbody _cjilds = _childUnitsRight[i].transform.gameObject.AddComponent<Rigidbody>();
                _cjilds.AddForce(_childUnitsRight[i].transform.right * _jumpStrenght, ForceMode.Impulse);
            }
            for (int i = 0; i < _childUnitsLeft.Length; i++)
            {
                Rigidbody _cjilds = _childUnitsLeft[i].transform.gameObject.AddComponent<Rigidbody>();
                _cjilds.AddForce(-_childUnitsLeft[i].transform.right * _jumpStrenght, ForceMode.Impulse);
            }
    }
}
