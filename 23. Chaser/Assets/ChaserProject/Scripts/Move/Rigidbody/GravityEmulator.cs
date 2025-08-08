using UnityEngine;

public class GravityEmulator
{
    private Vector3 _gravityForce;
    private float _gravitySquaredMagnitude;
    
    public GravityEmulator(Vector3 gravityForce, float gravityScale)
    {
        _gravityForce = gravityForce * gravityScale;
        _gravitySquaredMagnitude = _gravityForce.sqrMagnitude;
        
        Down = gravityForce.normalized;
        Up = -Down;
    }
    
    public Vector3 Up { get; }
    public Vector3 Down { get; }
    public Vector3 FixedForceDelta => _gravityForce * Time.fixedDeltaTime;
    
    public Vector3 Affect(Vector3 currentVelocity, Vector3 fallVelocity, Vector3 newHorizontalVelocity)
    {
        Vector3 verticalVelocity = Up * Vector3.Dot(currentVelocity, Up);
        Vector3 newVerticalVelocity = verticalVelocity + fallVelocity;
        
        float gravityProjectionSquared = Vector3.Project(newVerticalVelocity, _gravityForce).sqrMagnitude;
        
        if (gravityProjectionSquared > _gravitySquaredMagnitude)
            newVerticalVelocity = _gravityForce;
        
        return newHorizontalVelocity + newVerticalVelocity;
    }
}
