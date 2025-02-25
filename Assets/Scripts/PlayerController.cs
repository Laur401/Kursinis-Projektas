using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // leidzia dirbti su UI elementais (metodais, parametrais)
using TMPro;

public class PlayerController : MonoBehaviour
{
	public float speed; // savas kintamasis, parametras - is mazosios
	private Rigidbody rb; // savas metodas - is didziosios (pvz komponentas RigidBody is didz)

    public TextMeshProUGUI countText;
    private int count; //skaiciuoja surinktus obj.
    public TextMeshProUGUI winText;

    public AudioSource as1; // public tam, kad galetume susiet su AudioSource obj.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // gaunamas bet koks komponentas
                                        // gale () - GetComponent yra metodas
                                        // kiekvieno sak. gale ;
        SetCountText(); // tai nestandartinis, o sukurtas metodas, isvedantis info i ekrana
        count = 0;
        winText.text = ""; // pradzioje uzraso buti neturi
    }

    // Update is called once per frame (for Update() at least)
    void FixedUpdate() //apdorojama ivestis is klaviaturos
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // x koord
		float moveVertical = Input.GetAxis("Vertical"); // z koord
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); 
		// suformuojamas krypties vektorius - parodoma, kuria kryptimi judės mūsų kamuoliukas
		//0.0f - f parodo float tipą (x,y,z reikšmėms)
		rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up")) // priskirti zyme norimiems rinkti obj., kamuoliui paemus pickup'a pridedamas vienetas
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            if (count == 12)
            {
                winText.text = "YOU WIN!";
                Time.timeScale = 0; // sustabdo zaidima (sita eilute naudojama ir pauzes meniu), Time (T is didz.)
                as1.Stop();
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString(); // count sk. pakeiciamas i tekstine eilute
    }
}
