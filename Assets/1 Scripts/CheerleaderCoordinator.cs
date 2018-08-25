using System.Collections;
using UnityEngine;
using Utilities.Extensions;

public class CheerleaderCoordinator : Singleton<CheerleaderCoordinator>
{
	[SerializeField] new RuntimeAnimatorController animation;

	QuerySDMecanimController[] sQuerySDMecanimControllers;
	private void Start( )
	{
		foreach ( QuerySDMecanimController cheerleaderP in
			( sQuerySDMecanimControllers = GetComponentsInChildren<QuerySDMecanimController>( true ) ) )
			cheerleaderP.
				GetComponentInChildren<Animator>().
				runtimeAnimatorController = animation;
		ChooseRandomCheerleader();
	}

	public void ChooseRandomCheerleader( )
	{
		foreach ( QuerySDMecanimController cheerleaderP in
			sQuerySDMecanimControllers )
			cheerleaderP.gameObject.SetActive( true );

		QuerySDMecanimController cheerleader1 = sQuerySDMecanimControllers.GetRandomElement();
		QuerySDMecanimController cheerleader2 = sQuerySDMecanimControllers.GetRandomElement();

		while ( cheerleader1.gameObject.name == cheerleader2.gameObject.name )
			cheerleader2 = sQuerySDMecanimControllers.GetRandomElement();

		bool cheerleader1IsOnPositivePosition = Random.Range( 0 , 2 ) == 0;
		if ( cheerleader1IsOnPositivePosition )
		{
			cheerleader1.gameObject.transform.localPosition =
				new Vector3( 2 , 0 , 0 );
			cheerleader2.gameObject.transform.localPosition =
				new Vector3( -2 , 0 , 0 );
		}
		else
		{
			cheerleader1.gameObject.transform.localPosition =
				new Vector3( -2 , 0 , 0 );
			cheerleader2.gameObject.transform.localPosition =
				new Vector3( 2 , 0 , 0 );
		}
		foreach ( QuerySDMecanimController cheerleaderP in
			sQuerySDMecanimControllers )
		{
			if ( cheerleaderP.gameObject.name !=
				 cheerleader1.gameObject.name &&
				 cheerleaderP.gameObject.name !=
				 cheerleader2.gameObject.name )
				cheerleaderP.gameObject.SetActive( false );
		}
		ChangeAnimation( CheerState.Normal );
	}

	public enum CheerState
	{
		Normal,
		Score,
		AntiScore,
		Lose,
		Win
	}

	public static void ChangeAnimation( CheerState state ) => I.StartCoroutine( I.ChangeAnimIEnumerator( state ) );

	IEnumerator ChangeAnimIEnumerator( CheerState state )
	{
		switch ( state )
		{
		case CheerState.Normal:
			ChangeAnim( state );
			yield return null;
			break;
		case CheerState.Score:
		case CheerState.AntiScore:
			ChangeAnim( state );
			yield return new WaitForSeconds( 2.5f );
			ChangeAnim( CheerState.Normal );
			break;
		case CheerState.Win:
		case CheerState.Lose:
			ChangeAnim( state );
			yield return null;
			break;
		default:
			break;
		}
		yield return null;
	}
	void ChangeAnim( CheerState state )
	{
		int? stateOfMotion = null;
		foreach ( QuerySDMecanimController mechanimController in
			GetComponentsInChildren<QuerySDMecanimController>() )
		{
			switch ( state )
			{
			case CheerState.Normal:
				mechanimController.
					ChangeAnimation(
					QuerySDMecanimController.
					QueryChanSDAnimationType.NORMAL_IDLE );
				break;
			case CheerState.Score:
				if ( stateOfMotion != null )
					stateOfMotion = Random.Range( 0 , 3 );
				mechanimController.
					ChangeAnimation(
					// if
					stateOfMotion == 1 ?
					// then
					QuerySDMecanimController.
					QueryChanSDAnimationType.NORMAL_ITEMGET :
					// else
					QuerySDMecanimController.
					QueryChanSDAnimationType.OSAKA_CLAP );
				break;
			case CheerState.AntiScore:
				mechanimController.
					ChangeAnimation(
					QuerySDMecanimController.
					QueryChanSDAnimationType.NORMAL_DAMAGE );
				break;
			case CheerState.Lose:
				mechanimController.
					ChangeAnimation(
					QuerySDMecanimController.
					QueryChanSDAnimationType.NORMAL_LOSE );
				break;
			case CheerState.Win:
				mechanimController.
					ChangeAnimation(
					QuerySDMecanimController.
					QueryChanSDAnimationType.NORMAL_WIN );
				break;
			default:
				break;
			}
		}
	}
}
