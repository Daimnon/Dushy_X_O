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

    [SerializeField]
    private TextMeshProUGUI[] _shapes = new TextMeshProUGUI[9];

    [SerializeField]
    private GameObject _bruhPanel;

    [SerializeField]
    private AudioSource _bruhAudioSource;

    private int _turnCounter = 0;

    public bool _playerOneTurn = true, _playerTwoTurn = false;

    private void Start()
    {
        networkManager.StartUDP();
    }

    public NetworkManager networkManager; //don't forget to drag in inspector

    public void GotNetworkMessage (string message)
    {
        Debug.Log("got network message: " + message);
        switch(message)
        {
            //do something with the message
            //case 5:
            //Do something
        }
    }

    public void PositionClicked(Image tile)
    {
        if (_turnCounter == 8)
        {
            print("Bruh");
            _bruhPanel.SetActive(true);
            _bruhAudioSource.Play();
        }

        Vector3 tileTr = tile.transform.position;

        print($"{(Vector2)tileTr}");
        //draw the shape on the UI


        ChangeShape(tile);

        //update the other player about the shape
        networkManager.SendMessage($"Player Placed Shape at {tile.name}: {(Vector2)tileTr}");// your job to finish it

        
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

    private void ChangeShape(Image tile)
    {
        for (int i = 0; i < _shapes.Length; i++)
        {
            if (_shapes[i].transform.parent.name == tile.name && _playerOneTurn)
                _shapes[i].text = "X";

            else if (_shapes[i].transform.parent.name == tile.name && _playerTwoTurn)
                _shapes[i].text = "O";
        }
    }
}
