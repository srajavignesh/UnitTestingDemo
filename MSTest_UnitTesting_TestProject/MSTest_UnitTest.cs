using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest_Demo;

namespace MSTest_Demo_Tests
{
	[TestClass]
	public class MSTest_UnitTest
	{
		
		//Gets or sets the test context which provides
		//information about and functionality for the current test run.
		
		public TestContext TestContext { get; set; }


		[TestMethod]
		// To group Unit Test following attributes are used
		[TestCategory("ExceptionCheck")]
		[Priority(2)]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Capitalize_Throws_Exception_When_Argument_Is_Null()
		{
			var component = new SampleComponent();
			component.CapitalizeThis(null);
		}

		// The below test fails because of the incorrect file path
		// as we used filesystem to test the functionality
		// To avoid such dependency, filestream has been used in the 
		//below test method
		[TestMethod]
		[TestCategory("FileOperation")]
		[Priority(1)]
		public void ConfigData_Roundtrips_Successfully_UsingFileSystem()
		{
			var configData = new ConfigData
								{
			                 		CurrentOperation = "test",
			                 		MaxRetries = 55,
			                 		StorageDirectory = @"C:\temp"
								};
			var repository = new ConfigDataFileRepository("sample.xml");
			repository.Save(configData);

			var loaded = repository.Load();

			Assert.AreEqual(configData.CurrentOperation, loaded.CurrentOperation);
			Assert.AreEqual(configData.MaxRetries, loaded.MaxRetries);
			Assert.AreEqual(configData.StorageDirectory, loaded.StorageDirectory);
		}

		//
		// Test Method to use a MemoryStream and avoid the file system
		[TestMethod]
		[TestCategory("FileOperation")]
		[Priority(1)]
		public void ConfigData_Roundtrips_Successfully_UsingMemoryStream()
		{
			var configData = new ConfigData
			{
				CurrentOperation = "test",
				MaxRetries = 55,
				StorageDirectory = @"C:\temp"
			};
			var repository = new ConfigDataFileRepository("sample.xml");
			var ms = new MemoryStream();
			repository.SaveToStream(configData, ms);

			ms.Position = 0;
			var loaded = repository.LoadFromStream(ms);

			Assert.AreEqual(configData.CurrentOperation, loaded.CurrentOperation);
			Assert.AreEqual(configData.MaxRetries, loaded.MaxRetries);
			Assert.AreEqual(configData.StorageDirectory, loaded.StorageDirectory);
		}

		// Collection Verfication using Assert

		[TestMethod]
		[TestCategory("CollectionVerification")]
		[Priority(1)]
		public void Validate_GetEvens()
		{
			var sample = new SampleComponent();
			var evens = sample.GetEvens(12, 4);

			Assert.AreEqual(12, evens[0]);
			Assert.AreEqual(14, evens[1]);
			Assert.AreEqual(16, evens[2]);
			Assert.AreEqual(18, evens[3]);
		}


		// Collection Verfication using the class CollectionAssert
		
		[TestMethod]
		[TestCategory("CollectionVerification")]
		[Priority(1)]
		public void Validate_GetEvens_UsingCollectionAssert()
		{
			var sample = new SampleComponent();
			var evens = sample.GetEvens(12, 4);

			CollectionAssert.AreEqual(new[] {12, 14, 16, 18}, evens);
		}

		[TestMethod]
		[TestCategory("LinQ")]
		[Priority(2)]
		public void Find_Five_Temps_Over_100()
		{
			var sample = new SampleComponent();

			var results = sample.ComputeSamples();
			var query = from s in results
						where s.Temperature > 100.0
						select s;
			Assert.AreEqual(5, query.Count());
		}

		[TestMethod]
		[TestCategory("LinQ")]
		[Priority(2)]
		public void Find_Four_Highest_Temps()
		{
			var sample = new SampleComponent();

			var results = sample.ComputeSamples();
			var query = (from s in results
							orderby s.Temperature descending 
							select s.Temperature).Take(4);
			CollectionAssert.AreEqual(
				new [] { 106.7, 106.2, 105.2, 103.9 }, query.ToArray());
		}
	}
}
