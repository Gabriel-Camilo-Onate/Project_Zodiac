using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public enum itemTypes { Health, Shield, Buff, MaxHPPotion};
    Dictionary<string, int> dicItems = new Dictionary<string, int>();
    [SerializeField]
    int _maxLifeToAdd, _lifeToAdd, _invulTime, _bufferSeconds;
    [SerializeField]
    private PlayerControler playerController;
    [SerializeField]
    private PlayerFunctions PlayerFunctions;
    [SerializeField]
    private PlayerLiveObject PlayerLiveObject;
    [SerializeField]
    private GameObject InvOpen, InvClosed;
    [SerializeField]
    private Text[] texts;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioclip;
    void Start()
    {
        foreach (itemTypes item in itemTypes.GetValues(typeof(itemTypes)))
        {
            dicItems.Add(item.ToString(), 1);
            texts[(int)item].text = dicItems[((itemTypes)item).ToString()].ToString();
        }
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("No se pudo encontrar el componente AudioSource");
                return;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _audioSource.clip = _audioclip;
            _audioSource.Play();
            InvOpen.SetActive(!InvOpen.activeInHierarchy);
            InvClosed.SetActive(!InvClosed.activeInHierarchy);
            if (InvOpen.activeInHierarchy)
            {
                foreach (itemTypes item in itemTypes.GetValues(typeof(itemTypes)))
                {
                    texts[(int)item].text = dicItems[((itemTypes)item).ToString()].ToString();
                }
            }
        }
        UseItems();
    }


    public void ItemCounter(int itemtype)
    {
        dicItems[((itemTypes)itemtype).ToString()]++;
        if (InvOpen.activeInHierarchy)
        texts[(int)itemtype].text = dicItems[((itemTypes)itemtype).ToString()].ToString();

    }

    private void UseItems()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(dicItems["Health"] > 0)
            {
                PlayerLiveObject.AddLife(_lifeToAdd);
                dicItems["Health"]--;
                texts[0].text = dicItems["Health"].ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (dicItems["Shield"] > 0)
            {
                PlayerLiveObject.Invulnerability(_invulTime);
                dicItems["Shield"]--;
                texts[1].text = dicItems["Shield"].ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (dicItems["Buff"] > 0)
            {
                PlayerLiveObject.BufferAttack(_bufferSeconds);
                dicItems["Buff"]--;
                texts[2].text = dicItems["Buff"].ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (dicItems["MaxHPPotion"] > 0)
            {
                PlayerLiveObject.AddMaxLife(_maxLifeToAdd);
                dicItems["MaxHPPotion"]--;
                texts[3].text = dicItems["MaxHPPotion"].ToString();
            }
        }

    }

}
