using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserBehaviour : MonoBehaviour
{
    /// <summary>
    /// The visual text of the username
    /// </summary>
    [Tooltip("The visual text of the username")]
    [SerializeField]
    private Text userField;

    /// <summary>
    /// The name of the user/client
    /// </summary>
    [Tooltip("The name of the user")]
    [SerializeField]
    private string userName = "Plantion Customer";


    /// <summary>
    /// Start is called before the first frame update 
    /// </summary>
    void Start()
    {
        SetupInputfield("Username", userName);

        if (PlayerPrefs.HasKey(userName))
        {
            string username = PlayerPrefs.GetString(userName);
            PhotonNetwork.NickName = username;
            userField.text = "User: " + username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Sets the name of the client
    /// </summary>
    /// <param name="value">The name of the client</param>
    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player name == null");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(userName, value);
        userField.text = "User: " + value;
    }

    /// <summary>
    /// Setups the input field with the data from the PlayerPref file
    /// </summary>
    /// <param name="field">The input field</param>
    /// <param name="key">The key used to find the value in PlayerPref file</param>
    private void SetupInputfield(string field, string key)
    {
        InputField _inputField = GameObject.Find(field).GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(key))
                _inputField.text = PlayerPrefs.GetString(key);
        }
        else
            Debug.Log("Inputfield == null");

    }
}
