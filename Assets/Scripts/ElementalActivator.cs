using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalActivator : MonoBehaviour
{
    public float amountToFill;
    public float amount = 0;
    public float timeToFill;
    public bool filled;
    public int layerToGetActivated;
    public GameObject ObjToActivate;
    public GameObject uI;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if (amount > amountToFill)
        {
            filled = true;
            amount = 100;
        }
        uI.transform.localScale = new Vector3(uI.transform.localScale.x, amount / amountToFill, uI.transform.localScale.z);

        if (ObjToActivate.activeSelf == false && filled)
        {
            ObjToActivate.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToGetActivated)
        {
            if (collision.gameObject.GetComponent<Oxygen>())
            {
                if (Input.GetButton(collision.gameObject.GetComponent<PlayersHabilities>().useButton))
                { 
                    if (timer > timeToFill)
                    {
                        amount += 1;
                        timer = 0;
                    }
                    timer += Time.deltaTime;
                }
                
            }
           
        }
    }
}
