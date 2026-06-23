using System;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    // --- Singleton ------------------------------
    private static AuthManager _instance;
    public static AuthManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<AuthManager>();

                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<AuthManager>();
                    singletonObject.name = typeof(AuthManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("[Auth] AuthManager 싱글톤 생성.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        if (Auth != null)
        {
            Auth.StateChanged -= OnAuthStateChanged;
        }
    }

    // --- Auth ------------------------------
    public FirebaseAuth Auth { get; private set; }
    public FirebaseUser CurrentUser => Auth.CurrentUser;

    private bool _lastNotifiedSignedIn = false;
    public bool IsInitialized { get; private set; } = false;
    public bool IsLoggedIn => CurrentUser != null;

    public event Action<bool> LoginStateChanged;

    private async UniTaskVoid Start()
    {
        bool ready = await FirebaseInitializer.Instance.WaitForInitializationAsync();
        if (!ready)
        {
            Debug.LogError("[Auth] 파이어 베이스 초기화 실패 Auth 초기화 불가...");
            return;
        }

        Auth = FirebaseInitializer.Instance.Auth;
        Auth.StateChanged += OnAuthStateChanged;

        IsInitialized = true;

        NotifyLoginState();
    }

    private void OnAuthStateChanged(object sender, EventArgs eventArgs)
    {
        NotifyLoginState();
    }

    private void NotifyLoginState()
    {
        bool signedIn = IsLoggedIn;
        if (signedIn == _lastNotifiedSignedIn) return;

        _lastNotifiedSignedIn = signedIn;
        Debug.Log(signedIn ? $"[Auth] 로그인 상태: {CurrentUser.UserId}" : "[Auth] 로그아웃 상태");
        LoginStateChanged?.Invoke(signedIn);
    }

    public async UniTask<bool> WaitForInitializationAsync()
    {
        if (IsInitialized) return true;

        bool firebaseReady = await FirebaseInitializer.Instance.WaitForInitializationAsync();
        if (!firebaseReady) return false;

        await UniTask.WaitUntil(() => IsInitialized);
        return true;
    }

    public async UniTask<(bool success, string error)> SignInAnonymouslyAsync()
    {
        if (!IsInitialized)
        {
            bool ready = await FirebaseInitializer.Instance.WaitForInitializationAsync();
            if (!ready) return (false, "초기화가 완료되지 않았습니다.");
        }

        try
        {
            Debug.Log("[Auth] 익명 로그인 시도...");
            await Auth.SignInAnonymouslyAsync();

            NotifyLoginState();

            Debug.Log($"[Auth] 익명 로그인 성공: {CurrentUser.UserId}");
            return (true, null);
        }
        catch (Exception ex)
        {
            Debug.Log($"[Auth] 익명 로그인 실패: {ex.Message}");
            return (false, ParseFirebaseError(ex.Message));
        }
    }

    public async UniTask<(bool success, string error)> CreateUserWithEmailAsync(string email, string passwd, string nickname = "Anonymous")
    {
        if (!IsInitialized)
        {
            bool ready = await FirebaseInitializer.Instance.WaitForInitializationAsync();
            if (!ready) return (false, "초기화가 완료되지 않았습니다.");
        }

        try
        {
            Debug.Log("[Auth] 회원 가입 시도...");
            await Auth.CreateUserWithEmailAndPasswordAsync(email, passwd);

            await CurrentUser.UpdateUserProfileAsync(new UserProfile { DisplayName = nickname });

            NotifyLoginState();

            Debug.Log($"[Auth] 회원 가입 성공: {CurrentUser.UserId} ({CurrentUser.DisplayName})");
            return (true, null);
        }
        catch (Exception ex)
        {
            Debug.Log($"[Auth] 회원 가입 실패: {ex.Message}");
            return (false, ParseFirebaseError(ex.Message));
        }
    }

    public async UniTask<(bool success, string error)> SignInUserWithEmailAsync(string email, string passwd)
    {
        if (!IsInitialized)
        {
            bool ready = await FirebaseInitializer.Instance.WaitForInitializationAsync();
            if (!ready) return (false, "초기화가 완료되지 않았습니다.");
        }

        try
        {
            Debug.Log("[Auth] 로그인 시도...");
            await Auth.SignInWithEmailAndPasswordAsync(email, passwd);

            NotifyLoginState();

            Debug.Log($"[Auth] 로그인 성공: {CurrentUser.UserId}");
            return (true, null);
        }
        catch (Exception ex)
        {
            Debug.Log($"[Auth] 로그인 실패: {ex.Message}");
            return (false, ParseFirebaseError(ex.Message));
        }
    }

    public async UniTask<(bool success, string error)> LinkWithEmailAsync(string email, string passwd)
    {
        if (!IsInitialized)
        {
            bool ready = await FirebaseInitializer.Instance.WaitForInitializationAsync();
            if (!ready) return (false, "초기화가 완료되지 않았습니다.");
        }

        if (CurrentUser == null) return (false, "로그인된 사용자가 없습니다.");
        if (!CurrentUser.IsAnonymous) return (false, "이미 이메일 계정에 연동된 사용자입니다.");

        try
        {
            Debug.Log("[Auth] 익명 → 이메일 계정 연동 시도...");
            Credential credential = EmailAuthProvider.GetCredential(email, passwd);
            await CurrentUser.LinkWithCredentialAsync(credential);

            NotifyLoginState();

            Debug.Log($"[Auth] 계정 연동 성공: {CurrentUser.UserId}");
            return (true, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[Auth] 계정 연동 실패: {ex.Message}");
            return (false, ParseFirebaseError(ex.Message));
        }
    }

    public void SignOut()
    {
        if (Auth != null && CurrentUser != null)
        {
            Debug.Log("[Auth] 로그아웃");
            Auth.SignOut();

            NotifyLoginState();
        }
    }

    private string ParseFirebaseError(string error)
    {
        Debug.LogWarning($"[Auth] Firebase 에러 원문: {error}");

        string lower = error.ToLowerInvariant();

        if (lower.Contains("already in use") || lower.Contains("email-already"))
        {
            return "이미 사용 중인 이메일입니다.";
        }
        if (lower.Contains("at least 6") || lower.Contains("weak") || lower.Contains("password is invalid"))
        {
            return "비밀번호는 6자 이상이어야 합니다.";
        }
        if (lower.Contains("badly formatted") || lower.Contains("invalid-email"))
        {
            return "이메일 형식이 올바르지 않습니다.";
        }
        if (lower.Contains("network"))
        {
            return "네트워크 연결을 확인해주세요.";
        }

        return "이메일 또는 비밀번호를 확인해주세요.";
    }
}