using UnityEngine;

public class RoamerReverseState : IRoamerState
{
    private float elapsedTime;
    // Duration (in seconds) to reverse before returning to normal movement.
    private float reverseDuration = 2f;

    public void EnterState(RoamerBase roamer)
    {
        elapsedTime = 0f;
    }

    public void UpdateState(RoamerBase roamer)
    {
        if (roamer.waypoints == null || roamer.waypoints.Length == 0)
            return;

        // Move toward the last checkpoint.
        Transform checkpoint = roamer.waypoints[roamer.lastCheckpointIndex];
        roamer.transform.position = Vector3.MoveTowards(roamer.transform.position,
            checkpoint.position, roamer.speed * Time.deltaTime);

        elapsedTime += Time.deltaTime;
        // Switch back to normal state if reached the checkpoint or after reverseDuration.
        if (Vector3.Distance(roamer.transform.position, checkpoint.position) < 0.1f || elapsedTime >= reverseDuration)
        {
            roamer.SwitchState(new RoamerNormalState());
        }
    }

    public void ExitState(RoamerBase roamer)
    {
        // (Optional) Cleanup for reverse state
    }
}
