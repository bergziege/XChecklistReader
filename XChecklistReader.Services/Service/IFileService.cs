using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace XChecklistReader.Services.Service {
    public interface IFileService {
        Task<IList<string>> ReadFileAsLines(StorageFile filePath);
    }
}