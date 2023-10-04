using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public int maxHP { get; set; }
    [HideInInspector] public int maxAmmo { get; set; }

    [Header("References")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Image armor;
    [SerializeField] private Sprite[] armorSprite;

    public void UpdateHP(int hpValue)
    {
        hpText.text = hpValue.ToString() + "/" + maxHP;
        hpSlider.value = hpValue / 3;
    }

    public void UpdateArmor(int armorValue)
    {
        armor.sprite = armorSprite[armorValue];
    }

    public void UpdateBulletCount(int bulletValue)
    {
        ammoText.text = bulletValue.ToString() + "/" + maxAmmo;
    }
}
