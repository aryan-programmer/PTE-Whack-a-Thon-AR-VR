namespace Utilities.VRUI
{
	[UnityEngine.RequireComponent( typeof( UnityEngine.EventSystems.EventTrigger ) )]
	public
	class VRUIButton : UnityEngine.MonoBehaviour
	{
		public UnityEngine.Events.UnityEvent OnButtonClicked;

	}

}
