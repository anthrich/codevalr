using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codevalr
{
	public class UnitTest
	{
		public UnitTest() { }

		public string Preamble { get; private set; }
		public string Test { get; private set; }
		public string Postamble { get; private set; }

		public void SetPreamble(string preamble)
		{
			Preamble = preamble;
		}

		public void SetPostamble(string postamble)
		{
			Postamble = postamble;
		}

		public void SetTest(string test)
		{
			Test = test;
		}
	}
}
