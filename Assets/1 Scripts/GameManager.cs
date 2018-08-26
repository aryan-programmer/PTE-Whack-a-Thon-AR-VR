using UnityEngine;
using Utilities.Extensions;
using Utilities;
using Utilities.Timers;

public class GameManager : Singleton<GameManager>
{
	enum ScreenType { Start, Play, Pause, GameOver, Help }

#pragma warning disable 0649
	[SerializeField] GameObject moleContainer, fullArcadeGameObject;
	[SerializeField] RandomizedIncreaseingTimer.Initializer initializer;
	[Header( "Gui Elements" )]
	[SerializeField] TextMesh ScoreText;
	[SerializeField]
	TextMesh ScoreEndText, HighscoreText, NewHighscoreTextDisplay;
	[SerializeField] TextMesh[] colorfulInfoTexts;
	[SerializeField]
	GameObject StartScreen, PlayScreen, PauseScreen, GameOverScreen, HelpScreen;
#pragma warning restore 0649

	int score;
	Mole[] moles;
	Color[] targetColors;
	GameObject[] screens;
	Timer colorChangeTimer;
	RandomizedIncreaseingTimer spawnTimer;
	bool GameIsRunning;

	// Use this for initialization
	void Start( )
	{
		fullArcadeGameObject.SetActive( !Application.isEditor );
		CheerleaderCoordinator.ChangeAnimation(
			CheerleaderCoordinator.CheerState.Normal );
		screens = new GameObject[] {
			StartScreen , PlayScreen ,
			PauseScreen , GameOverScreen , HelpScreen };
		moles = moleContainer.GetComponentsInChildren<Mole>();
		GameIsRunning = false;
		SetEnabledScreen( ScreenType.Start );
		targetColors = new Color[ colorfulInfoTexts.Length ];
		colorChangeTimer = new Timer( 1f , ChangeColor );
		spawnTimer = new RandomizedIncreaseingTimer( initializer , Spawn );
	}

	public void StartGame( )
	{
		GameIsRunning = true;
		SetEnabledScreen( ScreenType.Play );
		ScoreText.text = $"SCORE: {score}";
		HighscoreText.text = $"Highscore: {PlayerPrefsManager.Highscore}";
		moles.GetRandomElement().Rise();
	}

	void Update( )
	{
		if ( GameIsRunning )
		{
			if ( score < 0 ) score = 0;
			spawnTimer.OnUpdate();
			ScoreText.text = $"SCORE: {score}";
		}

		colorChangeTimer.OnUpdate();

		for ( int i = 0; i < colorfulInfoTexts.Length; i++ )
			colorfulInfoTexts[ i ].color =
				Color.Lerp(
					colorfulInfoTexts[ i ].color ,
					targetColors[ i ] , Time.deltaTime * 1.5f );
	}

	private void Spawn( )
	{
		for ( int i = 0; i < Random.Range( 1 , 4 ); i++ )
			moles.GetRandomElement().Rise();
	}

	public void Pause( )
	{
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal );
		SetEnabledScreen( ScreenType.Pause );
		GameIsRunning = false;
	}

	public void Resume( )
	{
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal );
		SetEnabledScreen( ScreenType.Play );
		GameIsRunning = true;
	}

	public void ShowHelpScreen( )
	{
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Normal );
		SetEnabledScreen( ScreenType.Help );
		GameIsRunning = false;
	}

	public void ResetHighscore( )
	{
		PlayerPrefsManager.ResetHighscore();
		HighscoreText.text = $"Highscore: {PlayerPrefsManager.Highscore}";
	}

	public void PlayAgain( ) => SceneManagemant.LoadLevel();

	public void Quit( ) => SceneManagemant.Quit();

	public void GUIGameOver( )
	{
		GameIsRunning = false;
		SetEnabledScreen( ScreenType.GameOver );
		ScoreEndText.text = $"Game Over! You got\n{score} points!!";
		bool isNewHighScore = PlayerPrefsManager.IsNewHighscore( score );
		NewHighscoreTextDisplay.gameObject.SetActive( isNewHighScore );
		if ( isNewHighScore )
			CheerleaderCoordinator.ChangeAnimation(
				CheerleaderCoordinator.CheerState.Win );
		else
			CheerleaderCoordinator.ChangeAnimation(
				CheerleaderCoordinator.CheerState.Lose );
	}

	void ChangeColor( )
	{
		for ( int i = 0; i < targetColors.Length; i++ )
			targetColors[ i ] = Random.ColorHSV(
					hueMin: 0 , hueMax: 1 ,
					saturationMin: 0.65f , saturationMax: 1 ,
					valueMin: 0.75f , valueMax: 1 );
	}

	internal void IncreaseScore( int points )
	{
		CheerleaderCoordinator.ChangeAnimation( CheerleaderCoordinator.CheerState.Score );
		score += points;
	}

	internal void DecreaseScore( int points )
	{
		CheerleaderCoordinator.ChangeAnimation(
			CheerleaderCoordinator.CheerState.AntiScore );
		score -= points;
	}

	void SetEnabledScreen( ScreenType screenType )
	{
		foreach ( var screen in screens ) screen.SetActive( false );
		screens[ screenType.GetHashCode() ].SetActive( true );
	}
}
