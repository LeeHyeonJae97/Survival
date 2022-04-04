using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public UserSO User
    {
        get
        {
            if (_user == null)
            {
                _user = Resources.Load<UserSO>("User");
                _user.Load();
            }
            return _user;
        }
    }

    private UserSO _user;

    private void OnDisable()
    {
        User.Save();
    }
}
