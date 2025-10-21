using UnityEngine;

public class DeradiatorState : IGunState
{
    public PlayerController player;
    public void Enter(PlayerController _player)
    {
        Debug.Log("Holding Deradiator");
        player = _player;
        player.deradiator.SetActive(true);
    }
    public void Exit()
    {
        player.deradiationParticles.Clear();
        player.deradiationParticles.Stop();
        player.deradiator.SetActive(false);
        
    }
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.SetState(new GunState());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.SetState(new NoneState());
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.deradiationParticles.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            player.deradiationParticles.Stop();
        }
    }
}
