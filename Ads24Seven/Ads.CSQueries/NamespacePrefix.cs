namespace Ads.CSQuery
{
    using System;

    /// <summary>
    /// Represent a type or attribute name.
    /// </summary>
 
    [Serializable]
 
    public struct NamespacePrefix
    {
         
        public static readonly NamespacePrefix None = new NamespacePrefix(null);

         
        public static readonly NamespacePrefix Empty = new NamespacePrefix(string.Empty);

         
        public static readonly NamespacePrefix Any = new NamespacePrefix("*");

         
        public NamespacePrefix(string text) : this()
        {
            Text = text;
        }

         
        public string Text { get; private set; }

         
        public bool IsNone { get { return Text == null; } }

         
        public bool IsAny
        {
            get { return !IsNone && Text.Length == 1 && Text[0] == '*'; }
        }

         
        public bool IsEmpty { get { return !IsNone && Text.Length == 0; } }

         
        public bool IsSpecific { get {return !IsNone && !IsAny; } }

         
        public override bool Equals(object obj)
        {
            return obj is NamespacePrefix && Equals((NamespacePrefix) obj);
        }

         
        public bool Equals(NamespacePrefix other)
        {
            return Text == other.Text;
        }

         
        public override int GetHashCode()
        {
            return IsNone ? 0 : Text.GetHashCode();
        }

         
        public override string ToString()
        {
            return IsNone ? "(none)" : Text;
        }

         
        public string Format(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name.Length == 0) throw new ArgumentException(null, "name");

            return Text + (IsNone ? null : "|") + name;
        }
    }
}
