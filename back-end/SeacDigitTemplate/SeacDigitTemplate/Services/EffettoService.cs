using SeacDigitTemplate.Model;
using System.Collections.Generic;

namespace SeacDigitTemplate.Services
{
    public class EffettoService
    {
        public string GetEffetti(Documento documento, RigaDigitata[] rigaDigitataArray)
        {
            var effettoArray = new List<Effetto>();
            //doc assign

            //riga assign
            foreach(RigaDigitata rigaDigitata in rigaDigitataArray)
            {
                //look for a template

            }

            return "";
        }
    }
}
