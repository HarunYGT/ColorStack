using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    GameObject choosedObject;
    GameObject choosedStand;
    Ring ring;
    public bool isMove;
    [SerializeField] private TextMeshProUGUI levelText;

    public int targetColorNum;
    public int completedColorNum;

    void Start()
    {
        levelText.text = "Level : " + SceneManager.GetActiveScene().buildIndex.ToString();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (choosedObject != null && choosedStand != hit.collider.gameObject)
                    { // Send a Ring
                        Stand _Stand = hit.collider.GetComponent<Stand>();

                        if (_Stand.Rings.Count != 4 && _Stand.Rings.Count != 0)
                        {
                            if (ring.Color == _Stand.Rings[^1].GetComponent<Ring>().Color)
                            {
                                choosedStand.GetComponent<Stand>().ChangeSocketOperation(choosedObject);

                                ring.Move("PosChange", hit.collider.gameObject, _Stand.GiveTheFreeSocket(), _Stand.MovePos);

                                _Stand.freeSocket++;
                                _Stand.Rings.Add(choosedObject);
                                _Stand.CheckTheRings();

                                choosedObject = null;
                                choosedStand = null;
                            }
                            else
                            {
                                ring.Move("ReturnSocket");
                                choosedObject = null;
                                choosedStand = null;
                            }

                        }
                        else if (_Stand.Rings.Count == 0)
                        {
                            choosedStand.GetComponent<Stand>().ChangeSocketOperation(choosedObject);

                            ring.Move("PosChange", hit.collider.gameObject, _Stand.GiveTheFreeSocket(), _Stand.MovePos);

                            _Stand.freeSocket++;
                            _Stand.Rings.Add(choosedObject);
                             _Stand.CheckTheRings();

                            choosedObject = null;
                            choosedStand = null;
                        }
                        else
                        {
                            ring.Move("ReturnSocket");
                            choosedObject = null;
                            choosedStand = null;
                        }

                    }
                    else if (choosedStand == hit.collider.gameObject)
                    {
                        ring.Move("ReturnSocket");
                        choosedObject = null;
                        choosedStand = null;
                    }
                    else
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        choosedObject = _Stand.GiveTheTopRing();
                        ring = choosedObject.GetComponent<Ring>();
                        isMove = true;

                        if (ring.canItMove)
                        {
                            ring.Move("Choosed", null, null, ring._belongStand.GetComponent<Stand>().MovePos);
                            choosedStand = ring._belongStand;
                        }
                    }
                }
            }
        }
    }

    public void StandCompleted()
    {
        completedColorNum++;
        if(completedColorNum == targetColorNum)
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Level",SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
