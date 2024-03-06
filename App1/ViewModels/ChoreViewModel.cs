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
using System.Collections.Specialized;
using System.ComponentModel;

namespace App1.ViewModels
{
	public class ChoreViewModel : BaseViewModel
	{
		string newChoreText = "";
		public string NewChoreTextProperty { get { return newChoreText; } set { newChoreText = value; OnPropertyChanged(); } }
		public ObservableCollection<ChoreDisplay> Chores1 { get; set; }


		public AsyncCommand RefreshCommand { get; }
		ChoreDisplay selectedChore;
		ChoreDisplay previouslySelectedChore;

		public ChoreDisplay SelectedChore { 
			get => selectedChore; 
			set
			{
				if (value != null)
				{
					if (previouslySelectedChore != null) previouslySelectedChore.IsExpanded = false;
					previouslySelectedChore = (ChoreDisplay)value;
					previouslySelectedChore.IsExpanded = true;
					previouslySelectedChore.IncrementTitle();
					Application.Current.MainPage.DisplayAlert("aoeu", previouslySelectedChore.Name + previouslySelectedChore.IsExpanded,"ok");
					value = null;
				}
				selectedChore = value;
				OnPropertyChanged();

			}
		}
		public ChoreViewModel()
		{
			//Chores1.CollectionChanged += Chores_CollectionChanged;
			Chores1 = new ObservableCollection<ChoreDisplay>
			{
				new ChoreDisplay("thing1", "Noteaoaoentuh"),
				new ChoreDisplay("thing52", "note 222"),
				new ChoreDisplay("thing32", "note1"),
				new ChoreDisplay("thing", "thing")
			};

			RefreshCommand = new AsyncCommand(Refresh);
			addNewChoreCommand = new MvvmHelpers.Commands.Command(AddChore);
		}

		private void Chores_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.OldItems != null)
			{
				foreach (INotifyPropertyChanged item in e.OldItems)
					item.PropertyChanged -= item_PropertyChanged;
			}
			if (e.NewItems != null)
			{
				foreach (INotifyPropertyChanged item in e.NewItems)
					item.PropertyChanged += item_PropertyChanged;
			}
		}

	 private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Application.Current.MainPage.DisplayAlert("property changing", "poop", "ok");
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
				NewChoreTextProperty = "";
			}
			else
			{
				Application.Current.MainPage.DisplayAlert("text cannot be empty", "chore entry", "ok");

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
