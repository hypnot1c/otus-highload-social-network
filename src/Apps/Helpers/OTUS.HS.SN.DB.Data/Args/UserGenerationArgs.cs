using PowerArgs;

namespace OTUS.HS.SN.DB.Data
{
  public class UserGenerationArgs
  {
    [ArgRequired, ArgShortcut("c"), ArgDescription("Count"), ArgDefaultValue(1), ArgPosition(1)]
    public int Count { get; set; }

    [ArgShortcut("o"), ArgDescription("Output file path"), ArgPosition(4)]
    public string? OutputFile { get; set; }
  }
}
