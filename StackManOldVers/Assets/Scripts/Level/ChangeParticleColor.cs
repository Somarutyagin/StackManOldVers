using UnityEngine;

public class ChangeParticleColor : MonoBehaviour
{
    [SerializeField] private Material[] _colors;
    [SerializeField] private ParticleSystemRenderer _partRenderer;

    private int _indexOfColor;

    private void Start()
    {
        _indexOfColor = Random.Range(0, _colors.Length);

        for (int i = 0; i < _colors.Length; i++) {
            if (_indexOfColor == i)
            {
                _partRenderer.material = _colors[i];
            }
        }
    }

}
