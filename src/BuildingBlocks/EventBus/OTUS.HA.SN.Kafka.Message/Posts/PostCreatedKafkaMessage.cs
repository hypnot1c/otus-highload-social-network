namespace OTUS.HA.SN.Kafka.Message
{
  public class PostCreatedKafkaMessage
  {
    public int PostId { get; set; }
    public int AuthorId { get; set; }
    public string Text { get; set; }
  }
}
