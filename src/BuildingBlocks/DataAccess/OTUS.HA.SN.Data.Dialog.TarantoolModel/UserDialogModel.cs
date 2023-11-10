using ProGaudi.MsgPack.Light;

namespace OTUS.HA.SN.Data.Dialog.TarantoolModel
{
  [MsgPackArray]
  public class UserDialogModel
  {
    private MsgPackToken _addditionalDataRaw;

    [MsgPackArrayElement(0)]
    public string Id { get; set; }
    [MsgPackArrayElement(1)]
    public int FromUserId { get; set; }
    [MsgPackArrayElement(2)]
    public int ToUserId { get; set; }
    [MsgPackArrayElement(3)]
    public string Text { get; set; }
    [MsgPackArrayElement(4)]
    public string CreatedAt { get; set; }

    [MsgPackArrayElement(5)]
    public MsgPackToken AddditionalDataRaw
    {
      get => _addditionalDataRaw;
      set
      {
        _addditionalDataRaw = value;
        var _ = TryParseString(value) || TryParseInt(value);
      }
    }

    public string AdditionalData { get; private set; }

    private bool TryParseString(MsgPackToken token)
    {
      try
      {
        AdditionalData = (string)token;
        return true;
      }
      catch
      {
        return false;
      }
    }

    private bool TryParseInt(MsgPackToken token)
    {
      try
      {
        AdditionalData = ((double)token).ToString();
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
