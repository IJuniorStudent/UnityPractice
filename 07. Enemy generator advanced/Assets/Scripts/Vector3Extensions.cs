using UnityEngine;

public static class Vector3Extensions
{
    public static Quaternion VerticalLookAt(this Vector3 self, Vector3 target)
    {
        Vector3 direction = target - self;
        direction.y = 0.0f;
        
        if (direction == Vector3.zero)
            return Quaternion.Euler(0, 0, 0);
        
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        
        return Quaternion.Euler(0, angle, 0);
    }
}
