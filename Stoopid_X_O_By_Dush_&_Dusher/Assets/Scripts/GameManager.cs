using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Protocol
    /*
     * This is a proposal only
     Message = playerID [space] positionID

     int that represents position of the player's step.
     [0][1][2]
     [3][4][5]
     [6][7][8]
     */
    #endregion

    [SerializeField] private TextMeshProUGUI[] _shapes = new TextMeshProUGUI[9];
    [SerializeField] private GameObject _bruhPanel;
    [SerializeField] private AudioSource _bruhAudioSource;

    private int _turnCounter = 0;

    public bool _playerOneTurn = true, _playerTwoTurn = false;
    public NetworkManager networkManager; //don't forget to drag in inspector

    private void Start()
    {
        networkManager.StartUDP();
    }

    public static void GotNetworkMessage (string message)
    {
        Debug.Log("got network message: " + message);
        switch(message)
        {
            case "2":
                    break;
            //do something with the message
            //case 5:
            //Do something
        }
    }

    public void PositionClicked(Image tile)
    {
        EndDraw();
        
        //draw the shape on the UI
        DrawShape(tile);
        print($"Message Sent");
        SwitchTurn();
    }

    private void SwitchTurn()
    {
        _turnCounter++;

        if (_playerOneTurn)
            PlayerOneTurn();

        else
            PlayerTwoTurn();
    }

    private void PlayerOneTurn()
    {
        _playerOneTurn = false;
        _playerTwoTurn = true;

        networkManager.ListeningPort = 40001;
        networkManager.SendingPort = 40000;
    }

    private void PlayerTwoTurn()
    {
        _playerOneTurn = true;
        _playerTwoTurn = false;

        networkManager.ListeningPort = 40000;
        networkManager.SendingPort = 40001;
    }

    private void DrawShape(Image tile)
    {
        for (int i = 0; i < _shapes.Length; i++)
        {
            if (_shapes[i].transform.parent.name == tile.name && _playerOneTurn)
            {
                _shapes[i].text = "X";

                //update the other player about the shape
                //networkManager.SendMessage($"{_shapes[i].transform.parent.name}");
                networkManager.SendMessage($"Player Placed {_shapes[i].text} at {tile.name} \n");
                print($"{_shapes[i].transform.parent.name}");
            }

            else if (_shapes[i].transform.parent.name == tile.name && _playerTwoTurn)
            {
                _shapes[i].text = "O";

                //update the other player about the shape
                //networkManager.SendMessage($"{_shapes[i]}");
                networkManager.SendMessage($"Player Placed {_shapes[i].text} at {tile.name} \n");
                print($"{_shapes[i].transform.parent.name}");
            }
        }
    }

    private void EndDraw()
    {
        if (_turnCounter == 8)
        {
            print("Bruh");
            _bruhPanel.SetActive(true);
            _bruhAudioSource.Play();
        }
    }
}
