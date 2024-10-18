using UnityEngine;

public class Spring
{
    private float _strength;
    private float _damper;
    private float _target;
    private float _velocity;
    private float _value;
    public float Value => _value;

    // calulates the new value for the spring
    public void Update(float deltaTime)
    {
        float target = _target - _value;
        float direction = target >= 0 ? 1f : -1f;
        float force = Mathf.Abs(target) * _strength;
        _velocity += (force * direction - _velocity * _damper) * deltaTime;
        _value += _velocity * deltaTime;
    }

    // reset the velocity and value of the spring simulation
    public void Reset()
    {
        _velocity = 0f;
        _value = 0f;
    }

    // set the value of the spring simulation
    public void SetValue(float value)
    {
        _value = value;
    }

    // set the value of the spring simulation
    public void SetTarget(float target)
    {
        _target = target;
    }

    // set the damper of the spring simulation
    public void SetDamper(float damper)
    {
        _damper = damper;
    }

    // set the strength of the spring simulation
    public void SetStrength(float strength)
    {
        _strength = strength;
    }

    // set the velocity of the spring simulation
    public void SetVelocity(float velocity)
    {
        _velocity = velocity;
    }
}