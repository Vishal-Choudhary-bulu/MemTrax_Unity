using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;

public class Authorization : MonoBehaviour
{

    public string webClientId = "Client ID here";


    private FirebaseAuth auth;

    private GoogleSignInConfiguration configuration;

    private CurrentUser user;

    private static FirebaseUser fireUser;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        //AuthStateChanged(this, null);
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true
        };
    }

    void Start()
    {
        CheckFireBase();
        user = CurrentUser.thisUser;
        SignOut();
        if (user.IsLoggedIn())
        {
            //Login();
        }
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != fireUser)
        {
            bool signedIn = (fireUser != auth.CurrentUser)&& (auth.CurrentUser != null);

            if (!signedIn && user != null)
            {
                AppEvents.RaiseUserLogout();
            }
            fireUser = auth.CurrentUser;
            if (signedIn)
            {
                AppEvents.RaiseUserLogin();
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    #region Email password auth

    public void Login()
    {
        string email = user.current.email;
        string password = user.current.password;
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

            FirebaseUser newUser = task.Result;

            user.current.username = task.Result.DisplayName;
            user.current.email = task.Result.Email;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignUp()
    {
        string email = user.current.email;
        string password = user.current.password;
        string username = user.current.username;

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
            FirebaseUser newUser = task.Result;

            UpdateUserProfile();
            user.SetPlayerPrefs(CurrentUser.thisUser.current);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        
    }

    #endregion

    #region Update profile and others

    private void UpdateUserProfile()
    {
        string username = CurrentUser.thisUser.current.username;

        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            UserProfile profile = new UserProfile
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
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.  
            }
            return false;
        });
        return false;
    }


    #endregion

    #region Google Auth
    public void SignInGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Failed to SignIn");
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Cancelled login");
        }
        else
        {
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    Debug.LogError("Failed to signin to firebase");
            }
            else
            {
                Debug.Log("Signed In" + task.Result.DisplayName);
                user.current.username = task.Result.DisplayName;
                user.current.email = task.Result.Email;
            }
        });

        
    }

    public void OnSignOut()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        SignOut();
    }

    #endregion

}
