using UnityEngine;
using UnityEngine.UI;

public class LevelTrecker : MonoBehaviour
{
    [SerializeField] private Slider _levelProgressDisplay;

    private Platforms[] _platformsCount;

    private void Start()
    {
        _platformsCount = GameObject.FindObjectsOfType<Platforms>();

        _levelProgressDisplay.value = 0;

        Debug.Log(_platformsCount.Length);
        _levelProgressDisplay.maxValue = _platformsCount.Length;
    }

    private void Update()
    {
        _levelProgressDisplay.value = PlayerMovement.LevelTrackerCounter;
    }
}
