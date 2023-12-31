using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using Firebase.Database;
using System.Data.Common;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Threading;

public class FirebaseAuthManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public GameObject loginObject;
    public GameObject registerObject;
    [SerializeField] private DatabaseReference database;
    ListenerRegistration listenerRegistration;

    // Login Variables
    [Space]
    [Header("Login")]
    [SerializeField] TMP_InputField emailLoginField;
    [SerializeField] TMP_InputField passwordLoginField;

    // Registration Variables
    [Space]
    [Header("Registration")]
    [SerializeField] TMP_InputField nameRegisterField;
    [SerializeField] TMP_InputField emailRegisterField;
    [SerializeField] TMP_InputField passwordRegisterField;
    [SerializeField] TMP_InputField confirmPasswordRegisterField;
    private void Start()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());
    }
    private IEnumerator CheckAndFixDependenciesAsync()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        dependencyStatus = dependencyTask.Result;
        if (dependencyStatus == DependencyStatus.Available)
        {
            InitializeFirebase();
            yield return new WaitForEndOfFrame();
            StartCoroutine(CheckForAutoLogin());
        }
        else
        {
            Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
        }
    }

    void InitializeFirebase()
    {
        //Set the default instance object
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    private IEnumerator CheckForAutoLogin()
    {
        if (user != null)
        {
            var reloadUser = user.ReloadAsync();
            yield return new WaitUntil(() => reloadUser.IsCompleted);
            AutoLogin();
        }
        else
        {
            UIManager.Instance.OpenLoginPanel();
        }
    }
    private void AutoLogin()
    {
        if (user != null)
        {
            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);
            APIUser.Instance.getConnectedUserByUId(user.Email);

            UnityEngine.SceneManagement.SceneManager.LoadScene("HomeText");
        }
        else
        {
            UIManager.Instance.OpenLoginPanel();
        }
    }
    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }
    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }
    public void BtnRegisterButton()
    {
        loginObject.SetActive(false);
        registerObject.SetActive(true);
    }
    private IEnumerator LoginAsync(string email, string password)
    {

        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;


            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    break;
                default:
                    failedMessage = "Login Failed";
                    break;
            }

            Debug.Log(failedMessage);
        }
        else
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);
            APIUser.Instance.getConnectedUserByUId(email);

            // yield return new WaitUntil(() => APIUser.Instance.getConnectedUserByUId(email));
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomeText");

            References.userName = user.DisplayName;

        }
    }
    public async void getInfor(string email)
    {
        Debug.LogFormat("begin getInfor");
        Task<int> result = GetInforUserProcess(email);
        int val = await result;
        if (val == 1)
        {
            Debug.LogFormat("if var == 1");

        }
        Debug.LogFormat("end");
    }
    static async Task<int> GetInforUserProcess(string email)
    {
        Console.WriteLine("LongProcess Started");


        Console.WriteLine("LongProcess Completed");

        return 1;
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }
    public void BtnLoginButton()
    {
        loginObject.SetActive(true);
        registerObject.SetActive(false);
    }
    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
    {

        if (name == "")
        {
            Debug.LogError("User Name is empty");
        }
        else if (email == "")
        {
            Debug.LogError("email field is empty");
        }
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null || registerTask.Result == null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Registration Failed! Becuase ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        break;
                    default:
                        failedMessage = "Registration Failed";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profile update Failed! Becuase ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Profile update Failed";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    CreateNewUser(username: name, password: password, email: email);
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);
                    loginObject.SetActive(true);
                    registerObject.SetActive(false);
                    // UIManager.Instance.OpenLoginPanel();
                }
            }
        }
    }
    public void CreateNewUser(string username, string password, string email)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        string userId;
        userId = System.Guid.NewGuid().ToString(); ;
        User user = new User(userId, email, username, password, "1", 0);
        reference.Child("Users")
            .Child(userId)
            .Child("id_user")
            .SetValueAsync(user.id_user);
        reference.Child("Users")
            .Child(userId)
            .Child("username")
            .SetValueAsync(user.username);
        reference.Child("Users")
            .Child(userId)
            .Child("password")
            .SetValueAsync(user.password);
        reference.Child("Users")
            .Child(userId)
            .Child("email")
            .SetValueAsync(user.email);
        reference.Child("Users")
            .Child(userId)
            .Child("id_level")
            .SetValueAsync(user.id_level);
        reference.Child("Users")
            .Child(userId)
            .Child("experience")
            .SetValueAsync(user.experience);

        Debug.Log("New User Created");
    }
}
