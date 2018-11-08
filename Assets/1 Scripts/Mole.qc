<QC>
Using Utilities.Timers
Using HoloToolkit.MRDL.PeriodicTable
Using UnityEngine
Using Utilities.Extensions

Public <Class> Mole : MonoBehaviour
#pragma warning disable 0649 _
	[SerializeField] Float visibleHeight = 2, hiddenHeight = -0.3f, speed = 3, dissapperDuration = 1, probabiltyOfSpecialMole = 1, valueMultiplier = 1
	[SerializeField] Int MainMatIndex
	[SerializeField] Renderer rend
	[SerializeField] Material GoldenMaterial, BadMaterial, OrigMaterial
	[SerializeField] AudioClip hitClip
	[SerializeField] Element elementObj
#pragma warning restore 0649 _

	Bool isGolden, isEvil, hiding
	Vector3 targetPos
	Timer timer
	bool isTimerStarted = False

	Public <Property> Hiding Of Bool
		<Get>
			Give hiding
		</>
	</>

	Public <Property> SAudioSource Of AudioSource
		<Get>
			Give __audioSource ?? ( __audioSource = this.GetAdd[[AudioSource]]() )
		</>
	</>

	AudioSource __audioSource

	<Function> Start( )
		<If> rend == Null
			rend = Me.Get[[Renderer]]()
		</>
		timer = New Timer( 2 , Delegate ( ) _
		{ _
			elementObj.gameObject.SetActive( isTimerStarted = False )
		} )
		targetPos = transform.localPosition
		targetPos.y = hiddenHeight
		elementObj.gameObject.SetActive( False )
	</>

	<Function> Update( )
		transform.localPosition = Vector3.Lerp( transform.localPosition , targetPos , Time.deltaTime * speed )
		hiding = Mathf.FloorToInt( transform.localPosition.y ) == Mathf.FloorToInt( hiddenHeight )
		<If> isTimerStarted
			timer.OnUpdate()
		</>
	</>

	Public <Function> Rise( )
		<If> !hiding || elementObj.isActiveAndEnabled
			GiveIn
		</>

		<If> Random.value Less probabiltyOfSpecialMole
			<If> Random.Range( 0 , 3 ) == 1
				Auto mats = rend.materials
				mats[ MainMatIndex ] = GoldenMaterial
				rend.materials = mats
				isGolden = True
				isEvil = False
			</>
			<Else>
				Auto mats = rend.materials
				mats[ MainMatIndex ] = BadMaterial
				rend.materials = mats
				isGolden = False
				isEvil = True
			</>
		</>
		<Else>
			Auto mats = rend.materials
			mats[ MainMatIndex ] = OrigMaterial
			rend.materials = mats
			isGolden = False
			isEvil = False
		</>
		targetPos.y = visibleHeight
		Invoke( `Hide` , dissapperDuration )
	</>

	Public <Function> OnHit( )
		<If> !hiding
			SAudioSource.PlayOneShot( hitClip )
			<If> isGolden
				GameManager.I.IncreaseScore( Mathf.RoundToInt( 3 * valueMultiplier ) )
			</>
			<ElseIf> isEvil
				GameManager.I.DecreaseScore( Mathf.RoundToInt( 15 * valueMultiplier ) )
			</>
			<Else>
				GameManager.I.IncreaseScore( Mathf.RoundToInt( valueMultiplier ) )
			</>
			elementObj.gameObject.SetActive( True )
			ElementHandler.I.SetRandomElement( elementObj )
			isTimerStarted = True
			WindowsVoice.Speak( $`{elementObj.Data.name} {elementObj.Data.number}.` )
			Hide()
		</>
	</>

	Public <Function> Hide( )
		Auto mats = rend.materials
		mats[ MainMatIndex ] = OrigMaterial
		rend.materials = mats
		isGolden = False
		isEvil = False
		targetPos.y = hiddenHeight
	</>
</>
</>