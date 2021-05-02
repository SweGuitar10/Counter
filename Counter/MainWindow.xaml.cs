using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Counter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
		public MainWindow()
		{
			InitializeComponent();
			StartInputCheckThread();
		}


		private void UpdateTextFile()
		{
			FileHandler.Path = txtFilePath.Text;
			FileHandler.UpdateFile(AddString());
		}

		private string AddString()
		{
			string fileContent = "";
			fileContent += $"{txtCounter.Text} {lblCount.Content}";
			return fileContent;
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			Add();
		}

		public void Add()
		{
			int count = int.Parse(lblCount.Content.ToString());
			count++;
			lblCount.Content = count;

			UpdateTextFile();
		}

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			lblCount.Content = FileHandler.OpenFile();
			txtFilePath.Text = FileHandler.Path;
		}

		[DllImport("User32.dll")]
		public static extern int GetAsyncKeyState(Int32 i);

		public int KeyCode = 145;
		private int pressedState = 32769;
		private bool checkInput = true;

		public void CheckInput()
		{
			Thread.Sleep(500);
			while (checkInput)
			{
				for (int i = 0; i <= 255; i++)
				{
					int keyState = GetAsyncKeyState(i);
					if (i == KeyCode && keyState == pressedState)
					{
						//Add();
						this.Dispatcher.Invoke(() => { Add(); });
					}
				}
			}
		}

		private void StartInputCheckThread()
		{
			Thread thread = new Thread(new ThreadStart(CheckInput));
			thread.Start();
		}

		private void ChangeKey()
		{

			MessageBox.Show("Change Key!");
			Thread.Sleep(500);
			bool loop = true;
			while (loop)
			{
				for (int i = 0; i <= 255; i++)
				{
					int keyState = GetAsyncKeyState(i);
					if (keyState == pressedState)
					{
						KeyCode = i;
						loop = false;
					}
				}
			}
			
			checkInput = true;
			StartInputCheckThread();
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			ChangeKey();
		}
	}
}
