using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codevalr
{
	public class UnitTestResult
	{
		public UnitTestResult()
		{
			Messages = new List<string>();
		}

		public bool IsPassing { get; set; }
		public List<string> Messages { get; set; }
	}
}
