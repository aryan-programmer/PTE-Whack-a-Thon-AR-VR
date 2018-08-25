using UnityEngine;

namespace Utilities.VRUI
{
	[RequireComponent( typeof( UnityEngine.EventSystems.EventTrigger ) )]
	public class VRUIButton : MonoBehaviour
	{
		public UnityEngine.Events.UnityEvent OnButtonClicked;
		public void TriggerAllEvents( ) => OnButtonClicked.Invoke();
	}
}
