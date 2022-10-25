namespace iprep_api.Models.Response
{
    public class SearchResponse
    {
        public int MapperId { get; set; }
        public int SubjectId { get; set; }
        public string Subject { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}
