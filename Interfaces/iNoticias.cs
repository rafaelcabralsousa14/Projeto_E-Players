using System.Collections.Generic;
using Eplayers.Models;

namespace Eplayers.Interfaces
{
    public interface iNoticias
    {
        void Create(Noticias a);
        List<Noticias> ReadAll();
        void Update(Noticias a);
        void Delete(int idNoticias);
        
    }
}