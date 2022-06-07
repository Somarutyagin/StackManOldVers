using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MoreMountains.NiceVibrations;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PersonMovement personMove;
    [SerializeField] private GameObject _person;
    [SerializeField] private Animator _personAnim;
    [SerializeField] private TrailRenderer[] _lines;
    [SerializeField] private GameObject _wonPage;
    [SerializeField] private GameObject tapToRunLabel;
    [SerializeField] private GameObject _game;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _wonSound;
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private GameObject _shopVBtn;
    [SerializeField] private Shop sh;
    [SerializeField] private Slider _combo;
    [SerializeField] private GameObject _comboObk;
    [SerializeField] private GameObject leftFire;
    [SerializeField] private GameObject rightFire;
    [SerializeField] private GameObject headFire;
    [SerializeField] private TextMeshProUGUI comboCountDisplay;
    
    private bool _clicked = false;
    private bool _death = false;
    [SerializeField] private bool _triggered = false;
    [SerializeField] private bool _stoped = false;
    private bool _comboWork = false;
    private int _randomSound = 0;

    private float _comboValue = 0f;

    public static float LevelTrackerCounter = 0;

    private Platforms[] _platforms;

    private bool finished = false;

    public static int finishedBg = 0;

    public GameObject bolt;
    public GameObject deathParticle;
    public GameObject hat;

    private void OnOff() 
    {
        if (Music.offOn == 0)
        {

        }
        else
        {
            MMVibrationManager.Vibrate();
        }
    }

    private void Start()
    {
        hat.SetActive(true);
        _combo.maxValue = Random.Range(5,8);

        LevelTrackerCounter = 0;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        _platforms = GameObject.FindObjectsOfType<Platforms>();
    }

    private void Update()
    {
        deathParticle.transform.Rotate(transform.forward * 100 * Time.deltaTime);
        comboCountDisplay.text = $"COMBO:{(int)_comboValue}";
        if (_comboWork)
        {
            leftFire.SetActive(true);
            rightFire.SetActive(true);
            headFire.SetActive(true);
            if (transform.localScale.x <= 20)
                transform.localScale = new Vector3(transform.localScale.x + 20 * Time.deltaTime, transform.localScale.y + 20 * Time.deltaTime, transform.localScale.z + 20 * Time.deltaTime);
            else
                transform.localScale = new Vector3(20, 20, 20);
        }
        else
        {
            leftFire.SetActive(false);
            rightFire.SetActive(false);
            headFire.SetActive(false);
            if (transform.localScale.x >= 9.811419)
                transform.localScale = new Vector3(transform.localScale.x - 20 * Time.deltaTime, transform.localScale.y - 20 * Time.deltaTime, transform.localScale.z - 20 * Time.deltaTime);
            else
                transform.localScale = new Vector3(9.811419f, 9.811419f, 9.811419f);
        }

        if (_comboValue >= _combo.maxValue)
        {
            _comboWork = true;
        }


        if (_combo.value > 0)
        {
            _comboValue -= 2.5f * Time.deltaTime;
            _combo.value -= 2.5f * Time.deltaTime;
            _comboObk.SetActive(true);
        }
        else
        {
            _comboObk.SetActive(false);
        }

        if (_combo.value <= 0)
        {
            _combo.value = 0;
            _comboWork = false;
        }

        if (_clicked)
        {
            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].enabled = false;
            }
        }
    }
    public void BoltSpawn()
    {
        Instantiate(bolt, bolt.transform.position + gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    public void onDownClick() 
    {
        if (!finished && !_death)
        {
            _shopVBtn.SetActive(false);
            tapToRunLabel.SetActive(false);
            _personAnim.SetInteger("PersonStates", 1);
            personMove._speed = 6f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _clicked = true;
            Debug.Log(_clicked);
        }

        _clicked = true;
    }

    public void onUpClick() 
    {
        if (!finished && !_triggered && !_death)
        {
            _personAnim.SetInteger("PersonStates", 0);
            personMove._speed = 0f;
            Debug.Log(_clicked);
        }
        _clicked = false;
        _stoped = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Bad>(out Bad Bad) || collision.gameObject.GetComponent<Good>())
        {
            _triggered = false;
        }

    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Bad>(out Bad Bad) || collision.gameObject.GetComponent<Good>() && !_death)
        {
            _triggered = true;
            if (_stoped == true)
            {
                OnHitWalls();
            }
        }
    }

    private void OnHitWalls()
    {
        if (!_death)
        {
            _stoped = false;
            _personAnim.SetInteger("PersonStates", 12);
            if (!_clicked)
                personMove._speed = 0f;
            Debug.Log("hit");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Good>() && !_death)
        {
            _triggered = true;
            if (_clicked)
            {
                _clickSound.pitch += 0.04f;
                if (_comboWork)
                {
                    _randomSound = Random.Range(0, 3);
                    switch (_randomSound)
                    {
                        case 0:
                            //
                            break;
                        case 1:
                            //
                            break;
                        case 2:
                            //
                            break;
                    }
                    _clickSound.Play();
                    collision.transform.localScale = new Vector3(collision.transform.localScale.x + 1.5f * Time.deltaTime, collision.transform.localScale.y + 1.5f * Time.deltaTime, collision.transform.localScale.z + 1.5f * Time.deltaTime);
                    Money.CoinsPerGame += Random.Range(0.7f, 1.2f);
                    _personAnim.SetInteger("PersonStates", 2);
                    LevelTrackerCounter = LevelTrackerCounter + 1;
                    Debug.Log(LevelTrackerCounter);
                    OnOff();
                    collision.transform.parent.GetComponent<PlatformsDestruction>().enabled = true;
                    collision.transform.parent.GetComponent<Platforms>().enabled = false;
                    Destroy(collision.gameObject.GetComponent<MeshCollider>());
                    Destroy(collision.transform.parent.gameObject, 1);
                }
                else
                {
                    _randomSound = Random.Range(0, 3);
                    switch (_randomSound)
                    {
                        case 0:
                            //
                            break;
                        case 1:
                            //
                            break;
                        case 2:
                            //
                            break;
                    }
                    _clickSound.Play();
                    _comboValue += 1;
                    OnOff();
                    _combo.value = _comboValue;
                    Money.CoinsPerGame += Random.Range(0.3f, 0.5f);
                    _personAnim.SetInteger("PersonStates", 2);
                    LevelTrackerCounter = LevelTrackerCounter + 1;
                    Debug.Log(LevelTrackerCounter);
                    collision.transform.parent.GetComponent<PlatformsDestruction>().enabled = true;
                    collision.transform.parent.GetComponent<Platforms>().enabled = false;
                    Destroy(collision.gameObject.GetComponent<MeshCollider>());
                    Destroy(collision.transform.parent.gameObject, 1);
                }
            }
        }
        else if (collision.gameObject.GetComponent<Fire>() && !_death)
        {
            _comboValue = _combo.maxValue + 1;
            _combo.value = _comboValue;
            _clickSound.pitch += 0.04f;
            _randomSound = Random.Range(0, 3);
            switch (_randomSound)
            {
                case 0:
                    //
                    break;
                case 1:
                    //
                    break;
                case 2:
                    //
                    break;
            }
            _clickSound.Play();
            OnOff();
            Money.CoinsPerGame += Random.Range(0.1f, 0.3f);
            _personAnim.SetInteger("PersonStates", 2);
            LevelTrackerCounter = LevelTrackerCounter + 1;
            Debug.Log(LevelTrackerCounter);
            collision.transform.parent.GetComponent<PlatformsDestruction>().enabled = true;
            collision.transform.parent.GetComponent<Platforms>().enabled = false;
            Destroy(collision.gameObject.GetComponent<MeshCollider>());
            Destroy(collision.transform.parent.gameObject, 1);
        }
        else if (collision.gameObject.TryGetComponent<Bad>(out Bad Bad) && _clicked)
        {
            _triggered = true;
            if (_comboWork && !_death)
            {
                _clickSound.pitch += 0.04f;
                _randomSound = Random.Range(0, 3);
                switch (_randomSound)
                {
                    case 0:
                        //
                        break;
                    case 1:
                        //
                        break;
                    case 2:
                        //
                        break;
                }
                _clickSound.Play();
                OnOff();
                Money.CoinsPerGame += Random.Range(0.1f, 0.3f);
                _personAnim.SetInteger("PersonStates", 2);
                LevelTrackerCounter = LevelTrackerCounter + 1;
                Debug.Log(LevelTrackerCounter);
                collision.transform.parent.GetComponent<PlatformsDestruction>().enabled = true;
                collision.transform.parent.GetComponent<Platforms>().enabled = false;
                Destroy(collision.gameObject.GetComponent<MeshCollider>());
                Destroy(collision.transform.parent.gameObject, 1);
            }
            else
            {
                _death = true;
                _deathSound.Play();
                personMove._speed = 0f;
                _personAnim.SetInteger("PersonStates", 13);
                StartCoroutine(death());
                hat.SetActive(false);
                //collision.transform.parent.GetComponent<PlatformsDestruction>().enabled = true;
                //collision.transform.parent.GetComponent<Platforms>().enabled = false;
                //Destroy(collision.gameObject.GetComponent<MeshCollider>());
                //Destroy(collision.transform.parent.gameObject, 1);
            }
        }
        else if (collision.gameObject.GetComponent<Finish>())
        {
            _wonSound.Play();
            OnOff();
            finished = true;
            _clicked = false;
            personMove._speed = 0;
            Money.CoinsMain += (int)Money.CoinsPerGame;
            PlayerPrefs.SetInt("CoinsMain", Money.CoinsMain);
            _personAnim.SetInteger("PersonStates", 3); //Shop.selectedDance
            onFinishShowWonPage();
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Destroy(_person.GetComponent<Rigidbody>());
            personMove.enabled = false;
            this.GetComponent<PlayerMovement>().enabled = false;
        }
        else if (collision.name == "EndTrigger")
        {
            _triggered = false;
        }
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(0.6f);
        deathParticle.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        finishedBg = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void onFinishShowWonPage() 
    {
        _wonPage.SetActive(true);
    }
}
