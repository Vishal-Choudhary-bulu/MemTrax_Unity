using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender
{
    male,
    female,
    other
}

public class User
{
    public int id { get; set; }
    public string username{ get; set; }
    public string password{ get; set; }
    public string email{ get; set; }
    public int age { get; set; }
    public Gender gender { get; set; }

    public User(int newid, string newusername, string newpassword, string newemail, int newage, Gender newgender)
    {
        id = newid;
        username = newusername;
        password = newpassword;
        email = newemail;
        age = newage;
        gender = newgender;
    }

    public User()
    {

    }
    
}
