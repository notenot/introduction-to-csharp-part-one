using System.Collections.Generic;
using System.Diagnostics;

namespace Profiling
{
	public class ProfilerTask : IProfiler
	{
		public List<ExperimentResult> Measure(IRunner runner, int repetitionsCount)
		{
		    var result = new List<ExperimentResult>();

            foreach (var fieldCount in Constants.FieldCounts)
		    {
		        PerformExperiment(runner, true, fieldCount, 1);
		        var classTime = PerformExperiment(runner, true, fieldCount, repetitionsCount);

		        PerformExperiment(runner, false, fieldCount, 1);
		        var structTime = PerformExperiment(runner, false, fieldCount, repetitionsCount);

		        result.Add(new ExperimentResult(fieldCount, classTime, structTime));
		    }

            return result;
		}

	    private double PerformExperiment
	        (IRunner runner, bool isClass, int fieldCount, int repetitionsCount)
	    {
	        var watch = new Stopwatch();

	        watch.Start();
	        runner.Call(isClass, fieldCount, repetitionsCount);
	        watch.Stop();

	        return (double)watch.ElapsedMilliseconds / repetitionsCount;
        }
    }
}