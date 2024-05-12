using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHud : MonoBehaviour
{
    // Public variables for bullet images
    public Image[] bulletImages;

    // Ammo variable (max bullets 10)
    public int maxAmmo = 10;
    public int currentAmmo = 0;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        // Update the UI based on current ammo
        UpdateAmmoUI();
    }

    // Method to update the visibility of bullet images based on current ammo
    void UpdateAmmoUI()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            // If the current index is less than the current ammo, show the bullet image
            if (i < currentAmmo)
            {
                bulletImages[i].enabled = true;
            }
            // Otherwise, hide the bullet image
            else
            {
                bulletImages[i].enabled = false;
            }
        }
    }

    // Method to set the current ammo
    public void SetAmmo()
    {
        currentAmmo--;
        // Update the UI to reflect the new ammo count
        UpdateAmmoUI();
    }


    // Public method to get the current ammo
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
