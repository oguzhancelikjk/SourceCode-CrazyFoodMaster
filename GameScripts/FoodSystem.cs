using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSystem : MonoBehaviour
{

    GameObject character;
    CharacterController characterController;

    public GameObject spPartic;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("MainChar");
        characterController= character.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x + 50 * Time.deltaTime, transform.rotation.y + 100 * Time.deltaTime, transform.rotation.z - 120 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Npc"))
        {
            if (characterController.money>=100)
            {
                characterController.money -= 100;
                PlayerPrefs.SetInt("money", characterController.money);
                characterController.moneyText.text = characterController.money.ToString();
            }
            other.gameObject.GetComponent<Animator>().Play("Reaction");

            GameObject splash = Instantiate(spPartic, other.transform.position, Quaternion.identity);
            Destroy(splash, 1);
            Destroy(gameObject);
        }
        if (other.CompareTag("Table"))
        {
            characterController.money += 500;
            PlayerPrefs.SetInt("money", characterController.money);
            characterController.moneyText.text = characterController.money.ToString();
            Destroy(gameObject);
        }
    }
}
