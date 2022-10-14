using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool canTouch = false;

    public BoxCollider2D collis;
    public SpriteRenderer spriteRenderer;
    public string weaponType;
    public float weaponMultiplierSpeed = 1;
    public float weaponCamMultiplier = 1;
    public float weaponAmmo;
    public float weaponDamage;
    public float weaponSpeed;
    public float weaponDelay;
    public float weaponSpread;
    public float weaponForce;
    public GameObject playerObject;
    public Player player;


    private void Start()
    {
        player = playerObject.GetComponent<Player>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       canTouch = false;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        canTouch = true;
    }
    private void Update()
    {
        if(canTouch)
        {
            if (Input.GetKeyDown(KeyCode.E))
                if (!player.weaponInHands)
                {
                    GiveWeapon();
                    player.canShoot = true;
                }
        }
    }
    public void GiveWeapon()
    {
        player.weaponDamage = weaponDamage;
        player.weaponSpeed = weaponSpeed;
        player.weaponDelay = weaponDelay;
        player.weaponSpread = weaponSpread;
        player.weaponForce = weaponForce;
        player.weaponAmmo = weaponAmmo;
        player.weaponType = weaponType;
        player.weaponMultiplierSpeed = weaponMultiplierSpeed;
        player.weaponCamMultiplier = weaponCamMultiplier;

        

        player.weapon = gameObject;
        player.weaponInHands = true;
        collis.enabled = false;
        gameObject.transform.rotation = playerObject.transform.rotation;
        gameObject.transform.SetParent(playerObject.transform);
        player.TeleportWeapon();
        
        

        //spriteRenderer.enabled = false;
    }
    public void Drop()
    {
        player.weaponDamage = 0;
        player.weaponSpeed = 0;
        player.weaponDelay = 0;



        collis.enabled = true;
        gameObject.transform.SetParent(null);
    }
}
