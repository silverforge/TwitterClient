using System;

namespace Silverforge.TwitterClient.Common
{
	public static class FormatHelper
	{
		public static string HtmlToBbCodeText(string htmlText)
		{
			var workingPiece = htmlText.Replace("[", "[]").Replace("]", "[]").Replace("[[", "[").Replace("]]", "]");
			var bbCodeText = workingPiece.Replace("</a>", "[/url]").Replace("\" target=\"_blank\">", "]").Replace("<a href=\"", "[url=");
			return bbCodeText;
		}

		public static string UniDate(DateTime date)
		{
			var referenceDate = DateTime.Now;
			if (referenceDate.Year.Equals(date.Year) &&
				referenceDate.Month.Equals(date.Month) &&
				referenceDate.Day.Equals(date.Day))
				return String.Format("{0:HH:mm}", date);

			return String.Format("{0:yyyy-MM-dd HH:mm}", date);
		}
	}
}