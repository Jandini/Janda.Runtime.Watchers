namespace Janda.Runtime.Watchers
{
    internal class MethodCall
    {
        public MethodParameters Parameters { get; set; }
        public object ReturnValue { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as MethodCall;
            return Parameters == other.Parameters;
        }

        public override int GetHashCode()
        {
            return Parameters.GetHashCode();
        }
    }
}
