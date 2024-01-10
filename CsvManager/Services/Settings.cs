using System.IO;
using CsvManager.Core.Services;
using Newtonsoft.Json;

namespace CsvManager.Services
{
    public class Settings : ISettings
    {
        public IDataSettings Data { get; private set; }
        private readonly JsonSerializerSettings _serializerOptions;
        private FileInfo _fileInfo;

        public void Load(string fileName)
        {
            _fileInfo = new FileInfo(fileName);
            Load();

        }

        public void Load()
        {
            if (!_fileInfo.Exists) return;
            var json = File.ReadAllText(_fileInfo.FullName);
            Data = JsonConvert.DeserializeObject<DataSettings>(json, _serializerOptions);

        }
        public void Save()
        {
            var json = JsonConvert.SerializeObject(Data, _serializerOptions);
            File.WriteAllText(_fileInfo.FullName, json);
        }

        public void SaveAs(string fileName)
        {
            _fileInfo = new FileInfo(fileName);
            Save();
        }

        public string GetText()
        {
            return JsonConvert.SerializeObject(Data, _serializerOptions);
        }

        public void SetText(string text)
        {
            Data = JsonConvert.DeserializeObject<DataSettings>(text);
        }

        public Settings()
        {
            _serializerOptions = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                //ContractResolver = new CamelCaseToSnakeCaseResolver(),
                //Converters = {new SnakeCaseToCamelCaseConverter()}
            };
            Data = new DataSettings();
        }
    }
}
