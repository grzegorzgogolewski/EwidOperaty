namespace EwidOperaty.Tools
{
    public struct OracleStatus
    {
        public readonly bool Status;
        public readonly string Text;

        public OracleStatus(bool status, string text)
        {
            Status = status;
            Text = text;
        }
    }

}
