using UnityEngine;

public class NoneState : IGunState
{
    public PlayerController player;
    public void Enter(PlayerController _player)
    {
        Debug.Log("Holding Nothing");
        player = _player;
        player.gun.SetActive(false);
        player.deradiator.SetActive(false);
    }
    public void Exit()
    {

    }
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.SetState(new GunState());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.SetState(new DeradiatorState());
        }
    }
    public void Update()
    {
        
    }
}
