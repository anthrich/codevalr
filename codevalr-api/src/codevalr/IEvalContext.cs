using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codevalr
{
	public interface IEvalContext
	{
		CodeEvalResult EvaluateCodeChallenge(CodeChallenge codeChallenge);
	}
}
