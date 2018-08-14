using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public delegate Coroutine CoroutineRunner ( IEnumerator routine );

    public delegate TReturn Action<TReturn> ();

    public delegate TReturn Action<TReturn , TParameter> ( TParameter parameter );

    public delegate TReturn Action<TReturn , TParameter1 , TParameter2> ( TParameter1 parameter1 , TParameter2 parameter2 );

    public delegate TReturn Action<TReturn , TParameter1 , TParameter2 , TParameter3> ( TParameter1 parameter1 , TParameter2 parameter2 , TParameter3 parameter3 );

    public delegate TReturn Action<TReturn , TParameter1 , TParameter2 , TParameter3 , TParameter4> ( TParameter1 parameter1 , TParameter2 parameter2 , TParameter3 parameter3 , TParameter4 parameter4 );

    public delegate TReturn Action<TReturn , TParameter1 , TParameter2 , TParameter3 , TParameter4 , TParameter5> ( TParameter1 parameter1 , TParameter2 parameter2 , TParameter3 parameter3 , TParameter4 parameter4 , TParameter5 parameter5 );

    public delegate TReturn Action<TReturn , TParameter1 , TParameter2 , TParameter3 , TParameter4 , TParameter5 , TParameter6> ( TParameter1 parameter1 , TParameter2 parameter2 , TParameter3 parameter3 , TParameter4 parameter4 , TParameter5 parameter5 , TParameter6 parameter6 );

    /// <summary>
    /// The main utilities class.
    /// </summary>
    public static class U
    {
        # region Randomize Array
        /// <summary>
        /// Shuffles the array you passed in and returns it.
        /// Along with keeping the array you passed in as a parameter intact.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array
        /// </typeparam>
        /// <param name="arrayParam">
        /// The array that will be copied and shuffled
        /// </param>
        /// <returns>
        /// Another array that has all the elements as the orignal array but just shuffeed
        /// </returns>
        public static T[] ShuffleArray<T> ( T[] arrayParam )
        {
            T[] array = DuplicateArray ( arrayParam );

            System.Random prng = new System.Random ();

            for ( int i = 0 ;
                i < array.Length - 1 ;
                i++ )
            {
                int randomIndex = prng.Next ( i , array.Length );
                T tempItem = array[ randomIndex ];
                array[ randomIndex ] = array[ i ];
                array[ i ] = tempItem;
            }

            return array;
        }

        /// <summary>
        /// Shuffles the array you passed in and returns it.
        /// Along with keeping the array you passed in as a parameter intact.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array
        /// </typeparam>
        /// <param name="arrayParam">
        /// The array that will be copied and shuffled
        /// </param>
        /// <param name="seed">
        /// The seed value for the random generator
        /// </param>
        /// <returns>
        /// Another array that has all the elements as the orignal array but just shuffeed
        /// </returns>
        public static T[] ShuffleArray<T> ( T[] array , int seed )
        {
            System.Random prng = new System.Random ( seed );

            for ( int i = 0 ;
                i < array.Length - 1 ;
                i++ )
            {
                int randomIndex = prng.Next ( i , array.Length );
                T tempItem = array[ randomIndex ];
                array[ randomIndex ] = array[ i ];
                array[ i ] = tempItem;
            }

            return array;
        }

        /// <summary>
        /// Gets a random element from the array you passed in and returns it
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array
        /// </typeparam>
        /// <param name="array">
        /// The array from which to extract random element
        /// </param>
        /// <returns>
        /// A random element from the array you passed in
        /// </returns>
        public static T GetRandomElementFromArray<T> ( T[] array )
        {
            return array[ Random.Range ( 0 , array.Length ) ];
        }
        #endregion

        # region Random Between Vector 2 and 4
        /// <summary>
        /// Returns a random value between Vector 2 Bounds
        /// </summary>
        /// <param name="V2">
        /// The bounds
        /// </param>
        /// <returns>
        /// Returns a random value between Vector 2 Bounds
        /// </returns>
        public static float RandomBetweenV2Bounds ( Vector2 V2 )
        {
            return Random.Range ( V2.x , V2.y );
        }

        /// <summary>
        /// Returns a random Vector 2 between Vector 4 Bounds
        /// </summary>
        /// <param name="V4">
        /// The bounds
        /// </param>
        /// <returns>
        /// Returns a random Vector 2 between Vector 4 Bounds
        /// </returns>
        public static Vector2 RandomBetweenV4 ( Vector4 V4 )
        {
            Vector2 v = new Vector2 ( V4.x , V4.y );
            Vector2 v2 = new Vector2 ( V4.z , V4.w );
            return new Vector2 ( RandomBetweenV2Bounds ( v ) , RandomBetweenV2Bounds ( v2 ) );
        }
        # endregion

        # region Swapping and Array Duplication
        /// <summary>
        /// Swaps the values the variables you passed in
        /// </summary>
        /// <typeparam name="T">
        /// The type of the variables you pass in
        /// </typeparam>
        /// <param name="variable1">
        /// The variable whose value you want to replace with the second variable
        /// </param>
        /// <param name="variable2">
        /// The variable whose value you want to replace with the first variable
        /// </param>
        public static void Swap<T> ( ref T variable1 , ref T variable2 )
        {
            T temp = variable1;
            variable1 = variable2;
            variable2 = temp;
        }

        /// <summary>
        /// Returns a new array in a different memory location,
        /// the new array being identical to the array you passed in as a parameter.
        /// (Refer at C or C++ pointers to know why arrays are like this)
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array you want to duplicate
        /// </typeparam>
        /// <param name="arrayToDuplicate">
        /// The array you want to duplicate
        /// </param>
        /// <returns>
        /// A new array in a different memory location,
        /// the new array being identical to the array you passed in as a parameter.
        /// (Again Refer at C or C++ pointers to know why arrays are like this)
        /// </returns>
        public static T[] DuplicateArray<T> ( T[] arrayToDuplicate )
        {
            T[] array;
            {
                List<T> temp = new List<T> ();
                foreach ( T tempItem in arrayToDuplicate )
                    temp.Add ( tempItem );
                array = temp.ToArray ();
            }
            return array;
        }
        # endregion
    }

    /// <summary>
    /// The scene management class this namespace
    /// </summary>
    public static class SceneManagemant
    {
        # region Load Level Overloads
        /// <summary>
        /// Reloads the current level or scene
        /// </summary>
        public static void LoadLevel ()
        {
            SceneManager.LoadScene ( SceneManager.GetActiveScene ().buildIndex );
        }

        /// <summary>
        /// Loads the scene or level with the build index
        /// </summary>
        /// <param name="buildIndex">
        /// The build index of the level you want to load
        /// </param>
        public static void LoadLevel ( int buildIndex )
        {
            SceneManager.LoadScene ( buildIndex );
        }

        /// <summary>
        /// Loads the scene or level with the name you pass in
        /// </summary>
        /// <param name="sceneName">
        /// The name of the level you want to load
        /// </param>
        public static void LoadLevel ( string sceneName )
        {
            SceneManager.LoadScene ( sceneName );
        }
        # endregion

        /// <summary>
        /// Returns the number of the type of object 
        /// that you passed in that are in the scene
        /// </summary>
        /// <typeparam name="T">
        /// The type of the number of objects you want to get
        /// </typeparam>
        /// <returns>
        /// The number of the type of objects in the scene
        /// </returns>
        public static int ObjsOfTypeInScreen<T> () where T : Object
        {
            return GameObject.FindObjectsOfType<T> ().Length;
        }

        /// <summary>
        /// Quits the application
        /// </summary>
        public static void Quit ()
        {
            Application.Quit ();
        }
    }

    /// <summary>
    /// The class for easy data access
    /// </summary>
    public static class PlayerPrefsManager
    {
        # region PlayerPrefs Constants
        // The constant string data keys for data access
        const string MASTER_VOLUME_KEY = "master_volume";
        const string BGM_VOLUME_KEY = "bgm_volume_key";
        const string SFX_VOLUME_KEY = "sfx_volume_key";
        const string HIGHSCORE_KEY = "highscore_key";
        #endregion

        #region Volume Functions
        /// <summary>
        /// The SFX volume property
        /// </summary>
        public static float SFXVolume
        {
            get
            {
                return PlayerPrefs.GetFloat ( SFX_VOLUME_KEY , 1 );
            }
            set
            {
                PlayerPrefs.SetFloat ( SFX_VOLUME_KEY , value );
                PlayerPrefs.Save ();
            }
        }

        /// <summary>
        /// The Master volume property
        /// </summary>
        public static float MasterVolume
        {
            get
            {
                return PlayerPrefs.GetFloat ( MASTER_VOLUME_KEY , 1 );
            }
            set
            {
                PlayerPrefs.SetFloat ( MASTER_VOLUME_KEY , value );
                PlayerPrefs.Save ();
            }
        }

        /// <summary>
        /// The BackGround Music volume property
        /// </summary>
        public static float BGMVolume
        {
            get
            {
                return PlayerPrefs.GetFloat ( BGM_VOLUME_KEY , 1 );
            }
            set
            {
                PlayerPrefs.SetFloat ( BGM_VOLUME_KEY , value );
                PlayerPrefs.Save ();
            }
        }
        # endregion

        #region HighScore Functions
        public static bool IsNewHighscore ( int values )
        {
            if ( values > Highscore )
            {
                Highscore = values;
                return true;
            }
            return false;
        }

        public static void ResetHighscore ()
        {
            Highscore = 0;
        }

        public static int Highscore
        {
            get
            {
                return PlayerPrefs.GetInt ( HIGHSCORE_KEY , 0 );
            }
            private set
            {
                PlayerPrefs.SetInt ( HIGHSCORE_KEY , value );
                PlayerPrefs.Save ();
            }
        }
        #endregion

        // Fill it in yourself
        # region Looks And Prefrences
        #endregion
    }

    /// <summary>
    /// Remember the PlayerPrefs class, 
    /// as you all know it doesn't, even in Unity 2017.2.0f3, 
    /// have any provision to save boolean values(true or false).
    /// This class helps you to save boolean values
    /// and retrieve them. And it saves the values right when you set them.
    /// So you wont have to write:
    ///     BoolPlayerPrefs.SetBool(x,true);
    ///     PlayerPrefs.Save ();
    /// You can just write:
    ///     BoolPlayerPrefs.SetBool(x,true);
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
            PlayerPrefs.SetString ( key , value.ToString () );
            PlayerPrefs.Save ();
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
            string v = PlayerPrefs.GetString ( key , defaultValue.ToString () );
            if ( v == true.ToString () )
                return true;
            return false;
        }
    }

    /// <summary>
    /// Just a simple pair
    /// </summary>
    /// <typeparam name="T">
    /// The pair type
    /// </typeparam>
    [System.Serializable]
    public class Pair<T>
    {
        /// <summary>
        /// The first value in the pair
        /// </summary>
        public T T1;
        /// <summary>
        /// The second value in the pair
        /// </summary>
        public T T2;

        /// <summary>
        /// Initializes the pair with the 2 values you passed in
        /// </summary>
        /// <param name="t1">
        /// 1st value
        /// </param>
        /// <param name="t2">
        /// 2nd value
        /// </param>
        public Pair ( T t1 , T t2 )
        {
            T1 = t1;
            T2 = t2;
        }

        /// <summary>
        /// Initializes the pair
        /// </summary>
        public Pair () { }
    }
}

namespace Utilities.Timers
{
    [System.Serializable]
    public class Timer
    {
        float secondsBetweenTicks ,
              currentTimerValue;
        System.Action functionToCallEachTick;
        bool stopped;
        CoroutineRunner runner;

        public Timer ( float _secondsBetweenTicks ,
                       System.Action _functionToCallEachTick ,
                       CoroutineRunner _runner )
        {
            secondsBetweenTicks = _secondsBetweenTicks;
            functionToCallEachTick = _functionToCallEachTick;
            runner = _runner;
        }

        public void StartTimer ()
        {
            runner ( StartIETimer () );
        }

        IEnumerator StartIETimer ()
        {
            stopped = false;
            while ( !stopped )
            {
                currentTimerValue -= Time.deltaTime;
                if ( currentTimerValue <= 0 )
                {
                    functionToCallEachTick ();
                    currentTimerValue = secondsBetweenTicks;
                }
                yield return null;
            }
        }

        public void StopTimer ()
        {
            stopped = true;
        }
    }

    public class RandomizedTimer
    {
        float secondsBetweenTicksMin ,
              secondsBetweenTicksMax ,
              currentTimerValue;
        System.Action functionToCallEachTick;
        bool stopped;
        CoroutineRunner runner;

        public RandomizedTimer ( float _secondsBetweenTicksMin ,
                                 float _secondsBetweenTicksMax ,
                                 System.Action _functionToCallEachTick ,
                                 CoroutineRunner _runner )
        {
            secondsBetweenTicksMin = _secondsBetweenTicksMin;
            secondsBetweenTicksMax = _secondsBetweenTicksMax;
            functionToCallEachTick = _functionToCallEachTick;
            runner = _runner;
        }

        public void StartRandomizedTimer ()
        {
            runner ( StartIETimer () );
        }

        IEnumerator StartIETimer ()
        {
            stopped = false;
            while ( !stopped )
            {
                currentTimerValue -= Time.deltaTime;
                if ( currentTimerValue <= 0 )
                {
                    functionToCallEachTick ();
                    currentTimerValue = UnityEngine.Random.Range ( secondsBetweenTicksMin , secondsBetweenTicksMax );
                }
                yield return null;
            }
        }

        public void StopRandomizedTimer ()
        {
            stopped = true;
        }
    }
}

namespace Utilities.Timers.Custom
{
    [System.Serializable]
    public class RandomizedIncreaseingTimer
    {
        float secondsBetweenTicksMin ,
              secondsBetweenTicksMax ,
              increaseInSecondsBetweenTicksMinMin ,
              increaseInSecondsBetweenTicksMinMax ,
              currentTimerValue ,
              startSecondsBetweenTicksMax;
        System.Action functionToCallEachTick;
        bool stopped;
        CoroutineRunner runner;

        public RandomizedIncreaseingTimer ( float _secondsBetweenTicksMin ,
                                            float _secondsBetweenTicksMax ,
                                            float _increaseInSecondsBetweenTicksMinMin ,
                                            float _increaseInSecondsBetweenTicksMinMax ,
                                            System.Action _functionToCallEachTick ,
                                            CoroutineRunner _runner )
        {
            startSecondsBetweenTicksMax = _secondsBetweenTicksMax;
            secondsBetweenTicksMin = _secondsBetweenTicksMin;
            secondsBetweenTicksMax = _secondsBetweenTicksMax;
            increaseInSecondsBetweenTicksMinMin = _increaseInSecondsBetweenTicksMinMin;
            increaseInSecondsBetweenTicksMinMax = _increaseInSecondsBetweenTicksMinMax;
            functionToCallEachTick = _functionToCallEachTick;
            runner = _runner;
        }

        public RandomizedIncreaseingTimer StartTimer ()
        {
            runner ( StartIETimer () );
            return this;
        }

        IEnumerator StartIETimer ()
        {
            stopped = false;
            while ( !stopped )
            {
                currentTimerValue -= Time.deltaTime;
                if ( currentTimerValue <= 0 )
                {
                    functionToCallEachTick ();
                    currentTimerValue = Random.Range ( secondsBetweenTicksMin , secondsBetweenTicksMax );
                    secondsBetweenTicksMax -= Random.Range ( increaseInSecondsBetweenTicksMinMin , increaseInSecondsBetweenTicksMinMax );
                    if ( secondsBetweenTicksMin >= secondsBetweenTicksMax )
                        secondsBetweenTicksMax = startSecondsBetweenTicksMax;
                }
                yield return null;
            }
        }

        public void StopTimer ()
        {
            stopped = true;
        }
    }
}

namespace Utilities.NotWorking
{
    [System.Serializable]
    public class Array<T>
    {
        public T[] array;
        public void Shuffle ()
        {
            System.Random prng = new System.Random ();

            for ( int i = 0 ;
                i < array.Length - 1 ;
                i++ )
            {
                int randomIndex = prng.Next ( i , array.Length );
                T tempItem = array[ randomIndex ];
                array[ randomIndex ] = array[ i ];
                array[ i ] = tempItem;
            }
        }
        public Array ( T[] arrayToSet )
        {
            array = new T[ arrayToSet.Length ];
            array = arrayToSet;
        }
        public T GetI ( uint index )
        {
            return Get ( index );
        }
        public T Get ( uint index )
        {
            return array[ ( index - 1 < array.Length ) ? ( array.Length ) : ( ( int ) ( index ) ) ];
        }
    }
}

namespace Utilities
{
    public class CommonGameobjectComponents : MonoBehaviour
    {
        new public Rigidbody rigidbody
        {
            get
            {
                try
                {
                    return Get<Rigidbody> ();
                }
                catch { return null; }
            }
        }
        new public Animation animation
        {
            get
            {
                try
                {
                    return Get<Animation> ();
                }
                catch { return null; }
            }
        }
        public Animator animator
        {
            get
            {
                try
                {
                    return Get<Animator> ();
                }
                catch { return null; }
            }
        }
        new public Collider collider
        {
            get
            {
                try
                {
                    return Get<Collider> ();
                }
                catch { return null; }
            }
        }
        new public Collider2D collider2D
        {
            get
            {
                try
                {
                    return Get<Collider2D> ();
                }
                catch { return null; }
            }
        }
        new public Renderer renderer
        {
            get
            {
                try
                {
                    return Get<Renderer> ();
                }
                catch { return null; }
            }
        }
        new public Rigidbody2D rigidbody2D
        {
            get
            {
                try
                {
                    return Get<Rigidbody2D> ();
                }
                catch { return null; }
            }
        }
        public Vector3 position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }
        public float positionX
        {
            get
            {
                return position.x;
            }
            set
            {
                position = new Vector3 ( value , position.y , position.z );
            }
        }
        public float positionY
        {
            get
            {
                return position.y;
            }
            set
            {
                position = new Vector3 ( position.x , value , position.z );
            }
        }
        public float positionZ
        {
            get
            {
                return position.z;
            }
            set
            {
                position = new Vector3 ( position.x , position.y , value );
            }
        }
        public T Get<T> () where T : Object
        {
            return gameObject.GetComponent<T> ();
        }
        public T GetAdd<T> () where T : Component
        {
            T component = gameObject.GetComponent<T> ();
            if ( component == null ) return gameObject.AddComponent<T> ();
            else return component;
        }
        public T Find<T> () where T : Object
        {
            return GameObject.FindObjectOfType<T> ();
        }
        public void SetPosition ( float posX , float posY , float posZ )
        {
            position = new Vector3 ( posX , posY , posZ );
        }
    }
}
