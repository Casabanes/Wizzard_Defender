using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Collider _collider;

    [SerializeField] private float speed;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _damage;
    [SerializeField] private int[] _damageLayers;
    [SerializeField] private ParticleSystem[] _ps;
    [SerializeField] private ParticleSystem _endParticleSystem;
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _launchSound;
    [SerializeField] private AudioClip _impactSound;
    [SerializeField] private Vector3 _direction;

    private float timeToDestroyAffterImpact=2f;
    private LiveEntity _target;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            _rigidBody=gameObject.AddComponent<Rigidbody>();
            Debug.Log("The rigid body component was added automaticaly");
        }
        _movement = new Movement().SetSpeed(speed).SetRigidBody(_rigidBody).SetTransform(transform);
        AbleAndDisable(true);
    }
    public FireBall SetDirection(Vector3 direction)
    {

        _direction = direction.normalized;
        transform.forward = _direction;
      
        return this;
    }
    public FireBall SetDamage(float damage)
    {
        _damage = damage;
        return this;
    }
    public FireBall SetPosition(Vector3 position)
    {
        transform.position = position;
        return this;
    }
    void Update()
    {
        _movement.Move(_direction.x, _direction.y, _direction.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < _damageLayers.Length; i++)
        {
            if(other.gameObject.layer == _damageLayers[i])
            {
                _target = other.GetComponent<LiveEntity>();
                if (_target != null)
                {
                    Impact(other.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
                }
            }
        }
    }
    private void Impact(Vector3 impactPoint)
    {
        _audiosource.Stop();
        _audiosource.clip = _impactSound;
        _audiosource.Play();
        _target.TakeDamage(_damage);
        AbleAndDisable(false);
        Instantiate(_endParticleSystem, impactPoint,transform.rotation);
    }
    private void AbleAndDisable(bool value)
    {
        _collider.enabled = value;
        _rigidBody.isKinematic = !value;
        if (value)
        {
            _audiosource.clip = _launchSound;
            _audiosource.Play();
            foreach (var item in _ps)
            {
                item.Play();
            }
        }
        else
        {
            foreach (var item in _ps)
            {
                item.Stop();
                StartCoroutine(DestroySelf());
            }
        }
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToDestroyAffterImpact);
        Destroy(gameObject);
    }
}
