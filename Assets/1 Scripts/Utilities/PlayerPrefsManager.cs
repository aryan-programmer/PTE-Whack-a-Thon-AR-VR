using UnityEngine;

namespace Utilities
{
	[System.Serializable]
	public struct AvatarValuesHolder
	{
		[SerializeField]
		private int _avatarGroupValue;

		public int avatarGroupValue
		{
			get => _avatarGroupValue;
			set => _avatarGroupValue = value;
		}

		[SerializeField]
		private int _avatarSubGroupValue;

		public int avatarSubGroupValue
		{
			get => _avatarSubGroupValue;
			set => _avatarSubGroupValue = value;
		}

		public AvatarValuesHolder ( int avatarGroupValue , int avatarSubGroupValue )
		{
			_avatarGroupValue = avatarGroupValue;
			_avatarSubGroupValue = avatarSubGroupValue;
		}
	}

	/// <summary>
	/// The class for easy hard disk data access(PlayerPrefs)
	/// </summary>
	public static class PlayerPrefsManager
	{
		#region PlayerPrefs Constants
		// The constant string data keys for data access
		const string MASTER_VOLUME_KEY = "master_volume";
		const string BGM_VOLUME_KEY = "bgm_volume_key";
		const string SFX_VOLUME_KEY = "sfx_volume_key";
		const string HIGHSCORE_KEY = "highscore_key";
		const string ROTATE_CAMERA_BOOL_KEY = "ROTATE_CAMERA_BOOL_KEY";
		const string AVATAR_GROUP_VALUE_KEY = "AVATAR_GROUP_VALUE_KEY";
		const string AVATAR_SUB_GROUP_VALUE_KEY = "AVATAR_SUB_GROUP_VALUE_KEY";
		const string TOUCH_MODE_KEY = "TOUCH_MODE_KEY";
		#endregion

		#region Volume Functions
		/// <summary>
		/// The SFX volume property
		/// </summary>
		public static float SFXVolume
		{
			get
			{
				return PlayerPrefs.GetFloat( SFX_VOLUME_KEY , 1 );
			}
			set
			{
				PlayerPrefs.SetFloat( SFX_VOLUME_KEY , value );
				PlayerPrefs.Save();
			}
		}

		/// <summary>
		/// The Master volume property
		/// </summary>
		public static float MasterVolume
		{
			get
			{
				return PlayerPrefs.GetFloat( MASTER_VOLUME_KEY , 1 );
			}
			set
			{
				PlayerPrefs.SetFloat( MASTER_VOLUME_KEY , value );
				PlayerPrefs.Save();
			}
		}

		/// <summary>
		/// The BackGround Music volume property
		/// </summary>
		public static float BGMVolume
		{
			get
			{
				return PlayerPrefs.GetFloat( BGM_VOLUME_KEY , 1 );
			}
			set
			{
				PlayerPrefs.SetFloat( BGM_VOLUME_KEY , value );
				PlayerPrefs.Save();
			}
		}
		#endregion

		#region HighScore Functions
		/// <summary>
		/// Checks if the value you passed in is a new highscore.
		/// If it is then it sets the highscore to the value you passed in and returns true.
		/// If not the it just returns false.
		/// </summary>
		/// <param name="value">
		/// The value to check and set highscore
		/// </param>
		/// <returns>
		/// If the value you passed in is greater than the old highscore then it
		/// sets the highscore to the value you passed in and returns true.
		/// If not the it just returns false.
		/// </returns>
		public static bool IsNewHighscore ( float value )
		{
			if ( value > Highscore )
			{
				Highscore = value;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Resets the highscore
		/// </summary>
		public static void ResetHighscore ()
		{
			Highscore = 0;
		}

		/// <summary>
		/// The highscore value
		/// </summary>
		public static float Highscore
		{
			get
			{
				return PlayerPrefs.GetFloat( HIGHSCORE_KEY , 0 );
			}
			private set
			{
				PlayerPrefs.SetFloat( HIGHSCORE_KEY , value );
				PlayerPrefs.Save();
			}
		}
		#endregion

		#region VisualIty
		public static bool RotateCamera
		{
			get { return BoolPlayerPrefs.GetBool( ROTATE_CAMERA_BOOL_KEY , Application.isMobilePlatform ); }
			set { BoolPlayerPrefs.SetBool( ROTATE_CAMERA_BOOL_KEY , value ); }
		}

		public static AvatarValuesHolder CurrentAvatarValues
		{
			get { return new AvatarValuesHolder( PlayerPrefs.GetInt( AVATAR_GROUP_VALUE_KEY , 0 ) , PlayerPrefs.GetInt( AVATAR_SUB_GROUP_VALUE_KEY , 0 ) ); }
			set
			{
				PlayerPrefs.SetInt( AVATAR_GROUP_VALUE_KEY , value.avatarGroupValue );
				PlayerPrefs.Save();
				PlayerPrefs.SetInt( AVATAR_SUB_GROUP_VALUE_KEY , value.avatarSubGroupValue );
				PlayerPrefs.Save();
			}
		}

		public static bool TouchMode
		{
			get { return BoolPlayerPrefs.GetBool( TOUCH_MODE_KEY , false ); }
			set { BoolPlayerPrefs.SetBool( TOUCH_MODE_KEY , value ); }
		}
		#endregion
	}

	/// <summary>
	/// This class helps you to save boolean values
	/// and retrieve them. And it saves the values right when you set them.
	/// <para>So you wont have to write:</para>
	/// <para>BoolPlayerPrefs.SetBool(x,true);</para>
	/// <para>PlayerPrefs.Save ();</para>
	/// You can just write:
	/// <para>BoolPlayerPrefs.SetBool(x,true);</para>
	/// </summary>
	public sealed class BoolPlayerPrefs
	{
		/// <summary>
		/// Sets the boolean value with the key you passed in.
		/// </summary>
		/// <param name="key">
		/// The key, tag of the boolean value you want to save to or edit.
		/// </param>
		/// <param name="value">
		/// The value you want to set to the corresponding key you passed in.
		/// </param>
		public static void SetBool ( string key , bool value )
		{
			PlayerPrefs.SetString( key , value.ToString() );
			PlayerPrefs.Save();
		}

		/// <summary>
		/// Gets the boolean value with the key you passed in.
		/// </summary>
		/// <param name="key">
		/// The key, tag of the boolean value you want to read.
		/// </param>
		/// <param name="defaultValue">
		/// The defaule value return value you want to get if the actual value isn't set.
		/// </param>
		/// <returns>
		/// The boolean value with the key you passed in.
		/// </returns>
		public static bool GetBool ( string key , bool defaultValue = false )
		{
			string v = PlayerPrefs.GetString( key , defaultValue.ToString() );
			if ( v == true.ToString() ) return true;
			else return false;
		}
	}
}
