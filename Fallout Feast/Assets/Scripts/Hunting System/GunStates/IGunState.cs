public interface IGunState
{
    void Enter(PlayerController player);
    void Update();
    void Exit();
    void HandleInput();

}