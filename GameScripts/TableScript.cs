using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableScript : MonoBehaviour
{

    GameObject character;
    CharacterController characterController;

    public GameObject moneyGo;

    GameObject moneyIcon;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("MainChar");
        characterController = character.GetComponent<CharacterController>();
        moneyIcon = GameObject.Find("Money3Dpos");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            StartCoroutine(timerss());
            GameObject fakeFood = Instantiate(transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel], transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel].transform.position, Quaternion.identity);
            fakeFood.SetActive(true);
            fakeFood.transform.parent = transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel].transform.parent.transform;
            fakeFood.transform.localScale = transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel].transform.localScale;
            fakeFood.transform.DOPunchScale(new Vector3(fakeFood.transform.localScale.x/10, fakeFood.transform.localScale.x/10, fakeFood.transform.localScale.x / 10), 1, 4);
            Destroy(fakeFood, 2.5f);
        }

        IEnumerator timerss()
        {
            GameObject money1 = Instantiate(moneyGo, transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel].transform.position, Quaternion.identity);
            money1.transform.DOMove(moneyIcon.transform.position, 1);
            Destroy(money1, .95f);
            yield return new WaitForSeconds(.1f);
            GameObject money2 = Instantiate(moneyGo, transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel].transform.position, Quaternion.identity);
            money2.transform.DOMove(moneyIcon.transform.position, 1);
            Destroy(money2, .95f);
            yield return new WaitForSeconds(.1f);
            GameObject money3 = Instantiate(moneyGo, transform.parent.gameObject.GetComponent<NpcController>().fakeFood[characterController.foodLevel].transform.position, Quaternion.identity);
            money3.transform.DOMove(moneyIcon.transform.position, 1);
            Destroy(money3, .95f);
        }
    }
}