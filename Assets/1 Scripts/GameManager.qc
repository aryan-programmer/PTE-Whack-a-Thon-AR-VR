<QC>
Using UnityEngine
Using Utilities.Extensions
Using Utilities
Using Utilities.Timers

Public <Class> GameManager : Singleton[[GameManager]]
	<Enum> ScreenType
		Start, Play, Pause, GameOver, Help _
	</>

#pragma warning disable 0649 _
	[SerializeField] GameObject moleContainer, fullArcadeGameObject
	[SerializeField] RandomizedIncreaseingTimer.Initializer initializer
	[Header( "Gui Elements" )] _
	[SerializeField] TextMesh ScoreText
	[SerializeField] TextMesh ScoreEndText, HighscoreText, NewHighscoreTextDisplay
	[SerializeField] TextMesh[] colorfulInfoTexts
	[SerializeField] GameObject StartScreen, PlayScreen, PauseScreen, GameOverScreen, HelpScreen
#pragma warning restore 0649 _

	Int score
	Mole[] moles
	Color[] targetColors
	GameObject[] screens
	Timer colorChangeTimer
	RandomizedIncreaseingTimer spawnTimer
	Bool GameIsRunning

	<Function> Start( )
		fullArcadeGameObject.SetActive( !Application.isEditor )
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal )
		screens = New GameObject[] { StartScreen , PlayScreen , PauseScreen , GameOverScreen , HelpScreen }
		moles = moleContainer.GetComponentsInChildren[[Mole]]()
		GameIsRunning = False
		SetEnabledScreen( ScreenType.Start )
		targetColors = New Color[ colorfulInfoTexts.Length ]
		colorChangeTimer = New Timer( 1f , ChangeColor )
		spawnTimer = New RandomizedIncreaseingTimer( initializer , Spawn )
	</>

	Public <Function> StartGame( )
		GameIsRunning = True
		SetEnabledScreen( ScreenType.Play )
		ScoreText.text = $`SCORE: {score}`
		HighscoreText.text = $`Highscore: {PlayerPrefsManager.Highscore}`
		moles.GetRandomElement().Rise()
	</>

	<Function> Update( )
		<If> GameIsRunning
			<If> score Less 0 
				score = 0
			</>
			spawnTimer.OnUpdate()
			ScoreText.text = $`SCORE: {score}`
		</>

		colorChangeTimer.OnUpdate()

		<For> Variable i = 0 To colorfulInfoTexts.Length - 1
			colorfulInfoTexts[ i ].color = Color.Lerp( colorfulInfoTexts[ i ].color , targetColors[ i ] , Time.deltaTime * 1.5f )
		</Next>
	</>

	<Function> Spawn( )
		Auto rng = Random.Range( 1 , 4 )
		<For> Variable i = 1 To rng
			moles.GetRandomElement().Rise()
		</Next>
	</>

	Public <Function> Pause( )
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal )
		SetEnabledScreen( ScreenType.Pause )
		GameIsRunning = False
	</>

	Public <Function> Resume( )
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal )
		SetEnabledScreen( ScreenType.Play )
		GameIsRunning = True
	</>

	Public <Function> ShowHelpScreen( )
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal )
		SetEnabledScreen( ScreenType.Help )
		GameIsRunning = False
	</>

	Public <Function> ResetHighscore( )
		PlayerPrefsManager.ResetHighscore()
		HighscoreText.text = $`Highscore: {PlayerPrefsManager.Highscore}`
	</>

	Public <Function> PlayAgain( )
		SceneManagemant.LoadLevel()
	</>

	Public <Function> Quit( )
		SceneManagemant.Quit()
	</>

	Public <Function> GUIGameOver( )
		GameIsRunning = false
		SetEnabledScreen( ScreenType.GameOver )
		ScoreEndText.text = $`Game Over! You got\n{score} points!!`
		Auto isNewHighScore = PlayerPrefsManager.IsNewHighscore( score )
		NewHighscoreTextDisplay.gameObject.SetActive( isNewHighScore )
		<If> isNewHighScore
			CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Win )
		</>
		<Else> 
			CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Lose )
		</>
	</>

	<Function> ChangeColor( )
		<For> Variable i = 0 To targetColors.Length - 1
			targetColors[ i ] = Random.ColorHSV( hueMin: 0 , hueMax: 1 , saturationMin: 0.65f , saturationMax: 1 , valueMin: 0.75f , valueMax: 1 )
		</Next>
	</>

	Internal <Function> IncreaseScore( int points )
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Score )
		score += points
	</>

	Internal <Function> DecreaseScore( int points )
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.AntiScore )
		score -= points
	</>

	<Function> SetEnabledScreen( ScreenType screenType )
		<ForEach> Auto screen In screens
			screen.SetActive( False )
		</Next>
		screens[ screenType.GetHashCode() ].SetActive( True )
	</>
</>
</>