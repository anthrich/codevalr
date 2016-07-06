using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codevalr
{
	public class CodeChallenge
	{
		public CodeChallenge()
		{
			_publicUnitTests = new List<UnitTest>();
			_privateUnitTests = new List<UnitTest>();
		}

		public string AnswerPreamble { get; private set; }
		public string Answer { get; private set; }
		public string AnswerPostamble { get; private set; }
		public IEnumerable<UnitTest> PublicUnitTests => _publicUnitTests;
		private List<UnitTest> _publicUnitTests;
		public IEnumerable<UnitTest> PrivateUnitTests => _privateUnitTests;
		private List<UnitTest> _privateUnitTests;


		public void SetAnswerPreamble(string preamble)
		{
			AnswerPreamble = preamble;
		}

		public void SetAnswerPostamble(string postamble)
		{
			AnswerPostamble = postamble;
		}

		public void SetAnswer(string answer)
		{
			Answer = answer;
		}

		public void AddPublicUnitTest(UnitTest test)
		{
			_publicUnitTests.Add(test);
		}

		public void AddPrivateUnitTest(UnitTest test)
		{
			_privateUnitTests.Add(test);
		}
	}
}
