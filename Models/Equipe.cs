using System;
using System.Collections.Generic;
using System.IO;
using Eplayers.Interfaces;

namespace Eplayers.Models
{
    public class Equipe : EplayersBase , iEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/equipe.csv";
        
        /// <summary>
        /// Método Construtor de Equipe, que cria o caminho/arquivo csv, se ele não existir
        /// </summary>
        public Equipe(){
            CreateFolderAndFile(PATH);
        }

        /// <summary>
        /// Cria uma Equipe
        /// </summary>
        /// <param name="e">Equipe que será adicionada</param>
        public void Create(Equipe e)
        {
            string[] linha = {PrepararLinha(e)};
            File.AppendAllLines(PATH, linha);
        }

        /// <summary>
        /// Prepara a linha no formato CSV
        /// </summary>
        /// <param name="e">Equipe que será formatada</param>
        /// <returns>Linha preparada</returns>
        private string PrepararLinha(Equipe e){
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        /// <summary>
        /// Deleta uma Equipe
        /// </summary>
        /// <param name="idEquipe">Id da Equipe que será deletada</param>
        public void Delete(int idEquipe)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            // 1;FLA;fla.png
            linhas.RemoveAll(x => x.Split(";")[0] == idEquipe.ToString());
            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// Lê todos as linhas do csv
        /// </summary>
        /// <returns>Lista de Equipes</returns>        
        public List<Equipe> ReadAll(){
            List<Equipe> equipes = new List<Equipe>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Equipe equipe = new Equipe();
                equipe.IdEquipe = Int32.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                equipes.Add(equipe);
            }
            return equipes;
        }
        
        /// <summary>
        /// Altera uma Equipe
        /// </summary>
        /// <param name="e">Equipe que será alterada</param>
        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            linhas.Add(PrepararLinha(e));
            RewriteCSV(PATH, linhas);
        }
    }
}