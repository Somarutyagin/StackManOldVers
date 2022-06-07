using UnityEngine;
using Newtonsoft;

public class GeneratePlatforms : MonoBehaviour
{
    [SerializeField] private GameObject[] _platform;

    [SerializeField] private float _angleStep;

    [SerializeField] public int _platformAmount;

    [SerializeField] private float _platformHeight;

    [SerializeField] private Material[] _goodMaterials;

    [SerializeField] private Material _badMaterial;

    [SerializeField] private Material _fireMaterial;

    [SerializeField] private Levels _levels;

    public static int _indexOfPlatform = 0;

    public float _badCounts = 10;

    private int _platformIndex = 0;

    public static int paltformsCounter = 0;

    private bool isFirstFireInst = false;

    [SerializeField] private boltController bolt;
    [SerializeField] private endTriggerController end;

    private int q;

    private void Start()
    {
        //PlayerPrefs.SetInt("LEvels", 1);
        PlayerPrefs.SetInt("platformCountX", 50);
        PlayerPrefs.SetFloat("shtangaScaleZX", 0.95f);
        PlayerPrefs.SetFloat("shtanga1ScaleZX", 33.38087f);
        if (PlayerPrefs.GetInt("LEvels") == 1)
        {
            PlayerPrefs.SetInt("badCounts", 3);
        }
        paltformsCounter = _platform.Length;
        _indexOfPlatform = PlayerPrefs.GetInt("indexOfPaltform", _indexOfPlatform);
        _platformAmount = PlayerPrefs.GetInt("platformCountX", _platformAmount);
        _badCounts = PlayerPrefs.GetFloat("badCounts", _badCounts);
        GeneratePlatforms3();
    }

    private void Update()
    {
        PlayerPrefs.SetInt("platformCountX", _platformAmount);
    }

    [ContextMenu("GeneratePlatforms")]
    public void GeneratePlatforms3()
    {
        if (_badCounts >= 100)
        {
            _badCounts = 100;
        }

        _platformIndex = Random.Range(0, _goodMaterials.Length);
        if (_platformIndex == 0)
            bolt._platformIndex = 0;
        else
            bolt._platformIndex = 1;

        for (int j = 0; j < _platform.Length; j++)
        {
            if (_indexOfPlatform == j)
            {
                for (int i = 0; i < _platformAmount; i++)
                {
                    var newObj = Instantiate(_platform[j], -transform.forward * -_platformHeight * i, Quaternion.Euler(0, 0, _angleStep * i));
                    if (i == 10 && PlayerPrefs.GetInt("LEvels") == 2)
                        isFirstFireInst = true;
                    else if (i == 5 && PlayerPrefs.GetInt("LEvels") == 4)
                        isFirstFireInst = true;
                    else if (i == 1 && PlayerPrefs.GetInt("LEvels") == 6)
                        isFirstFireInst = true;
                    if (j == 0)
                    {
                        bolt._platformIndex = 0;
                        end._platformIndex = 0;
                    }
                    else if (j == 2)
                    {
                        bolt._platformIndex = 2;
                        end._platformIndex = 2;
                    }
                    else
                    {
                        bolt._platformIndex = 100; //любое число
                    }
                    int childCount = newObj.transform.childCount;
                    for (int k = childCount - 1; k >= 0; k--)
                    {
                        if ((PlayerPrefs.GetInt("LEvels") == 2 || PlayerPrefs.GetInt("LEvels") == 4 || PlayerPrefs.GetInt("LEvels") == 6) && isFirstFireInst == true)
                        {
                            var child = newObj.transform.GetChild(k).gameObject;
                            child.AddComponent<Fire>();
                            child.GetComponent<Renderer>().material = _fireMaterial;
                        }
                        else
                        {
                            var child = newObj.transform.GetChild(k).gameObject;
                            child.AddComponent<Good>();
                            for (int v = 0; v < _goodMaterials.Length; v++)
                            {
                                if (_platformIndex == v)
                                {
                                    child.GetComponent<Renderer>().material = _goodMaterials[v];
                                }
                            }
                        }
                    }
                    isFirstFireInst = false;

                    if (Random.Range(0, _platformAmount) < _badCounts / 2)
                    {
                        int randChild = Random.Range(0, childCount);
                        int randChild2 = Random.Range(0, childCount);
                        int randChild3 = Random.Range(0, childCount);

                        if (randChild2 == randChild || randChild2 == randChild3)
                        {
                            randChild2 = Random.Range(0, childCount);
                        }
                        else if (randChild3 == randChild || randChild3 == randChild2)
                        {
                            randChild3 = Random.Range(0, childCount);
                        }
                        else if (randChild == randChild3 || randChild == randChild2)
                        {
                            randChild = Random.Range(0, childCount);
                        }
                        q = bolt._platformIndex;
                        if (q == 0 || q == 2)
                            q = 3;
                        else
                            q = 1;

                        for (int h = childCount - q; h >= 0; h--)
                        {
                            if (h == randChild)
                            {
                                continue;
                            }
                            if (h == randChild2)
                            {
                                continue;
                            }
                            if (h == randChild3)
                            {
                                continue;
                            }
                            var child = newObj.transform.GetChild(h).gameObject;
                            if (!child.GetComponent<Fire>())
                            {
                                child.AddComponent<Bad>();
                                Destroy(child.GetComponent<Good>());
                                child.GetComponent<Renderer>().material = _badMaterial;
                            }
                        }
                    }
                }
            }
        }
    }
}
