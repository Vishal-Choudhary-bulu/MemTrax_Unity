using UnityEngine;
using TMPro;

public class InputHelper : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField emailField;

    [SerializeField]
    private TMP_InputField passField;

    private string email;
    private string password;

    void Start()
    {
        if(passField == null || emailField == null)
        {
            Debug.Log("Input fields not assigned properly");
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
        CurrentUser.thisUser.user.password = password; 
    }

    public void UpdateEmail()
    {
        email = emailField.text;
        CurrentUser.thisUser.user.email = email;
    }
}
