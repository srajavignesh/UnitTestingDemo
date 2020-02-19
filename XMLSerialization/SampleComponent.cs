using System;
using System.Collections.Generic;
using System.Linq;

namespace MSTest_Demo
{
	public class SampleComponent
	{
		public string CapitalizeThis(string data)
		{
			if( data == null)
			{
				throw new ArgumentNullException("data");
			}

			return data.ToUpper();
		}

		public int[] GetEvens(int startingWith, int count)
		{
			return (from i in Enumerable.Range(startingWith, count*2)
			        where i%2 == 0
			        select i).ToArray();
		}
		
		public IEnumerable<WaterSample> ComputeSamples()
		{
			return new[]
			       	{
			       		new WaterSample {Temperature = 105.2},
			       		new WaterSample {Temperature = 103.9},
			       		new WaterSample {Temperature = 100.5},
			       		new WaterSample {Temperature = 106.7},
			       		new WaterSample {Temperature = 106.2},
			       		new WaterSample {Temperature = 98.6},
			       	};
		}
	}
}
