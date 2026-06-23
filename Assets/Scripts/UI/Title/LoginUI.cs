using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private PanelSwap _panelSwap;

    [Header("Popups")]
    [SerializeField] GameObject _loginPopup;
    [SerializeField] GameObject _signupPopup;

    [Header("Login Buttons")]
    [SerializeField] Button _loginButton;
    [SerializeField] Button _signupButton;
    [SerializeField] Button _anonymousButton;

    [Header("Login Form")]
    [SerializeField] TMP_InputField _emailInput;
    [SerializeField] TMP_InputField _passwordInput;
    [SerializeField] TextMeshProUGUI _loginErrorText;

    [Header("Signup Buttons")]
    [SerializeField] Button _backButton;
    [SerializeField] Button _confirmButton;

    [Header("Signup Form")]
    [SerializeField] TMP_InputField _newEmailInput;
    [SerializeField] TMP_InputField _newNicknameInput;
    [SerializeField] TMP_InputField _newPasswordInput;
    [SerializeField] TextMeshProUGUI _signupErrorText;

    private async UniTaskVoid Start()
    {
        await UniTask.WaitUntil(() => AuthManager.Instance.IsInitialized);

        _loginButton.onClick.AddListener(() => OnLoginButtonClicked().Forget());
        _signupButton.onClick.AddListener(ShowSignupPopup);
        _anonymousButton.onClick.AddListener(() => OnAnonymousButtonClicked().Forget());

        _backButton.onClick.AddListener(ShowLoginPopup);
        _confirmButton.onClick.AddListener(() => OnSignupButtonClicked().Forget());

        UpdateUI().Forget();
    }

    public async UniTaskVoid UpdateUI()
    {
        if (!AuthManager.Instance.IsInitialized) return;

        bool isLoggedIn = AuthManager.Instance.IsLoggedIn;
        if (isLoggedIn)
        {
            _panelSwap.SwitchToTitle();
        }
    }

    private async UniTaskVoid OnLoginButtonClicked()
    {
        string email = _emailInput.text.Trim();
        string passwd = _passwordInput.text;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwd))
        {
            ShowError("이메일과 비밀번호를 입력하세요.");
            return;
        }

        SetButtonsInteractable(false);

        var (success, error) = await AuthManager.Instance.SignInUserWithEmailAsync(email, passwd);
        if (success)
        {
            UpdateUI().Forget();
        }
        else
        {
            ShowError(error);
        }

        SetButtonsInteractable(true);
    }

    private async UniTaskVoid OnSignupButtonClicked()
    {
        string email = _newEmailInput.text.Trim();
        string nickname = _newNicknameInput.text.Trim();
        string passwd = _newPasswordInput.text;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwd))
        {
            ShowError("이메일과 비밀번호를 입력하세요.");
            return;
        }

        SetButtonsInteractable(false);

        var (success, error) = await AuthManager.Instance.CreateUserWithEmailAsync(email, passwd, nickname);
        if (success)
        {
            UpdateUI().Forget();
        }
        else
        {
            ShowError(error);
        }

        SetButtonsInteractable(true);
    }

    private async UniTaskVoid OnAnonymousButtonClicked()
    {
        SetButtonsInteractable(false);

        var (success, error) = await AuthManager.Instance.SignInAnonymouslyAsync();
        if (success)
        {
            UpdateUI().Forget();
        }
        else
        {
            ShowError(error);
        }

        SetButtonsInteractable(true);
    }

    private void ShowError(string message)
    {
        _loginErrorText.text = message;
        _loginErrorText.color = Color.red;
        _signupErrorText.text = message;
        _signupErrorText.color = Color.red;
    }

    private void SetButtonsInteractable(bool interactable)
    {
        _loginButton.interactable = interactable;
        _signupButton.interactable = interactable;
        _anonymousButton.interactable = interactable;

        _backButton.interactable = interactable;
        _confirmButton.interactable = interactable;
    }

    private void ShowSignupPopup()
    {
        _loginPopup.SetActive(false);
        _signupPopup.SetActive(true);
    }

    private void ShowLoginPopup()
    {
        _loginPopup.SetActive(true);
        _signupPopup.SetActive(false);
    }
}