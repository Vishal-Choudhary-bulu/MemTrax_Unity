using UnityEngine;
using TMPro;

public class InputHelper : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField emailField = null;

    [SerializeField]
    private TMP_InputField passField = null;

    [SerializeField]
    private TMP_InputField usernameField = null;

    private string email;
    private string password;
    private string username;

    void Start()
    {
        if(passField == null || emailField == null)
        {
            Debug.Log("Input fields not assigned properly");
        }
        if(usernameField == null)
        {
            //this is for login
        }
    }

    public void Hide()
    {
        passField.contentType = TMP_InputField.ContentType.Password;
        passField.Select();
        passField.text = password;
    }

    public void Show()
    {
        passField.contentType = TMP_InputField.ContentType.Standard;
        passField.Select();
        passField.text = password;
    }

    public void UpdatePassword()
    {
        password = passField.text;
        CurrentUser.thisUser.current.password = password; 
    }

    public void UpdateEmail()
    {
        email = emailField.text;
        CurrentUser.thisUser.current.email = email;
    }

    public void UpdateUserName()
    {
        username = usernameField.text;
        CurrentUser.thisUser.current.username = username;
    }
}
