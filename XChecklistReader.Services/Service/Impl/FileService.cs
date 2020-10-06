using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace XChecklistReader.Services.Service.Impl {
    public class FileService : IFileService {
        public async Task<IList<string>> ReadFileAsLines(StorageFile filePath) {
            return await FileIO.ReadLinesAsync(filePath);
        }
    }
}