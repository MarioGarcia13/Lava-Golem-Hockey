using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints = new List<Transform>();
    private PlayerInputManager playerInputManager;
    public Transform P1Spawn;
    public Transform P2Spawn;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        startingPoints.Add(P1Spawn);
        startingPoints.Add(P2Spawn);
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        Debug.Log(startingPoints[0].position);
        Debug.Log(players.Count);

        //Transform playerParent = player.transform.parent;
        //playerParent.position = startingPoints[players.Count - 1].position;

        //test

        player.transform.position = startingPoints[startingPoints.Count - 1].position;
    }

}
