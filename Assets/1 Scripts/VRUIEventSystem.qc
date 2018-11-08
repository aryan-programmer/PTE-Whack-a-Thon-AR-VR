<QC>
Using UnityEngine

<Namespace> Utilities.VRUI
	[RequireComponent( TypeOf( UnityEngine.EventSystems.EventSystem ) , TypeOf( GvrPointerInputModule ) )] _
	Public <Class> VRUIEventSystem : MonoBehaviour
		<Function> Update( )
			<If> (GvrPointerInputModule.Pointer.TriggerDown || Input.GetKeyDown( KeyCode.X )) && _
				Physics.Raycast( Camera.main.transform.position , Camera.main.transform.forward , Out RaycastHit hit )
					hit.collider.gameObject.GetComponent[[VRUIButton]]()?.OnButtonClicked?.Invoke()
			</>
		</>
	</>
</>
</>