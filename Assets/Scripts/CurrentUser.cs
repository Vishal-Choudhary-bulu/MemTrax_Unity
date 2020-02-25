using UnityEngine;

public class CurrentUser : MonoBehaviour
{
    public static CurrentUser thisUser;

    public User user;

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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GetFromPlayerPrefs();
    }

    
    void GetFromPlayerPrefs()
    {
        
    }

}
