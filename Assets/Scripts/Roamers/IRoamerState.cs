public interface IRoamerState
{
    void EnterState(RoamerBase roamer);
    void UpdateState(RoamerBase roamer);
    void ExitState(RoamerBase roamer);
}
