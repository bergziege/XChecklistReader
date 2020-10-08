using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.Services.Service {
    public interface IChecklistParser {
        IList<Checklist> ParseLines(IList<string> lines);
    }
}