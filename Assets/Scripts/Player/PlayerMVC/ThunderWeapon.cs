using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ThunderWeapon : BaseWeapon
{
    [SerializeField] private Transform _shootPosition;
    private bool _isChaneling;
    private float _chanelThunderAnimationTime = 11f;
    [SerializeField] private ThunderTorrent _thunderTorrent;
    [SerializeField] private ThunderTorrent _thunderInstance;
    [SerializeField] private ParticleSystem[] _psattack2;
    [SerializeField] private float _lightningAttackRange;
    [SerializeField] private float _ThunderTorrentBasicCost;
    [SerializeField] private float _ThunderTorrentBasicDamage;
    [SerializeField] private Transform _atractor;
    public Action<bool> chanelingSpell;

    public override void StartAction1()
    {
        ShootThunderTorrent(_shootPosition.position);
    }
    public override void MantainAction1()
    {
        ShootThunderTorrent(_shootPosition.position);
    }
    public override void ExitAction1()
    {
        Shoot2Exit();
    }
    public void UseThunderTorrent()
    {
        if (player.mana.CurrentValue < _ThunderTorrentBasicCost * manaCostModifier)
        {
            FinishThunderTorrent();
            return;
        }
        player.mana.SubstractAmount(_ThunderTorrentBasicCost * Time.deltaTime);
        chanelingSpell?.Invoke(true);
        player.animator.SetFloat(player.attackSpeedMultiplier, attacksPerSecond);
        if (_isChaneling)
        {
            ShootThunderTorrent(shootPosition.position);
        }
        else
        {
            StartCoroutine(CastThunder());
        }
    }
    private IEnumerator CastThunder()
    {
        if (!_isChaneling)
        {
            yield return 60 / _chanelThunderAnimationTime;
            _isChaneling = true;
        }

    }
    public void FinishThunderTorrent()
    {
        Shoot2Exit();
    }
    public void ShootThunderTorrent(Vector3 origin)
    {
        OnLightning();
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (_thunderInstance == null)
        {
            _thunderInstance = Instantiate(_thunderTorrent, origin, Quaternion.identity);
            _thunderInstance.transform.SetParent(null);
        }
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, player.spellBounds))
        {
            foreach (var item in player.showPoint)
            {
                _atractor.position= raycastHit.point;
            }
        }
        else
        {
            foreach (var item in player.showPoint)
            {
                _atractor.position= player.ray.direction.normalized * _lightningAttackRange;
            }
        }
        _thunderInstance.Use(player.showPoint[0].position, 1 / attacksPerSecond, damage + _ThunderTorrentBasicDamage);
    }
    public void OnLightning()
    {
        foreach (var item in _psattack2)
        {
            item.Play();
        }
    }
    private void Shoot2Exit()
    {
        chanelingSpell?.Invoke(false);
        _isChaneling = false;
        foreach (var item in _psattack2)
        {
            item.Stop();
            if (_thunderInstance != null)
            {
                Destroy(_thunderInstance.gameObject);
            }
        }
    }
}
