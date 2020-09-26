using Newtonsoft.Json;

namespace AnnoBibLibrary.Models
{
    /// <summary>
    /// The link for the many-to-many relationship between
    /// Library and Annotation.
    /// </summary>
    public class AnnotationLink
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("libraryId")]
        public int LibraryId { get; set; }

        [JsonProperty("annotationId")]
        public int AnnotationId { get; set; }

        /// <summary>
        /// The keyword values, whose fields represent Library keyword groups,
        /// and values represent Annotation keywords
        /// </summary>
        [JsonProperty("keywordValues")]
        public string KeywordValues { get; set; }
    }
}