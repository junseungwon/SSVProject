using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public GameObject player;
    public ItemTable ItemTable { get; set; }
    public ItemBox ItemBox { get; set; }
    public PlayerController PlayerController { get; set; }
    public PlayerData PlayerData { get; set; }

    public UiManager UiManager { get; set; }

}
