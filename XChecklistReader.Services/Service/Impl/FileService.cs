using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace XChecklistReader.Services.Service.Impl {
    public class FileService : IFileService {
        public IList<string> ReadFileAsLines(string filePath) {
            return File.ReadAllLines(filePath);
        }
    }
}