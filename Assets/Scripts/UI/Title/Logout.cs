using UnityEngine;
using UnityEngine.UI;

public class Logout : MonoBehaviour
{
    [SerializeField] private PanelSwap _panelSwap;
    [SerializeField] Button _signoutButton;

    private void Awake()
    {
        _signoutButton.onClick.AddListener(OnLogoutButtonClicked);
    }

    private void Start()
    {
        AuthManager.Instance.LoginStateChanged += OnButtonInteractable;
    }

    private void OnButtonInteractable(bool loginState)
    {
        _signoutButton.interactable = loginState;
    }

    private void OnLogoutButtonClicked()
    {
        Debug.Log("[Auth] 로그아웃.");
        AuthManager.Instance.SignOut();
        _panelSwap.SwitchToTitle();
    }
}