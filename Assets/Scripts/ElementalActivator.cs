using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalActivator : MonoBehaviour
{    
    public float damageToCharater;
    public float amountToFill;
    public float timeToFill;
    public bool filled;
    public int layerToGetActivated;
    public GameObject ObjToActivate;
    public GameObject uI;
    public PlayersHabilities currentPlayer;
    public bool notActivates;
    float timer;
    public float startSize;
    bool canBeFilled;
    AudioSource aS;

    private void Start()
    {
        aS = GetComponent <AudioSource>();
        startSize = uI.transform.localScale.y;
        amountToFill = startSize;
    }


    void Update()
    {

        if (0 > amountToFill)
        {
            filled = true;
            amountToFill = 1;
            canBeFilled = false;
        }

        if (canBeFilled)
        {
            Fill();
        }
        else UnFill();


        uI.transform.localScale = new Vector3(uI.transform.localScale.x, (startSize * (amountToFill/startSize))-2f, uI.transform.localScale.z);

        if (filled && !notActivates)
        {
            ObjToActivate.SetActive(true);
        }
        else ObjToActivate.SetActive(false);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == layerToGetActivated)
        {
            canBeFilled = true;
            currentPlayer = collision.gameObject.GetComponent<OxygenPlayerHabilities>();
        }
        else currentPlayer = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeFilled = false;
        currentPlayer = null;
    }

    private void Fill()
    {
	    if (currentPlayer != null)
	    {
            if (currentPlayer.gameObject.layer == layerToGetActivated)
            {            
                if (Input.GetButton(currentPlayer.useButton))
                { 
                    if (timer > timeToFill)
                    {
                        if (!aS.isPlaying)
                        {
                            aS.Play();
                        }
                        amountToFill-=0.5f/amountToFill;
                        currentPlayer.GetComponent<Damaggeable>().health -= damageToCharater;
                        timer = 0;
                    }
                    timer += Time.deltaTime;
                }          
            }
	    }
    }

    private void UnFill()
    {
        if (!filled)
        { 
            if (timer > timeToFill)
            {
                amountToFill += 0.1f / amountToFill;                
                timer = 0;
                if (amountToFill > startSize)
                {
                    amountToFill = startSize;
                }
            }
            timer += Time.deltaTime;
        }
    }
}
