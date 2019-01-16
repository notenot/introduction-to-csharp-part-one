using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Profiling
{
	class ChartBuilder : IChartBuilder
	{
	    public Control Build(List<ExperimentResult> result)
	    {
	        var chart = new Chart();
	        chart.ChartAreas.Add(new ChartArea());
            var structGraph = new Series();
	        var classGraph = new Series();

            foreach (var experiment in result)
            {
                structGraph.Points.Add(new DataPoint(experiment.Size, experiment.StructResult));
                classGraph.Points.Add(new DataPoint(experiment.Size, experiment.ClassResult));
            }

            structGraph.ChartType = SeriesChartType.FastLine;
	        structGraph.Color = Color.Red;

	        classGraph.ChartType = SeriesChartType.FastLine;
	        classGraph.Color = Color.Green;

	        chart.Series.Add(structGraph);
	        chart.Series.Add(classGraph);
	        chart.Dock = DockStyle.Fill;
	        return chart;
        }
	}
}