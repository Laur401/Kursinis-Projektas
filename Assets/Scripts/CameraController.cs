using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        // transform.position - nera nurodyto obj., scriptas prikabintas ant kameros obj. 
        // player - public, padarysim nuoroda i kamuoliuka
        // apskaiciuojamas vekt.
    }

    // Update is called once per frame
    void LateUpdate() // late - standart metodas (pns i start/update), vykdomas po Update
    {
        transform.position = player.transform.position + offset;
        //nauja padetis = nauja kamuoliuko/zaidejo padetis + pradinis vekt. (atstumas nuo kameros iki kamuoliuko)
    }
}
