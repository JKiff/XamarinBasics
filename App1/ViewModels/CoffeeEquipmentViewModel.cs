using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Models;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace App1.ViewModels
{
	public class CoffeeEquipmentViewModel : BaseViewModel
	{
		private static readonly Regex sWhitespace = new Regex(@"\s+");
		string newChoreText = "";
		public string NewChoreTextProperty { get { return newChoreText; } set { newChoreText = value; } }
		public ObservableRangeCollection<ChoreDisplay> Chores1 { get; }
		public AsyncCommand RefreshCommand { get; }
		ChoreDisplay selectedChore;
		ChoreDisplay previouslySelectedChore;
		public ChoreDisplay SelectedChore { get => selectedChore; set
			{
				if (value != null)
				{
					previouslySelectedChore = (ChoreDisplay)value;
					Application.Current.MainPage.DisplayAlert("aoeu", previouslySelectedChore.Name,"ok");
					value = null;
				}
				selectedChore = value;
				OnPropertyChanged();

			}
		}
		public CoffeeEquipmentViewModel()
		{

			Chores1 = new ObservableRangeCollection<ChoreDisplay>
			{
				new ChoreDisplay("thing1", "Noteaoaoentuh"),
				new ChoreDisplay("thnig52", "note 222"),
				new ChoreDisplay("thing32", "note1"),
				new ChoreDisplay("thing", "thing")
			};

			RefreshCommand = new AsyncCommand(Refresh);
			addNewChoreCommand = new MvvmHelpers.Commands.Command(AddChore);
		}
		async Task Refresh()
		{
			IsBusy = true;
			await Task.Delay(1000);
			IsBusy = false;
		}

		public ICommand addNewChoreCommand { get; }
		public void AddChore()
		{
			if (!IsEmptyText(newChoreText))
			{
				var chore = new ChoreDisplay(newChoreText);
				Chores1.Add(chore);
				Application.Current.MainPage.DisplayAlert("adding chore", chore.Name, "ok");
			}
			else
			{
				Application.Current.MainPage.DisplayAlert("text cannot be empty", "Poop", "ok");

			}
		}
		public bool IsEmptyText(string text)
		{
			foreach(char c in text)
			{
				if (!(c == '\n' || c == '\t' || c == ' '))
				{
					return false;
				}
			}
			return true;
		}
	}

}
