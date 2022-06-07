using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Advertisements;
//using UnityEngine.Purchasing;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weapons;
    [SerializeField] private TextMeshProUGUI _dancing;
    [SerializeField] private TextMeshProUGUI _priceOfThings;

    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject[] _weaponsInGameR;
    [SerializeField] private GameObject[] _weaponsInGameL;

    [SerializeField] private Image _weaponIcon;

    [SerializeField] private Sprite[] _weaponsSprites;

    [SerializeField] private Animator camAnim;

    [SerializeField] private CamMovement cam;

    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject[] _weaponsOnPersonR;
    [SerializeField] private GameObject[] _weaponsOnPersonL;
    [SerializeField] private GameObject weaponBar;
    [SerializeField] private GameObject dancingBar;
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private GameObject selectBtn;
    [SerializeField] private GameObject selectedBtn;
    [SerializeField] private GameObject coinsShop;

    [SerializeField] private Sprite[] _weaponsImage;

    [SerializeField] private Animator _personAnim;

    public int[] weaponsBuyed = {0, 0, 0, 0, 0, 0};

    public int[] dancesBuyed = { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,0,0};

    private int showWeapon = 0;

    public int personDancesIndex = 3;

    private bool dances = false;
    private bool weapons = false;

    private string gameID = "4544328";
    private bool testMode = false;

    private int[] priceOfWeapons = 
    {
        500,
        1500,
        3000,
        4500,
        6000,
        7500
    };

    private int[] priceOfDance =
    {
        0,
        0,
        0,
        100,
        300,
        500,
        600,
        800,
        1000,
        1200,
        1500,
        2000,
        5000,
        10000
    };

    private string[] _labelOfWeapons = {"simple boxing", "spike boxing", "simple casetets", "spike castets", "simple bat", "spike bat"};

    public static int selectedDance;
    public static int selectedWeapon;

    private void Start()
    {
        
        //Advertisement.Initialize(gameID, testMode);
        selectedWeapon = PlayerPrefs.GetInt("selectedWeapon", selectedWeapon);
        selectedDance = PlayerPrefs.GetInt("selectedDance", selectedDance);

        camAnim.enabled = false;

        for (int i = 0; i < weaponsBuyed.Length; i++)
        {
            weaponsBuyed[i] = PlayerPrefs.GetInt($"{i}weapons", weaponsBuyed[i]);
        }

        for (int i = 0; i < dancesBuyed.Length; i++)
        {
            dancesBuyed[i] = PlayerPrefs.GetInt($"{i}dances", dancesBuyed[i]);
        }

        for (int i = 0; i < _weaponsOnPersonR.Length; i++)
        {
            if (selectedWeapon == i)
            {
                _weaponsOnPersonR[i].SetActive(true);
            }
            else
            {
                _weaponsOnPersonR[i].SetActive(false);
            }
        }

        for (int i = 0; i < _weaponsOnPersonL.Length; i++)
        {
            if (selectedWeapon == i)
            {
                _weaponsOnPersonL[i].SetActive(true);
            }
            else
            {
                _weaponsOnPersonL[i].SetActive(false);
            }
        }

        for (int i = 0; i < _weaponsInGameR.Length; i++)
        {
            if (selectedWeapon == i)
            {
                _weaponsInGameR[i].SetActive(true);
            }
            else
            {
                _weaponsInGameR[i].SetActive(false);
            }
        }

        for (int i = 0; i < _weaponsInGameL.Length; i++)
        {
            if (selectedWeapon == i)
            {
                _weaponsInGameL[i].SetActive(true);
            }
            else
            {
                _weaponsInGameL[i].SetActive(false);
            }
        }
    }

    public void Buy()
    {
        if (weapons)
        {
            for (int i = 0; i < weaponsBuyed.Length; i++)
            {
                if (showWeapon == i)
                {
                    if (Money.CoinsMain > priceOfWeapons[i])
                    {
                        Money.CoinsMain -= priceOfWeapons[i];
                        weaponsBuyed[i] = 1;
                        PlayerPrefs.SetInt($"{i}weapons", weaponsBuyed[i]);
                        if (weaponsBuyed[i] == 1)
                        {
                            selectBtn.SetActive(true);
                            buyBtn.SetActive(false);
                            _lock.SetActive(false);
                        }
                        else
                        {
                            selectBtn.SetActive(false);
                            _lock.SetActive(true);
                            buyBtn.SetActive(true);
                        }
                    }
                    else
                    {
                        Handheld.Vibrate();
                    }
                }

            }
        }
        if (dances)
        {
            for (int i = 0; i < dancesBuyed.Length; i++)
            {
                if (personDancesIndex == i)
                {
                    if (Money.CoinsMain > priceOfDance[i])
                    {
                        Money.CoinsMain -= priceOfDance[i];
                        dancesBuyed[i] = 1;
                        PlayerPrefs.SetInt($"{i}dances", dancesBuyed[i]);
                        if (dancesBuyed[i] == 1)
                        {
                            selectBtn.SetActive(true);
                            buyBtn.SetActive(false);
                            _lock.SetActive(false);
                        }
                        else
                        {
                            _lock.SetActive(true);
                            selectBtn.SetActive(false);
                            buyBtn.SetActive(true);
                        }
                    }
                    else
                    {
                        Handheld.Vibrate();
                    }
                }
            }
        }
    }

    public void Select()
    {
        if (weapons)
        {
            for (int i = 0; i < _weaponsInGameR.Length; i++)
            {
                if (showWeapon == i)
                {
                    selectedWeapon = i;
                    _weaponsInGameR[i].SetActive(true);
                    selectedBtn.SetActive(true);
                }
                else
                {
                    _weaponsInGameR[i].SetActive(false);
                }
            }

            for (int i = 0; i < _weaponsInGameL.Length; i++)
            {
                if (showWeapon == i)
                {
                    _weaponsInGameL[i].SetActive(true);
                }
                else
                {
                    _weaponsInGameL[i].SetActive(false);
                }
            }

            PlayerPrefs.SetInt("selectedWeapon", selectedWeapon);
        }

        if (dances)
        {
            for (int i = 0; i < dancesBuyed.Length; i++)
            {
                if (personDancesIndex == i)
                {
                    selectedDance = i;
                    selectedBtn.SetActive(true);
                }

            }
            PlayerPrefs.SetInt("selectedDance", selectedDance);
        }
    }

    public void onClickSelect(bool weapon) 
    {
        switch (weapon)
        {
            case true:
                weapons = true;
                weaponBar.SetActive(true);
                dancingBar.SetActive(false);
                dances = false;
                break;
            case false:
                weapons = false;
                weaponBar.SetActive(false);
                dancingBar.SetActive(true);
                dances = true;
                break;
        }
    }

    public void OnCLickOpenCoinShop() 
    {
        coinsShop.SetActive(true);
    }

    public void OnClickCloseCoinShop() 
    {
        coinsShop.SetActive(false);
    }

    public void OnClickToWatchFreeAd() 
    {
        //Advertisement.Show("Rewarded");
    }

    /*
    public void onBuyComplete(Product product) 
    {
        switch (product.definition.id)
        {
            case "coinadd300":
                Money.CoinsMain += 300;
                PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
                break;
            case "coinadd450":
                Money.CoinsMain += 450;
                PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
                break;
            case "coinadd500":
                Money.CoinsMain += 500;
                PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
                break;
            case "coinadd1500":
                Money.CoinsMain += 1500;
                PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
                break;
            case "coinadd10000":
                Money.CoinsMain += 10000;
                PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
                break;
        }
    }
    */
    public void onClickOpenShop() 
    {
        camAnim.enabled = true;
        _game.SetActive(false);
        cam.enabled = false;
        camAnim.SetInteger("shop",1);
        Invoke(nameof(open), 1);
    }

    private void open() 
    {
        _shop.SetActive(true);
        cam.enabled = false;
    }

    public void onClickCloseShop() 
    {
        camAnim.SetInteger("shop", 2);
        _shop.SetActive(false);
        Invoke(nameof(close),1);
    }

    private void close() 
    {
        camAnim.enabled = false;
        cam.enabled = true;
        _game.SetActive(true);
    }

    public void OnClickNextObject() 
    {
        if (dances)
        {
            personDancesIndex++;

            if (personDancesIndex >= 11)
            {
                personDancesIndex = 11;
            }

            _personAnim.SetInteger("PersonStates", personDancesIndex);

            _dancing.text = $"{personDancesIndex} dance";
        }
        
        if (weapons)
        {
            showWeapon++;

            if (showWeapon >= 5)
            {
                showWeapon = 5;
            }

            _personAnim.SetInteger("PersonStates", 0);

            for (int i = 0; i < _weaponsOnPersonR.Length; i++)
            {
                if (showWeapon == i)
                {
                    _weaponIcon.sprite = _weaponsImage[i];
                    _weapons.text = $"{_labelOfWeapons[i]}";
                    _weaponsOnPersonR[i].SetActive(true);
                }
                else
                {
                    _weaponsOnPersonR[i].SetActive(false);
                }
            }

            for (int i = 0; i < _weaponsOnPersonL.Length; i++)
            {
                if (showWeapon == i)
                {
                    _weaponsOnPersonL[i].SetActive(true);
                }
                else
                {
                    _weaponsOnPersonL[i].SetActive(false);
                }
            }
        }
    }

    public void OnClickPerviosObject()
    {
        if (dances)
        {
            personDancesIndex--;

            if (personDancesIndex <= 3)
            {
                personDancesIndex = 3;
            }

            _personAnim.SetInteger("PersonStates", personDancesIndex);

            _dancing.text = $"{personDancesIndex} dance";
        }

        if (weapons)
        {
            showWeapon--;
            if (showWeapon <= 0)
            {
                showWeapon = 0;
            }
            _personAnim.SetInteger("PersonStates", 0);

            for (int i = 0; i < _weaponsOnPersonR.Length; i++)
            {
                if (showWeapon == i)
                {
                    _weaponIcon.sprite = _weaponsImage[i];
                    _weapons.text = $"{_labelOfWeapons[i]}";
                    _weaponsOnPersonR[i].SetActive(true);

                }
                else
                {
                    _weaponsOnPersonR[i].SetActive(false);
                    
                }
            }

            for (int i = 0; i < _weaponsOnPersonL.Length; i++)
            {
                if (showWeapon == i)
                {
                    _weaponsOnPersonL[i].SetActive(true);

                }
                else
                {
                    _weaponsOnPersonL[i].SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (dances)
        {
            for (int i = 0; i < dancesBuyed.Length; i++)
            {
                if (personDancesIndex == i)
                {
                    if (dancesBuyed[i] == 1)
                    {
                        _lock.SetActive(false);
                        selectBtn.SetActive(true);
                        buyBtn.SetActive(false);
                        if (selectedDance == i)
                        {
                            selectedBtn.SetActive(true);
                            _lock.SetActive(false);
                        }
                        else
                        {
                            selectedBtn.SetActive(false);
                        }
                    }
                    else
                    {
                        _priceOfThings.text = $"{priceOfDance[i]}";
                        _lock.SetActive(true);
                        selectedBtn.SetActive(false);
                        selectBtn.SetActive(false);
                        _lock.SetActive(true);
                        buyBtn.SetActive(true);
                    }
                }
            }
        }

        if (weapons)
        {
            for (int i = 0; i < weaponsBuyed.Length; i++)
            {
                if (showWeapon == i)
                {
                    if (weaponsBuyed[i] == 1)
                    {
                        _lock.SetActive(false);
                        selectBtn.SetActive(true);
                        buyBtn.SetActive(false);
                        if (selectedWeapon == i)
                        {
                            selectedBtn.SetActive(true);
                            _lock.SetActive(false);
                        }
                        else
                        {
                            selectedBtn.SetActive(false);
                        }
                    }
                    else
                    {
                        _priceOfThings.text = $"{priceOfWeapons[i]}";
                        _lock.SetActive(true);
                        selectedBtn.SetActive(false);
                        selectBtn.SetActive(false);
                        _lock.SetActive(true);
                        buyBtn.SetActive(true);
                    }
                }
            }
        }
    }
}
