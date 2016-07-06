using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codevalr
{
	public class CodeEvalResult
	{
		public List<UnitTestResult> PublicUnitTestResults { get; set; }
		public List<UnitTestResult> PrivateUnitTestResults { get; set; }
	}
}
