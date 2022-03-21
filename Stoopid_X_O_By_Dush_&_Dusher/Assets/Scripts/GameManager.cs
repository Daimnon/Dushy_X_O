using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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

    public List<Vector2> tilePosList = new List<Vector2>(9);

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
        Vector3 tileTr = tile.transform.position;

        print($"{(Vector2)tileTr}");
        //draw the shape on the UI

        //update the other player about the shape
        networkManager.SendMessage($"Player Placed Shape at {tile.name}: {(Vector2)tileTr}");// your job to finish it
    }
}
