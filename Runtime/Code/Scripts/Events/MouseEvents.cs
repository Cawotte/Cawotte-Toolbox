using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using System;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace Cawotte.Toolbox
{
    public class MouseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public UnityEvent OnMouseEnterEvent = null;
        public UnityEvent OnMouseExitEvent = null;
        public UnityEvent OnMouseClickEvent = null;

        private void OnMouseEnter()
        {
            OnMouseEnterEvent?.Invoke();
        }

        private void OnMouseExit()
        {
            OnMouseExitEvent?.Invoke();
        }

        private void OnMouseDown()
        {
            OnMouseClickEvent?.Invoke();
        }

        #region New Input System
        //Requires "Physics Raycaster" on Camera to work.

        public void OnPointerEnter( PointerEventData eventData )
        {
            OnMouseEnterEvent?.Invoke();
        }

        public void OnPointerExit( PointerEventData eventData )
        {
            OnMouseExitEvent?.Invoke();
        }
        public void OnPointerDown( PointerEventData eventData )
        {
            OnMouseClickEvent?.Invoke();
        }

        #endregion
        // OnMouseDrag = Click + Move over collider
        // OnMouseOver = Called every frame is mouse is over
        // OnMouseUp   = Called when mouse click is released, even outside the button
        // OnMouseUpAsButton = Called when mouse click is release and still on collider
    }

}
