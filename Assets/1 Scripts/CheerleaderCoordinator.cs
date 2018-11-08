using System.Collections;
using UnityEngine;
using Utilities.Extensions;
public
class CheerleaderCoordinator : Singleton<CheerleaderCoordinator>
{
	#pragma warning disable 0649
	[SerializeField] new RuntimeAnimatorController animation;
	#pragma warning restore 0649
	QuerySDMecanimController[] __SQuerySDMecanimControllers;
	public
	QuerySDMecanimController[] SQuerySDMecanimControllers
	{

		get
		{
			return __SQuerySDMecanimControllers ?? (__SQuerySDMecanimControllers = GetComponentsInChildren<QuerySDMecanimController>( true ));


		}

	}
	void Start( )
	{

		foreach(var cheerleaderP in SQuerySDMecanimControllers)
		{
			Animator animator = cheerleaderP.GetComponentInChildren<Animator>();
			animator.applyRootMotion = false;
			animator.runtimeAnimatorController = animation;

		}
		ChooseRandomCheerleader();

	}
	public
	void ChooseRandomCheerleader( )
	{

		foreach(var cheerleaderP in SQuerySDMecanimControllers)
		{
			cheerleaderP.gameObject.SetActive( false );

		}
		QuerySDMecanimController cheerleader1 = SQuerySDMecanimControllers.GetRandomElement();
		QuerySDMecanimController cheerleader2 = SQuerySDMecanimControllers.GetRandomElement();
		while(cheerleader1.gameObject.name == cheerleader2.gameObject.name)
		{
			cheerleader2 = SQuerySDMecanimControllers.GetRandomElement();

		}
		bool cheerleader1IsOnPositivePosition = Random.Range( 0 , 2 ) == 0;
		if(cheerleader1IsOnPositivePosition)
		{
			cheerleader1.gameObject.transform.localPosition = new Vector3( 2 , 0 , 0 );
			cheerleader2.gameObject.transform.localPosition = new Vector3( -2 , 0 , 0 );

		}
		else
		{
			cheerleader1.gameObject.transform.localPosition = new Vector3( -2 , 0 , 0 );
			cheerleader2.gameObject.transform.localPosition = new Vector3( 2 , 0 , 0 );

		}
		cheerleader1.gameObject.SetActive( true );
		cheerleader2.gameObject.SetActive( true );
		ChangeAnimation( CheerState.Normal );

	}
	public
	enum CheerState
	{
		Normal,
		Score,
		AntiScore,
		Lose,
		Win

	}
	public static
	void ChangeAnimation( CheerState state )
	{
		I.StartCoroutine( I.ChangeAnimIEnumerator( state ) );

	}
	IEnumerator ChangeAnimIEnumerator( CheerState state )
	{

		switch(state)
		{

			case CheerState.Score:


			
			case CheerState.AntiScore:
				ChangeAnim( state );
				yield return new WaitForSeconds( 2.5f );
				ChangeAnim( CheerState.Normal );

			break;
			case CheerState.Normal:


			
			case CheerState.Win:


			
			case CheerState.Lose:
				ChangeAnim( state );

			break;
			default: 

			break;

		}
		yield return null;

	}
	void ChangeAnim( CheerState state )
	{
		int stateOfMotion = Random.Range( 0 , 3 );
		foreach(var mechanimController in SQuerySDMecanimControllers)
		{

			switch(state)
			{

				case CheerState.Normal:
					mechanimController.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_IDLE );

				break;
				case CheerState.Score:
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
					mechanimController.ChangeAnimation( QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_DAMAGE );

				break;
				case CheerState.Lose:
					mechanimController.ChangeAnimation( QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_LOSE );

				break;
				case CheerState.Win:
					mechanimController.ChangeAnimation( QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_WIN );

				break;
				default: 

				break;

			}

		}

	}

}
