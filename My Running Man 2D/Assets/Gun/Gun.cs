﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator modelAnimator;

    [Header("Gun Settings")]
    [SerializeField] private float msBetweenShots = 1000;

    [Header("Ammo")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;
    [SerializeField] private float reloadTime = 3f;

    /// <summary>
    /// Reference of the GunController 
    /// </summary>
    public GunController GunController { get; set; }

    private ObjectPooler _pooler;
    private float _nextShotTime;

    private float _reloadTimer;
    private bool _isReloading;
    private int _projectilesRamaining;

    private int _fireParameter = Animator.StringToHash("Fire");

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _projectilesRamaining = magazineSize;
    }

    private void Update()
    {
        if (autoReload)
        {
            Reload(true);
        }
    }

    /// <summary>
    /// Fires a projectile from the firePoint
    /// </summary>
    private void FireProjectile()
    {
        // Get Object from pool
        GameObject newProjectile = _pooler.GetObjectFromPool();
        newProjectile.transform.position = firePoint.position;
        newProjectile.SetActive(true);

        // Get projectile
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.GunEquipped = this;
        projectile.SetDirection((GunController.PlayerController.FacingRight ? Vector3.right : Vector3.left), firePoint.position);
        projectile.EnableProjectile();

        // Set animation
        modelAnimator.SetTrigger(_fireParameter);

    }

    /// <summary>
    /// Shoots our Gun
    /// </summary>
    public void Shoot()
    {
        if (Time.time > _nextShotTime && !_isReloading && _projectilesRamaining > 0)
        {
            _nextShotTime = Time.time + msBetweenShots / 1000f;
            FireProjectile();
            _projectilesRamaining--;


        }
    }

    /// <summary>
    /// Reloads this gun
    /// </summary>
    /// <param name="autoReload"></param>
    public void Reload(bool autoReload)
    {
        if (_projectilesRamaining > 0 && _projectilesRamaining <= magazineSize && !_isReloading && !autoReload)
        {
            StartCoroutine(IEWaitForReload());
        }

        if (_projectilesRamaining <= 0 && !_isReloading)
        {
            StartCoroutine(IEWaitForReload());
        }
    }

    /// <summary>
    /// Reload coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEWaitForReload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        _projectilesRamaining = magazineSize;
        _isReloading = false;
    }
}
