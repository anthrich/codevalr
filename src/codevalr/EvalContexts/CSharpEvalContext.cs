using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.IO;
using System.Runtime.Loader;

namespace codevalr.EvalContexts
{
	public class CSharpEvalContext : IEvalContext
	{
		public CodeEvalResult EvaluateCodeChallenge(CodeChallenge codeChallenge)
		{
			var publicTestResults = codeChallenge.PublicUnitTests.Select(ut => AssessUnitTest(ut, codeChallenge));
			var privateTestResults = codeChallenge.PrivateUnitTests.Select(ut => AssessUnitTest(ut, codeChallenge));

			return new CodeEvalResult()
			{
				PrivateUnitTestResults = privateTestResults.ToList(),
				PublicUnitTestResults = publicTestResults.ToList()
			};
		}

		private UnitTestResult AssessUnitTest(UnitTest unitTest, CodeChallenge codeChallenge)
		{
			var comp = GetCSharpCompilation(unitTest, codeChallenge);

			var unitTestResult = GetUnitTestResult(comp);			

			return unitTestResult;
		}

		private UnitTestResult GetUnitTestResult(CSharpCompilation comp)
		{
			var unitTestResult = new UnitTestResult();

			string filename = Path.GetRandomFileName();
			using (var ms = new FileStream(filename, FileMode.CreateNew))
			{
				EmitResult result = comp.Emit(ms);
				ms.Flush();


				if (!result.Success)
				{
					unitTestResult.IsPassing = false;
					IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
						diagnostic.IsWarningAsError ||
						diagnostic.Severity == DiagnosticSeverity.Error);

					foreach (Diagnostic diagnostic in failures)
					{
						unitTestResult.Messages.Add($"{diagnostic.Id} {diagnostic.GetMessage()}");
					}
				}
				else
				{
					Assembly assembly = Assembly.Load(AssemblyLoadContext.GetAssemblyName(filename));

					Type type = assembly.GetType("RoslynCompileSample.Writer");
					object obj = Activator.CreateInstance(type);
					type.GetTypeInfo().GetDeclaredMethod("Write")
						.Invoke(obj, null);
				}
			}

			return unitTestResult;
		}

		private CSharpCompilation GetCSharpCompilation(UnitTest unitTest, CodeChallenge codeChallenge)
		{
			string codeFile = $"{codeChallenge.AnswerPreamble} {codeChallenge.Answer} {codeChallenge.AnswerPostamble}";
			string unitTestFile = $"{unitTest.Preamble} {unitTest.Test} {unitTest.Postamble}";
			SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeFile + unitTestFile);

			string assemblyName = Path.GetRandomFileName();
			MetadataReference[] references = new MetadataReference[]
			{
				MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
				MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location)
			};

			CSharpCompilation comp = CSharpCompilation.Create(
				assemblyName,
				new[] { syntaxTree },
				references,
				new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

			return comp;
		}
	}
}
