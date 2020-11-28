using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject ammoSlot;

    [SerializeField]
    PlayerShoot player;

    List<GameObject> ammoSlots = new List<GameObject> ();

    void start(){
        for(int i = 0; i<player.maxAmmo; i++)
        {
            var slot = Instantiate(ammoSlot);
            slot.transform.SetParent(this.transform);
            ammoSlots.Add(slot);
        }
    }

    void OnEnable(){
        player.OnAmmoChanged += HandleAmmoChanged;
    }

    void OnDisable(){
        player.OnAmmoChanged -= HandleAmmoChanged;
    }

    void HandleAmmoChanged (int ammo){
        for(int i = 0; i<ammoSlots.Count; i++)
        {
            var canvasGroup= ammoSlots[i].GetComponent<CanvasGroup>();

            canvasGroup.alpha = i < ammo ? 1 : 0.1f;
        }

    }
}
