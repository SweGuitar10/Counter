using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Counter
{
	static class KeyHandler
	{
		// f1 - f12 == 112 - 123
		// ScrlLock == 145
		[DllImport("User32.dll")]
		public static extern int GetAsyncKeyState(Int32 i);

		public static int KeyCode = 145;
		public static Action OnButtonPressed;
		private static int pressedState = 32769;


		public static void Start()
		{
			Thread thread = new Thread(new ThreadStart(CheckInput));
			thread.Start();
			Thread.Sleep(1000);

			void CheckInput()
			{
				while (true)
				{
					for(int i = 0; i <= 255; i++)
					{
						int keyState = GetAsyncKeyState(i);
						if (i == KeyCode && keyState == pressedState)
						{
							// TODO: throws exception, invalidoperation thread doesn't own bla bla bla
							OnButtonPressed?.Invoke();
						}
					}
				}
			}
		}
	}
}
