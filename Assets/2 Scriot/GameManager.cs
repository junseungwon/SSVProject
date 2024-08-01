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
        DontDestroyOnLoad(this);
    }
    
    public GameObject player;
    public ItemBox ItemBox { get; set; }
    public PlayerController PlayerController { get; set; }
    public PlayerData PlayerData { get; set; }

    public MakeItemBox MakeItemBox { get; set; }
    public UiManager UiManager { get; set; }

    public ItemTable itemTable { get; set; }

    public PlayStoryManager PlayStoryManager { get; set; }

    public PlanNote PlanNote { get; set; }

    public PlayMoveGuideManager PlayMoveGuideManager { get; set; }
}
