using System.IO;
using System.Xml.Serialization;

namespace MSTest_Demo
{
	public class ConfigData
	{
		public string CurrentOperation { get; set; }
		public int MaxRetries { get; set; }
		public string StorageDirectory { get; set; }
	}

	public interface IConfigurationRepository
	{
		ConfigData Load();
		void Save(ConfigData configuration);
	}

	public class ConfigDataFileRepository : IConfigurationRepository
	{
		private readonly string filename;

		public ConfigDataFileRepository(string filename)
		{
			this.filename = filename;
		}

		public ConfigData Load()
		{
			using(var fs = new FileStream(filename,FileMode.Open))
			{
				return LoadFromStream(fs);
			}
		}

		public void Save(ConfigData configuration)
		{
			using (var fs = new FileStream(filename, FileMode.Open))
			{
				SaveToStream(configuration, fs);
			}
		}

		public ConfigData LoadFromStream(Stream stream)
		{
			var serializer = new XmlSerializer(typeof(ConfigData));
			return (ConfigData)serializer.Deserialize(stream);
		}
		public void SaveToStream(ConfigData configuration, Stream stream)
		{
			var serializer = new XmlSerializer(typeof(ConfigData));
			serializer.Serialize(stream, configuration);
		}
	}
}
