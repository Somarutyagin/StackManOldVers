using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    public static int CoinsMain = 0;
    public static float CoinsPerGame = 0;

    [SerializeField] private TextMeshProUGUI _coinsContDisplay;
    [SerializeField] private TextMeshProUGUI _coinsCountForGame;

    private void Start()
    {
        CoinsMain = PlayerPrefs.GetInt("CoinsMain",CoinsMain);
    }

    private void Update()
    {
        _coinsContDisplay.text = $"{CoinsMain}";
        _coinsCountForGame.text = $"{(int)CoinsPerGame}";
    }
}
