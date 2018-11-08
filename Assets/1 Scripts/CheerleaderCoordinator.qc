<QC>
Using System.Collections
Using UnityEngine
Using Utilities.Extensions

Public <Class> CheerleaderCoordinator : Singleton[[CheerleaderCoordinator]]
#pragma warning disable 0649 _
	[SerializeField] New RuntimeAnimatorController animation
#pragma warning restore 0649 _

	QuerySDMecanimController[] __SQuerySDMecanimControllers

	Public <Property> SQuerySDMecanimControllers Of QuerySDMecanimController[]
		<Get>
			Give __SQuerySDMecanimControllers ?? (__SQuerySDMecanimControllers = GetComponentsInChildren[[QuerySDMecanimController]]( True ))
		</>
	</>

	<Function> Start( )
		<ForEach> Auto cheerleaderP In SQuerySDMecanimControllers
			Animator animator = cheerleaderP.GetComponentInChildren[[Animator]]()
			animator.applyRootMotion = False
			animator.runtimeAnimatorController = animation
		</Next>

		ChooseRandomCheerleader()
	</>

	Public <Function> ChooseRandomCheerleader( )
		<ForEach> Auto cheerleaderP In SQuerySDMecanimControllers
			cheerleaderP.gameObject.SetActive( False )
		</Next>

		QuerySDMecanimController cheerleader1 = SQuerySDMecanimControllers.GetRandomElement()
		QuerySDMecanimController cheerleader2 = SQuerySDMecanimControllers.GetRandomElement()

		<While> cheerleader1.gameObject.name == cheerleader2.gameObject.name
			cheerleader2 = SQuerySDMecanimControllers.GetRandomElement()
		</Loop>

		Bool cheerleader1IsOnPositivePosition = Random.Range( 0 , 2 ) == 0
		<If> cheerleader1IsOnPositivePosition
			cheerleader1.gameObject.transform.localPosition = New Vector3( 2 , 0 , 0 )
			cheerleader2.gameObject.transform.localPosition = New Vector3( -2 , 0 , 0 )
		</>
		<Else>
			cheerleader1.gameObject.transform.localPosition = New Vector3( -2 , 0 , 0 )
			cheerleader2.gameObject.transform.localPosition = New Vector3( 2 , 0 , 0 )
		</>
		cheerleader1.gameObject.SetActive( True )
		cheerleader2.gameObject.SetActive( True )
		ChangeAnimation( CheerState.Normal )
	</>

	Public <Enum> CheerState
		Normal, _
		Score, _
		AntiScore, _
		Lose, _
		Win _
	</>

	Public Static <Function> ChangeAnimation( CheerState state ) 
		I.StartCoroutine( I.ChangeAnimIEnumerator( state ) )
	</>

	<Function> ChangeAnimIEnumerator( CheerState state ) Gives IEnumerator
		<Switch> state
		<FTCase> CheerState.Score 
		</> <Case> CheerState.AntiScore
			ChangeAnim( state )
			AlsoGive New WaitForSeconds( 2.5f )
			ChangeAnim( CheerState.Normal )
		</>
		<FTCase> CheerState.Normal
		</> <FTCase> CheerState.Win
		</> <Case> CheerState.Lose
			ChangeAnim( state )
		</>
		<Default>
		
		</>
		</>
		AlsoGive Null
	</>
	
	<Function> ChangeAnim( CheerState state )
		Int stateOfMotion = Random.Range( 0 , 3 )
		<ForEach> Auto mechanimController In SQuerySDMecanimControllers
			<Switch> state
			<Case> CheerState.Normal
				mechanimController.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_IDLE )
			</>
			<Case> CheerState.Score
				mechanimController. _
					ChangeAnimation( _
					REM if _
					stateOfMotion == 1 ? _
					REM then _
					QuerySDMecanimController. _
					QueryChanSDAnimationType.NORMAL_ITEMGET : _
					REM else _
					QuerySDMecanimController. _
					QueryChanSDAnimationType.OSAKA_CLAP )
			</>
			<Case> CheerState.AntiScore
				mechanimController.ChangeAnimation( QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_DAMAGE )
			</>
			<Case> CheerState.Lose
				mechanimController.ChangeAnimation( QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_LOSE )
			</>
			<Case> CheerState.Win
				mechanimController.ChangeAnimation( QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_WIN )
			</>
			<Default>
			</>
			</>
		</Next>
	</>
</>
</>