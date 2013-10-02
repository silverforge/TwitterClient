using Caliburn.Micro;

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
		private bool isExpanded;
		private bool isRetweeted;
		private string retweetedBy;
		private long originalId;

		public long OriginalId
		{
			get { return originalId; }
			set
			{
				originalId = value;
				NotifyOfPropertyChange(() => OriginalId);
			}
		}

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

		public bool IsExpanded
		{
			get { return isExpanded; }
			set
			{
				isExpanded = value;
				NotifyOfPropertyChange(() => IsExpanded);
			}
		}

		public bool IsRetweeted
		{
			get { return isRetweeted; }
			set
			{
				isRetweeted = value;
				NotifyOfPropertyChange(() => IsRetweeted);
			}
		}

		public string RetweetedBy
		{
			get { return retweetedBy; }
			set
			{
				retweetedBy = value;
				NotifyOfPropertyChange(() => RetweetedBy);
			}
		}
	}
}
