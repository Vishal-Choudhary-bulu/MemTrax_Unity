using UnityEngine;
using Google;
using Firebase.Auth;

public class CurrentUser : MonoBehaviour
{
    public static CurrentUser thisUser;

    public User current;

    private FirebaseAuth authLogOut;

    [HideInInspector]
    public bool LoggedIn = false;

    public GameObject loginPanel;
    public GameObject signupPanel;


    private void Awake()
    {
        authLogOut = FirebaseAuth.DefaultInstance;

        if(thisUser != null && thisUser != this)
        {
            Destroy(this);
        }
        else
        {
            thisUser = this;
        }
        current = new User();

        GetFromPlayerPrefs();
        DontDestroyOnLoad(gameObject);

        LoggedIn = IsLoggedIn();

    }

    public void SetPlayerPrefs(User user)
    {
        PlayerPrefs.SetString("email", user.email);
        PlayerPrefs.SetString("password", user.password);
        PlayerPrefs.SetInt("LoggedIn", 1);
    }
    
    public void GetFromPlayerPrefs()
    {
        current.email = PlayerPrefs.GetString("email");
        current.password = PlayerPrefs.GetString("password");
    }

    public bool IsLoggedIn()
    {
        return (PlayerPrefs.GetInt("LoggedIn") == 1 ? true : false);
    }

    public void LogOut()
    {
        authLogOut.SignOut();
        PlayerPrefs.SetInt("LoggedIn", 0);
        User newUser = new User();
        SetPlayerPrefs(newUser);
        GoogleSignIn.DefaultInstance.SignOut();
    }
}
