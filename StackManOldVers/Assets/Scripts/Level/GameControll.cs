using UnityEngine;

public class GameControll : MonoBehaviour
{
    [SerializeField] private Animator _camAnimator;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _game;

    public void OnClickOpenShop() 
    {
        Invoke(nameof(onOpens), 1);
    }

    private void onOpens() 
    {
        _shop.SetActive(true);
    }
}
