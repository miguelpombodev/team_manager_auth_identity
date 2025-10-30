using System.Text.Json.Serialization;

namespace TeamManager.Domain.Members.Entities;

public class EmailTemplate
{
    public int Id { get; set; }

    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }
    
    [JsonPropertyName("body_html")]
    public string BodyHtml { get; set; }
    
    [JsonPropertyName("body_plain_text")]
    public string BodyPlainText { get; set; }

}