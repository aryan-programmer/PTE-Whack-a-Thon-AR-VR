<QC>
Using UnityEngine

Public <Class> Player : Singleton[[Player]]
	<Function> Update( )
		<If> GvrPointerInputModule.Pointer.TriggerDown || Input.GetKeyDown( KeyCode.X )
			<If> Physics.Raycast( transform.position , transform.forward , Out RaycastHit hit )
				<If> hit.transform.GetComponent[[Mole]]() Is Mole mole
					<If> !mole.Hiding
						Hammer.I.Hit( mole.transform.position )
						mole.OnHit()
					</>
				</>
			</>
		</>
	</>
</>
</>