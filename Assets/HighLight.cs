using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(AudioSource))]
public class HighLight : MonoBehaviour,
                    IDragHandler, // para poder detectar cuando empiezo a hacer drag por encima de un objeto
                    IEndDragHandler, //para poder detectas cuando dejo de dragear un obejto
                    IPointerEnterHandler, // permitir detectar cuando el mouse comieza a estar por encima de un objeto
                    IPointerExitHandler, // permitir detectar cuando el mouse se va por encima del objeto
                    IPointerUpHandler, // permite detectar cuando suelto el boton del mouse por arriba del objeto
                    IPointerDownHandler, // permite cuando detectar cuando estamos pulsando click sobre el objeto.
                    IPointerClickHandler //permite detectar cuando hacemos click sobre el objeto
                 
                   
{
    public AudioSource aS;
    public AudioClip[] aC;

    public void Awake()
    {
        aS = GetComponent<AudioSource>();
    }


    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        aS.PlayOneShot(aC[1]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        aS.PlayOneShot(aC[0]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       
    }
    
}
