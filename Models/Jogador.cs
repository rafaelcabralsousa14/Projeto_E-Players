using System;
using System.Collections.Generic;
using System.IO;
using Eplayers.Interfaces;

namespace Eplayers.Models
{
    public class Jogador : EplayersBase , iJogador
    {
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public int IdEquipe { get; set; }
        public string Nickname { get; set; }
        public string Senha { get; set; }
        public string ImagemPerfil { get; set; } 

        private const string PATH = "Database/jogador.csv";

        /// <summary>
        /// Método Construtor da classe Jogador, que cria o caminho/arquivo csv, se ele não existir
        /// </summary>
        public Jogador(){
            CreateFolderAndFile(PATH);
        }
       
        /// <summary>
        /// Cria novo Jogador
        /// </summary>
        /// <param name="a">Jogador que será criado</param>       
        public void Create(Jogador j)
        {
            string[] linha = {PrepararLinha(j)};
            File.AppendAllLines(PATH, linha);
        }
        
        /// <summary>
        /// Prepara a linha no formato CSV
        /// </summary>
        /// <param name="a">Informações do Jogador que serão formatadas</param>
        /// <returns>Linha preparada</returns>
        private string PrepararLinha(Jogador j){
            // formata o texto em CSV
            return $"{j.IdJogador};{j.IdEquipe};{j.Nome};{j.Nickname};{j.ImagemPerfil}";
        }

        /// <summary>
        /// Deleta um jogador
        /// </summary>
        /// <param name="idJogador">Id do Jogador que será deletado</param>
        public void Delete(int idJogador)
        {
            //Lê todas as linhas
            List<string> linhas = ReadAllLinesCSV(PATH);
            //Remove o jogador com o Id apresentado
            linhas.RemoveAll(x => x.Split(";")[0] == idJogador.ToString());
            //Reescreve o CSV, sem a notícia removida
            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// Lê todos os jogadores cadastrados
        /// </summary>
        /// <returns>Lista de Jogadores</returns>
        public List<Jogador> ReadAll()
        {
            //Cria uma lista de jogadores e lê suas linhas 
            List<Jogador> jogadores = new List<Jogador>();
            string[] linhas = File.ReadAllLines(PATH);
            //Laço que lê o cada linha do CSV e o formata, tirando o ";"
            foreach (var item in linhas)
            {
                //Tira os ";"
                string[] linha = item.Split(";");
                //Cria um jogador e pega cada atributo separadamente. Depois, adiciona esse jogador com os atributos separados
                Jogador jogador = new Jogador();
                jogador.IdJogador = Int32.Parse(linha[0]);
                jogador.IdEquipe = Int32.Parse(linha[1]);
                jogador.Nome = linha[2];
                jogador.Nickname = linha[3];
                jogador.ImagemPerfil = linha[4];
                                
                jogadores.Add(jogador);
            }
            return jogadores;
        }
        
        /// <summary>
        /// Altera um Jogador
        /// </summary>
        /// <param name="j">Jogador que será alterado</param>
        public void Update(Jogador j)
        {
            //Abre uma lista linhas do CSV e as lê
            List<string> linhas = ReadAllLinesCSV(PATH);
            //Remove a notícia que deve ser alterada 
            linhas.RemoveAll(x => x.Split(";")[0] == j.IdJogador.ToString());
            //Adiciona, no lugar do removido, o novo jogador
            linhas.Add(PrepararLinha(j));
            //Reescreve o CSV com a notícia nova
            RewriteCSV(PATH, linhas);  
        }

    }
}