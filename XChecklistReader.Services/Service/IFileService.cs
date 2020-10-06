using System.Collections.Generic;
using System.Threading.Tasks;

namespace XChecklistReader.Services.Service {
    public interface IFileService {
        IList<string> ReadFileAsLines(string filePath);
    }
}