using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public GameObject _belongStand,_belongRingSocket;
    public bool canItMove;
    public string Color;
    public GameManager gameManager;

    GameObject MovePos,goingToStand;
    bool Choosed,PosChange,SitSocket,ReturnSocket;

    public void Move(string operation,GameObject stand =null, GameObject socket = null , GameObject MoveObject=null)
    {
        switch(operation)
        {
            case "Choosed":
                MovePos = MoveObject;
                Choosed = true;
                break;
            case "PosChange":
                goingToStand = stand;
                _belongRingSocket = socket;
                MovePos = MoveObject;
                PosChange = true;
                break;
            case "ReturnSocket":
                ReturnSocket = true;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Choosed)
        {
            transform.position = Vector3.Lerp(transform.position,MovePos.transform.position,.2f);
            if(Vector3.Distance(transform.position,MovePos.transform.position) < .10)
            {
                Choosed = false;
            }
        }
        if(PosChange)
        {
            transform.position = Vector3.Lerp(transform.position,MovePos.transform.position,.2f);
            if(Vector3.Distance(transform.position,MovePos.transform.position) < .10)
            {
                PosChange = false;
                SitSocket = true;
            }
        }
        if(SitSocket)
        {
            transform.position = Vector3.Lerp(transform.position,_belongRingSocket.transform.position,.2f);
            if(Vector3.Distance(transform.position,_belongRingSocket.transform.position) < .10)
            {
                transform.position = _belongRingSocket.transform.position;
                SitSocket = false;
                _belongStand = goingToStand; 

                if(_belongStand.GetComponent<Stand>().Rings.Count > 1)
                {
                    _belongStand.GetComponent<Stand>().Rings[^2].GetComponent<Ring>().canItMove = false;
                }
                gameManager.isMove = false;
            }
        }
        if(ReturnSocket)
        {
            transform.position = Vector3.Lerp(transform.position,_belongRingSocket.transform.position,.2f);
            if(Vector3.Distance(transform.position,_belongRingSocket.transform.position) < .10)
            {
                transform.position = _belongRingSocket.transform.position;
                ReturnSocket = false;
                gameManager.isMove = false;
            }
        }

    }
}
