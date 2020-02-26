using UnityEngine;

public class Authorization : MonoBehaviour
{
    private Firebase.Auth.FirebaseAuth auth;

    private CurrentUser user;
    private void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        user = CurrentUser.thisUser;
    }
    void Start()
    {
        CheckFireBase();
        if (user.IsLoggedIn())
        {
            Login();
        }
    }


    #region Email password auth

    public void Login()
    {
        string email = user.user.email;
        string password = user.user.password;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignUp()
    {
        string email = user.user.email;
        string password = user.user.password;
        string username = user.user.username;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;

            UpdateUserProfile();
            user.SetPlayerPrefs(CurrentUser.thisUser.user);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
    #endregion

    #region Update profile and others

    private void UpdateUserProfile()
    {
        string username = CurrentUser.thisUser.user.username;

        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = username
            };

            user.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.Log("something went wrong");
                }
                else
                {
                    Debug.Log("successfully updated profile");
                }
            });
        }
        else
        {
            Debug.Log("Couldn't update user profile");
        }
    }


    public void SignOut()
    {
        auth.SignOut();
        PlayerPrefs.SetInt("LoggedIn",0);
        User newUser = new User();
        user.SetPlayerPrefs(newUser);
    }


    public bool CheckFireBase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("firebase all set up");
                return true;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.  
            }
            return false;
        });
        return false;
    }


    #endregion

}
