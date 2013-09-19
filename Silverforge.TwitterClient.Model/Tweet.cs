﻿using Caliburn.Micro;

namespace Silverforge.TwitterClient.Model
{
	public class Tweet : PropertyChangedBase
	{
		private long id;
		private string userFullName;
		private string text;
		private string imageUrl;
		private bool isNew;
		private bool isFavorited;
		private string created;

		public long Id
		{
			get { return id; }
			set
			{
				id = value;
				NotifyOfPropertyChange(() => Id);
			}
		}

		public string UserFullName
		{
			get { return userFullName; }
			set
			{
				userFullName = value;
				NotifyOfPropertyChange(() => UserFullName);
			}
		}

		public string Text
		{
			get { return text; }
			set
			{
				text = value;
				NotifyOfPropertyChange(() => Text);
			}
		}

		public string ImageUrl
		{
			get { return imageUrl; }
			set
			{
				imageUrl = value;
				NotifyOfPropertyChange(() => ImageUrl);
			}
		}

		public string Created
		{
			get { return created; }
			set
			{
				created = value;
				NotifyOfPropertyChange(() => Created);
			}
		}

		public bool IsNew
		{
			get { return isNew; }
			set
			{
				isNew = value;
				NotifyOfPropertyChange(() => IsNew);
			}
		}

		public bool IsFavorited
		{
			get { return isFavorited; }
			set
			{
				isFavorited = value;
				NotifyOfPropertyChange(() => IsFavorited);
			}
		}
	}
}
