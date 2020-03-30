using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Launcher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// The version of the login/game
    /// </summary>
    private string gameVersion = "1.0";

    /// <summary>
    /// The text field that contains the status of the connection
    /// </summary>
    [Tooltip("The text field that contains the status of the connection")]
    [SerializeField]
    private Text statusText;

    /// <summary>
    /// Contains the status of the connection
    /// </summary>
    private string status = "Connection Status: ";

    /// <summary>
    /// The visual text of the version
    /// </summary>
    [Tooltip("The visual text of the username")]
    [SerializeField]
    private Text versionField;


    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    /// <summary>
    /// The name of the room/lobby
    /// </summary>
    [Tooltip("The name of the auction room")]
    [SerializeField]
    private string roomName = "Plantion veiling";

    /// <summary>
    /// Allows debugging mode AKA host you're own room
    /// </summary>
    [Tooltip("Turns on developer mode")]
    [SerializeField]
    private bool debug = false;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        versionField.text = "Version: " + gameVersion;
    }

    // Start is called before the first frame update
    void Start() => Connect();

    // Update is called once per frame
    void Update() => statusText.text = status;


    /// <summary>
    /// Connects the client to the plantion server
    /// </summary>
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (debug)
            {
                //if(PhotonNetwork.IsConnectedAndReady())
                PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = maxPlayersPerRoom });
            }
            if(PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
                //PhotonNetwork.JoinRandomRoom();
            }

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #region Callbacks & Overrides
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server!");
        status += "Connected to master server";
        //PhotonNetwork.JoinRandomRoom();
        if (!PhotonNetwork.InLobby && PhotonNetwork.IsConnectedAndReady)
            PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        Debug.Log("Connected");
        if (!PhotonNetwork.InLobby && PhotonNetwork.IsConnectedAndReady)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        status = "Connection Status: Not connected to master server";
        Debug.LogWarningFormat("Failed to connect. Reason: {0}", cause);
    }

    public override void OnJoinedLobby()
    {
        status = "Connection Status: Client is in lobby";
        Debug.Log("User is in lobby");
    }

    public override void OnLeftLobby()
    {
        status = "Connection Status: Left the lobby";
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        status = "Connection Status: Joined room: " + roomName;
        Debug.Log("Joined room: " + roomName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Failed to connect to {0}. Reason: {1} \n With Message: {2}", roomName, returnCode, message);
        status = "Connection Status: Plantion room";
    }

    #endregion
}
