using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.VRUI
{
    [RequireComponent ( typeof ( UnityEngine.EventSystems.EventTrigger ) )]
    public class VRUIButton : MonoBehaviour
    {
        public UnityEvent OnButtonClicked;
        public void TriggerAllEvents ()
        {
            OnButtonClicked.Invoke ();
        }
    }
}
