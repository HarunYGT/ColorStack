using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject MovePos;
    public GameObject[] Sockets;
    public int freeSocket;
    public List<GameObject> Rings = new();
    [SerializeField] private GameManager gameManager;

    int ringCompleteNum;

    public GameObject GiveTheTopRing()
    {
        return Rings[Rings.Count - 1];
    }
    public GameObject GiveTheFreeSocket()
    {
        return Sockets[freeSocket];
    }

    public void ChangeSocketOperation(GameObject ObjectToBeDeleted)
    {
        Rings.Remove(ObjectToBeDeleted);
        if(Rings.Count !=0)
        {
            freeSocket--;
            Rings[^1].GetComponent<Ring>().canItMove = true;
        }
        else
        {
            freeSocket = 0;
        }
    }

    public void CheckTheRings()
    {
        if(Rings.Count == 4)
        {
            string Color = Rings[0].GetComponent<Ring>().Color;
            foreach (var item in Rings)
            {
                if(Color == item.GetComponent<Ring>().Color)
                {
                    ringCompleteNum++;
                }
            }
            if(ringCompleteNum == 4)
            {
                gameManager.StandCompleted();
                CompletedStandOperation();
            }
            else
            {
                ringCompleteNum = 0;
            }
        }
    }
    void CompletedStandOperation(){
      foreach (var item in Rings)
      {
        item.GetComponent<Ring>().canItMove = false;
        Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
        color.a = 150;
        item.GetComponent<MeshRenderer>().material.SetColor("_Color",color);
        gameObject.tag="CompletedStand";
      }  
    }
}
