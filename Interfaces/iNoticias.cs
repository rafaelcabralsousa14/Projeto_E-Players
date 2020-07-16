using System.Collections.Generic;
using Eplayers.Models;

namespace Eplayers.Interfaces
{
    public interface iNoticias
    {
        void Create(Noticia a);
        List<Noticia> ReadAll();
        void Update(Noticia a);
        void Delete(int idNoticia);
        
    }
}