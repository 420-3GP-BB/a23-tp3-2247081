using System.Collections.ObjectModel;
using System.Xml;

namespace Model
{

    public class Membres
    {
        public string _Nom {  get; set; }
        public bool _Administrateur { get; set; }

        public override string ToString()
        {
            return _Nom;
        }
    }
}
