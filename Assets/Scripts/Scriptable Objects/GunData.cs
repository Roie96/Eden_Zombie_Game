using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Gun", menuName="Weapons/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    public bool autoShoot;

    [Header("Reloading")]
    public int currAmmo;

    public int currMagAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;

}
