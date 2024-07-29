using UnityEngine;

public class PlayerFollower : State
{
    [SerializeField] private TriggerZone _triggerZone;
    [SerializeField] private float _speed = 5f;

    private Vector3 _target;

    private void OnEnable()
    {
        _triggerZone.Follow += Following;
    }

    private void OnDisable()
    {
        _triggerZone.Follow -= Following;

        _target = Vector2.zero;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.fixedDeltaTime);
    }

    private void Following(Player player)
    {
        _target = player.transform.position;
    }
}