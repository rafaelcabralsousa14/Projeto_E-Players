using System;
using System.Collections.Generic;
using System.IO;
using Eplayers.Interfaces;

namespace Eplayers.Models
{
    public class Noticias : EplayersBase , iNoticias
    {
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string ImagemN { get; set; }

        private const string PATH = "Database/noticias.csv";

        /// <summary>
        /// Método Construtor das Notícias, que cria o caminho/aarquivo csv, se ele não existir
        /// </summary>
        public Noticias(){
            CreateFolderAndFile(PATH);
        }
       
        /// <summary>
        /// Cria nova Notícia
        /// </summary>
        /// <param name="a">Notícia que será criada</param>       
        public void Create(Noticias a)
        {
            string[] linha = {PrepararLinha(a)};
            File.AppendAllLines(PATH, linha);
        }
        
        /// <summary>
        /// Prepara a linha no formato CSV
        /// </summary>
        /// <param name="a">Notícia que será formatada</param>
        /// <returns>Linha preparada</returns>
        private string PrepararLinha(Noticias a){
            // formata o texto em CSV
            return $"{a.IdNoticia};{a.Titulo};{a.Texto};{a.ImagemN}";
        }

        /// <summary>
        /// Deleta uma notícia
        /// </summary>
        /// <param name="idNoticias">Id da Notícia que será deletada</param>
        public void DeleteN(int idNoticias)
        {
            //Lê todas as linhas
            List<string> linhas = ReadAllLinesCSV(PATH);
            //Remove a notícia com o Id apresentado
            linhas.RemoveAll(x => x.Split(";")[0] == idNoticias.ToString());
            //Reescreve o CSV, sem a notícia removida
            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// Lê todas as notícias
        /// </summary>
        /// <returns>Lista de Notícias</returns>
        public List<Noticias> ReadAll()
        {
            //Cria uma lista de noticias e lê suas linhas 
            List<Noticias> noticias = new List<Noticias>();
            string[] linhas = File.ReadAllLines(PATH);
            //Laço que lê o cada linha do CSV e o formata, tirando o ";"
            foreach (var item in linhas)
            {
                //Tira os ";"
                string[] linha = item.Split(";");
                //Cria uma notícia e pega cada atributo separadamente. Depois, adiciona essa notícia com os atributos separados
                Noticias noticia = new Noticias();
                noticia.IdNoticia = Int32.Parse(linha[0]);
                noticia.Titulo = linha[1];
                noticia.Texto = linha[2];
                noticia.ImagemN = linha[3];
                                
                noticias.Add(noticia);
            }
            return noticias;
        }
        
        /// <summary>
        /// Altera uma Notícia
        /// </summary>
        /// <param name="a">Notícia que será alterada</param>
        public void Update(Noticias a)
        {
            //Abre uma list da linhas do CSV e as lê
            List<string> linhas = ReadAllLinesCSV(PATH);
            //Remove a notícia que deve ser alterada 
            linhas.RemoveAll(x => x.Split(";")[0] == a.IdNoticia.ToString());
            //Adiciona, no lugar da removida, a nova notícia
            linhas.Add(PrepararLinha(a));
            //Reescreve o CSV com a notícia nova
            RewriteCSV(PATH, linhas);  
        }
    }
}