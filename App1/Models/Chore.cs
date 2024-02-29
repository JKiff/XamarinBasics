using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
	public class Chore
	{
		public string title;
		public string notes;
		public Chore(string title, string notes)
		{
			this.title = title;
			this.notes = notes;
		}
	}
	public class ChoreDisplay
	{
		Chore chore;
		bool isExpanded;
		public bool IsExpanded
		{
			get => isExpanded;
			set => isExpanded = value;
		}
		public ChoreDisplay(string title, string notes)
		{
			chore = new Chore(title, notes);
		}
		public ChoreDisplay(string title) : this(title, "") { }
		public string Name
		{
			get
			{
				return chore.title;
			}
			set
			{
				chore.title = value;
			}
		}
		public string Notes
		{
			get
			{
				return chore.notes;
			}
		}
	}
}
