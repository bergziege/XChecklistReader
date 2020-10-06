using System.Collections.Generic;
using System.Threading.Tasks;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.Services.Service {
    public interface IChecklistParser {
        IList<Checklist> ParseFromFile(string filePath);
    }
}