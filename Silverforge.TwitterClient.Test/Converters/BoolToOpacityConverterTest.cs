using FluentAssertions;
using NUnit.Framework;
using Silverforge.TwitterClient.Converters;

namespace Silverforge.TwitterClient.Test.Converters
{
	[TestFixture]
	public class BoolToOpacityConverterTest
	{
		private readonly BoolToOpacityConverter converter = new BoolToOpacityConverter();

		[Test]
		public void Convert_True_Valid()
		{
			var convert = converter.Convert(true, null, null, null);
			convert.Should().BeOfType<double>().And.Be(0.5d);
		}

		[Test]
		public void Convert_False_Valid()
		{
			var convert = converter.Convert(false, null, null, null);
			convert.Should().BeOfType<double>().And.Be(0.1d);
		}

		[Test]
		public void Convert_Null_InValid()
		{
			var convert = converter.Convert(null, null, null, null);
			convert.Should().BeOfType<double>().And.Be(0.1d);
		}

		[Test]
		public void Convert_String_InValid()
		{
			var convert = converter.Convert("Honey", null, null, null);
			convert.Should().BeOfType<double>().And.Be(0.1d);
		}

		[Test]
		public void Convert_Int_InValid()
		{
			var convert = converter.Convert(1, null, null, null);
			convert.Should().BeOfType<double>().And.Be(0.1d);
		}

		[Test]
		public void ConvertBack_True_Valid()
		{
			var convert = converter.ConvertBack(0.5d, null, null, null);
			convert.Should().BeOfType<bool>().And.Be(true);
		}

		[Test]
		public void ConvertBack_False_Valid()
		{
			var convert = converter.ConvertBack(0.1d, null, null, null);
			convert.Should().BeOfType<bool>().And.Be(false);
		}

		[Test]
		public void ConvertBack_False_Null_InValid()
		{
			var convert = converter.ConvertBack(null, null, null, null);
			convert.Should().BeOfType<bool>().And.Be(false);
		}

		[Test]
		public void ConvertBack_False_String_InValid()
		{
			var convert = converter.ConvertBack("Honey", null, null, null);
			convert.Should().BeOfType<bool>().And.Be(false);
		}

		[Test]
		public void ConvertBack_False_Int_InValid()
		{
			var convert = converter.ConvertBack(5, null, null, null);
			convert.Should().BeOfType<bool>().And.Be(false);
		}
	}
}