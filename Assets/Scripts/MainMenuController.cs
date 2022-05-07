using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.NetworkRoom;
using UnityEngine.UI;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
[AddComponentMenu("Network/NetworkManagerHUD")]
[RequireComponent(typeof(NetworkManager))]
[HelpURL("https://mirror-networking.gitbook.io/docs/components/network-manager-hud")]
public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenuView;
    public GameObject MultiplayerView;
    public TextField URLField;

    private NetworkManager manager;

    public void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    #region MainMenuFunctions
    public void StartMultiplayer()
    {
        MultiplayerView.SetActive(true);
        MainMenuView.SetActive(false);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void StartSinglePlayer()
    {
        manager.StartHost();
    }

    public void StartSinglePlayerScene()
    {
        NetworkClient.Ready();
        if (NetworkClient.localPlayer == null)
        {
            NetworkClient.AddPlayer();
        }
        var roomManager = GetComponent<NetworkRoomManagerExt>();
        roomManager.StartGameplayScene();

        MultiplayerView.SetActive(false);
        MainMenuView.SetActive(false);
    }
    #endregion

    #region MultiplayerFunctions
    public void StartServerClient()
    {
        if (!NetworkClient.active)
        {
            manager.StartHost();

            MultiplayerView.SetActive(false);
            MainMenuView.SetActive(false);
        }
    }

    public void StartClientButton()
    {
        if (!NetworkClient.active)
        {
            manager.StartClient();

            MultiplayerView.SetActive(false);
            MainMenuView.SetActive(false);
        }
        manager.networkAddress = URLField.text;
    }

    public void StartServerButton()
    {
        if (!NetworkClient.active)
        {
            manager.StartServer();

            MultiplayerView.SetActive(false);
            MainMenuView.SetActive(false);
        }
    }



    public void ExitFromMultiplayer()
    {
        MultiplayerView.SetActive(false);
        MainMenuView.SetActive(true);
    }
    #endregion
}
