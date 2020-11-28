using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour {
    public delegate void  AmmoEvent (int ammo);
    public delegate void ReloadEvent(bool isRealoading);
    public AmmoEvent OnAmmoChanged;
    public ReloadEvent OnReloadChanged;


    [SerializeField]
    Transform firePoint;

    [SerializeField]
    ObjectPool pool;

    public float bulletForce = 20f;

    [SerializeField]
    [Range (0, 0.5f)]
    float rate = 0;
    float _rateAcc = 0;
    bool _canShoot = true;
    bool _isShooting = false;
    bool _isReloading = false;

    [SerializeField]
    public int maxAmmo;

    private int _ammo;

    int ammo {
        get => _ammo;
        set{
            if (value != _ammo) {
                _ammo = value;
                OnAmmoChanged?.Invoke(ammo);
            }
        }
    }


    void Start(){
        ammo = maxAmmo;
    }

    public void Update () {
        if (!_canShoot) {
            _rateAcc = Mathf.Max (0, _rateAcc - rate);
        }
        if (_rateAcc == 0) {
            _canShoot = true;
            _rateAcc = 1;
        }

        if (_isShooting) {
            Shoot ();
        }
    }

    public void HandleInteract (InputAction.CallbackContext context) {
        _isShooting = context.ReadValue<float> () == 1;
    }

    IEnumerator Reload(){
        var wait = new WaitForSeconds(0.3f);
        _isReloading = true;
        OnReloadChanged?.Invoke(true);


        while(ammo < maxAmmo) {
            ammo += 1;
            yield return wait;
        }

        OnReloadChanged?.Invoke(false);
        _isReloading = false;

    }

    void Shoot () {
        if(!_canShoot) return;
        if(_isReloading) return;
        if(ammo == 0 ) {
            StartCoroutine(Reload());
            return;
        }
            var bullet = pool.GetObject();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.identity;
            var rb = bullet.GetComponent<Rigidbody> ();
            rb.AddForce (firePoint.forward * bulletForce, ForceMode.Impulse);
            _canShoot = false;
            ammo -= 1;
    }
}