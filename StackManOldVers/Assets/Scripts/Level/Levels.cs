using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Levels : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private TextMeshProUGUI _nextLevel;
    [SerializeField] private GeneratePlatforms _platforms;
    [SerializeField] private Transform _shtanga;
    [SerializeField] private Transform _shtanga1;
    [SerializeField] private adsTest advertisments;

    private float _shtangaScaleZ = 0.95f;
    private float _shtanga1ScaleZ = 33.38087f;

    private int showAdCount;

    public int LevelsCount = 0;
    public int _nextLevelCount = 0;

    private void Start()
    {

        //Advertisement.Initialize("4544328");
        LevelsCount = PlayerPrefs.GetInt("LEvels", LevelsCount);
        showAdCount = PlayerPrefs.GetInt("showAdCount", showAdCount);
        _shtangaScaleZ = PlayerPrefs.GetFloat("shtangaScaleZX", _shtangaScaleZ);
        _shtanga1ScaleZ = PlayerPrefs.GetFloat("shtanga1ScaleZX", _shtanga1ScaleZ);
        _nextLevelCount = LevelsCount + 1;
        //_shtanga.localScale = new Vector3(_shtanga.localScale.x, _shtanga.localScale.y, _shtangaScaleZ);
    }

    private void Update()
    {
        _shtanga.localScale = new Vector3(_shtanga.localScale.x, _shtanga.localScale.y, _shtangaScaleZ);
        _shtanga1.localScale = new Vector3(_shtanga1.localScale.x, _shtanga1.localScale.y, _shtanga1ScaleZ);
        PlayerPrefs.SetInt("LEvels", LevelsCount);

        _currentLevel.text = $"{LevelsCount}";
        _nextLevel.text = $"{_nextLevelCount}";
    }

    public void onClickNextLevel() 
    {
        showAdCount++;
        if (showAdCount >= 2)
        {
            //advertisments.ShowAdSimple();
            showAdCount = 0;
        }
        CamChangeColor.indexOfColor = Random.Range(0, CamChangeColor.colorsCount);
        PlayerPrefs.SetInt("IndexOFColor", CamChangeColor.indexOfColor);
        GeneratePlatforms._indexOfPlatform = Random.Range(0, GeneratePlatforms.paltformsCounter);
        PlayerPrefs.SetInt("indexOfPaltform", GeneratePlatforms._indexOfPlatform);
        PlayerPrefs.SetInt("showAdCount", showAdCount);

        if (LevelsCount <= 7)
        {
            _platforms._badCounts += 6f;
            _platforms._platformAmount += 0;
            _shtangaScaleZ += 0f;
        }
        else
        {
            _platforms._badCounts += 0f;
            _shtangaScaleZ += 0f;
            _platforms._platformAmount += 0;
        }
        PlayerPrefs.SetFloat("badCounts",_platforms._badCounts);
        PlayerPrefs.SetFloat("shtangaScaleZX", _shtangaScaleZ);
        LevelsCount++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onClickDouble() 
    {
        CamChangeColor.indexOfColor = Random.Range(0, CamChangeColor.colorsCount);
        GeneratePlatforms._indexOfPlatform = Random.Range(0, GeneratePlatforms.paltformsCounter);
        PlayerPrefs.SetInt("IndexOFColor", CamChangeColor.indexOfColor);
        PlayerPrefs.SetInt("indexOfPaltform", GeneratePlatforms._indexOfPlatform);
        //advertisments.ShowAd();
        Money.CoinsPerGame = Money.CoinsPerGame * 2;
        if (LevelsCount <= 7)
        {
            _platforms._badCounts += 6f;
            _platforms._platformAmount += 0;
            _shtangaScaleZ += 0f;
        }
        else
        {
            _platforms._badCounts += 0f;
            _shtangaScaleZ += 0f;
            _platforms._platformAmount += 0;
        }
        PlayerPrefs.SetFloat("badCounts",_platforms._badCounts);
        PlayerPrefs.SetFloat("shtangaScaleZX", _shtangaScaleZ);
        LevelsCount++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
    }
}
