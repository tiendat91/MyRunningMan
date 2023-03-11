using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private Transform holder;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private GameObject gunDrop;

    /// <summary>
    /// Reference of the Player owner of the Gun
    /// </summary>
    public HeroController PlayerController { get; set; }

    private Gun _gunEquipped;

    private void Start()
    {
        PlayerController = GetComponent<HeroController>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Reload();
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            DropGun();
        }
    }

    /// <summary>
    /// Shoots Projectiles
    /// </summary>
    private void Shoot()
    {
        if (_gunEquipped != null)
        {
            _gunEquipped.Shoot();
            shootSound.Play();
        }
    }

    /// <summary>
    /// Reloads this Gun
    /// </summary>
    private void Reload()
    {
        if (_gunEquipped != null)
        {
            _gunEquipped.Reload(false);
        }
    }

    /// <summary>
    /// Equipp a Gun
    /// </summary>
    /// <param name="newGun"></param>
    public void EquippGun(Gun newGun)
    {
        if (_gunEquipped == null)
        {

            _gunEquipped = Instantiate(newGun, holder.position, Quaternion.identity);

            _gunEquipped.GunController = this;
            _gunEquipped.transform.SetParent(holder);
            GameObject.Find("Gun(Clone)").transform.localScale = Vector3.one;
            if (GameObject.Find("GunCollectable") != null)
            {
                Destroy(GameObject.Find("GunCollectable"));
            }
            else
            {
                Destroy(GameObject.Find("GunCollectable(Clone)"));
            }

        }
    }
    public void DropGun()
    {
        if (_gunEquipped != null)
        {

            Vector3 rightDirection = new Vector3(-1, 1, 1);
            if (GameObject.Find("Player(Clone)").transform.localScale == rightDirection)
            {
                Instantiate(gunDrop, new Vector3(GameObject.Find("Player(Clone)").transform.position.x - 2f, GameObject.Find("Player(Clone)").transform.position.y, 0), Quaternion.identity);
                Vector3 vector = GameObject.Find("GunCollectable(Clone)").transform.localScale;
                vector.x *= -1;
                GameObject.Find("GunCollectable(Clone)").transform.localScale = vector;
            }
            else
            {
                Instantiate(gunDrop, new Vector3(GameObject.Find("Player(Clone)").transform.position.x + 2f, GameObject.Find("Player(Clone)").transform.position.y, 0), Quaternion.identity);
            }
            _gunEquipped.transform.SetParent(null);
            _gunEquipped = null;
            Destroy(GameObject.Find("Gun(Clone)"));
            Destroy(GameObject.Find("Pooler: All_Fire_Bullet_Pixel_16x16_277"));

        }
    }

}
