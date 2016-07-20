using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// This project can output the Class library as a NuGet Package.
// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
namespace codevalr
{
	public class Codevalr
	{
		private readonly IEvalContext _evalContext;

		public Codevalr(IEvalContext evalContext)
		{
			_evalContext = evalContext;
		}

		public CodeEvalResult EvalChallenge(CodeChallenge codeChallenge)
		{
			return _evalContext.EvaluateCodeChallenge(codeChallenge);
		}
	}
}
