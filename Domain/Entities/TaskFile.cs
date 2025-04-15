using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class TaskFile : BaseModel
    {
        public Guid TaskId { get; set; }
        [ForeignKey("TaskId")]
        [JsonIgnore]
        public Task Task { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
    }
}
