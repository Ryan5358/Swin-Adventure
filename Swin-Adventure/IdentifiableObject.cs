using System;

namespace Swin_Adventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;
        public IdentifiableObject(string[] idents)
        {
            _identifiers = new List<string>();
            for (int i = 0; i < idents.Length; i++)
            {
                _identifiers.Add(idents[i]);
            }
        }
        public bool AreYou(string id)
        {
            bool isFound = false;
            for (int i = 0; i < _identifiers.Count; i++)
            {
                if (id.Equals(_identifiers[i], StringComparison.OrdinalIgnoreCase))
                {
                    isFound = true;
                }
            }
            return isFound;
        }   
        public string FirstId
        {
            get 
            { 
                if (_identifiers.Count == 0)
                {
                    return "";
                }
                else
                {
                    return _identifiers[0];
                }
            }
        }
        public void AddIdentifier(string id)
        {
            _identifiers.Add (id.ToLower());
        }
    }
}
