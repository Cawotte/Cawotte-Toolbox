using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using System;
using UnityEngine.Events;

namespace Cawotte.Toolbox
{
    public class MouseEvents : MonoBehaviour
    {
        public UnityEvent OnMouseEnterEvent = null;
        public UnityEvent OnMonseExitEvent = null;
        public UnityEvent OnMouseClickEvent = null;

        private void OnMouseEnter()
        {
            OnMouseEnterEvent?.Invoke();
        }

        private void OnMouseExit()
        {
            OnMonseExitEvent?.Invoke();
        }
        private void OnMouseDown()
        {
            OnMouseClickEvent?.Invoke();
        }

        // OnMouseDrag = Click + Move over collider
        // OnMouseOver = Called every frame is mouse is over
        // OnMouseUp   = Called when mouse click is released, even outside the button
        // OnMouseUpAsButton = Called when mouse click is release and still on collider
    }

}
