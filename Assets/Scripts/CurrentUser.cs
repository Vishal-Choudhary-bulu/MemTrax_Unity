using UnityEngine;

public class CurrentUser : MonoBehaviour
{
    public static CurrentUser thisUser;

    public User user;

    [HideInInspector]
    public bool LoggedIn = false;

    public GameObject loginPanel;
    public GameObject signupPanel;


    private void Awake()
    {
        if(thisUser != null && thisUser != this)
        {
            Destroy(this);
        }
        else
        {
            thisUser = this;
        }
        user = new User();

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
        user.email = PlayerPrefs.GetString("email");
        user.password = PlayerPrefs.GetString("password");
    }

    public bool IsLoggedIn()
    {
        return (PlayerPrefs.GetInt("LoggedIn") == 1 ? true : false);
    }

}
