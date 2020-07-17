using System.Collections.Generic;
using Eplayers.Models;

namespace Eplayers.Interfaces
{
    public interface iJogador
    {
        void Create(Jogador j);
        List<Jogador> ReadAll();
        void Update(Jogador j);
        void Delete(int idJogador);
    }
}