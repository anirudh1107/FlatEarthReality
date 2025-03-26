using UnityEngine;

public class RoamerNormalState : IRoamerState
{
    public void EnterState(RoamerBase roamer)
    {
        // (Optional) Initialize normal state
    }

    public void UpdateState(RoamerBase roamer)
    {
        if (roamer.waypoints == null || roamer.waypoints.Length == 0)
            return;

        Transform target = roamer.waypoints[roamer.currentWaypointIndex];
        roamer.transform.position = Vector3.MoveTowards(roamer.transform.position,
            target.position, roamer.speed * Time.deltaTime);

        // If the roamer reaches the waypoint (within a tolerance)
        if (Vector2.Distance(roamer.transform.position, target.position) < 0.1f)
        {
            // Record the last checkpoint
            roamer.lastCheckpointIndex = roamer.currentWaypointIndex;
            // Advance to the next waypoint (looping back if at end)
            roamer.currentWaypointIndex = (roamer.currentWaypointIndex + 1) % roamer.waypoints.Length;
        }
    }

    public void ExitState(RoamerBase roamer)
    {
        // (Optional) Cleanup for normal state
    }
}
