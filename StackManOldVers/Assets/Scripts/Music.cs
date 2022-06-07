using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource main;
    [SerializeField] private AudioSource nomain;
    [SerializeField] private AudioSource snomain;

    [SerializeField] private Image musicBtn;

    [SerializeField] private Sprite OffMusic;
    [SerializeField] private Sprite OnMusic;

    public static int offOn = 1;

    private void Start()
    {
        offOn = PlayerPrefs.GetInt("sounderg", offOn);

        Debug.Log(offOn);
    }

    public void onClickOffOnMusic() 
    {
        offOn ++;
        if (offOn > 1)
        {
            offOn = 0;
        }
    }

    private void Update()
    {
        PlayerPrefs.SetInt("sounderg", offOn);

        if (offOn == 0)
        {
            main.enabled = false;
            nomain.enabled = false;
            snomain.enabled = false;
            musicBtn.sprite = OffMusic;
        }
        else if (offOn == 1)
        {
            main.enabled = true;
            nomain.enabled = true;
            snomain.enabled = true;
            musicBtn.sprite = OnMusic;
        }
    }
}
