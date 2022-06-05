using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPool 
{
    private class WaitForSeconds : CustomYieldInstruction
	{
		private float waitUntil;
		public override bool keepWaiting
		{
			get
			{
				if( Time.time < waitUntil )
					return true;

				Pool( this );
				return false;
			}
		}

		public void Initialize( float seconds )
		{
			waitUntil = Time.time + seconds;
		}
	}

	

	private const int POOL_INITIAL_SIZE = 1000;

	private static readonly Stack<WaitForSeconds> waitForSecondsPool;

	static WaitPool()
	{
		waitForSecondsPool = new Stack<WaitForSeconds>( POOL_INITIAL_SIZE );

		for( int i = 0; i < POOL_INITIAL_SIZE; i++ )
		{
			waitForSecondsPool.Push( new WaitForSeconds() );
			
		}
	}

	public static CustomYieldInstruction Wait( float seconds )
	{
		WaitForSeconds instance;
		if( waitForSecondsPool.Count > 0 )
			instance = waitForSecondsPool.Pop();
		else
			instance = new WaitForSeconds();

		instance.Initialize( seconds );
		return instance;
	}



	private static void Pool( WaitForSeconds instance )
	{
		waitForSecondsPool.Push( instance );
	}


}
