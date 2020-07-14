using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eplayers.Models;
using Microsoft.AspNetCore.Http;

namespace Eplayers.Controllers
{
    public class NoticiasController : Controller
    {
        Noticias noticiasModel = new Noticias();

        public IActionResult Index(){
            ViewBag.Noticias = noticiasModel.ReadAll(); 
            return View();
        }

        /// <summary>
        /// Cadastra notícia no arquivo CSV
        /// </summary>
        /// <param name="form">Arquivo onde serão cadastradas as informações</param>
        /// <returns>Notícia cadastrada na Página Notícias</returns>
        public IActionResult CadastrarNoticia(IFormCollection forms){
            Noticias novaNoticia = new Noticias();
            novaNoticia.IdNoticia = Int32.Parse(forms["IdNoticia"]);
            novaNoticia.Titulo = forms["Titulo"];
            novaNoticia.Texto = forms["Texto"];
            novaNoticia.Imagem = forms["Imagem"];

            noticiasModel.Create(novaNoticia);

            ViewBag.Noticias = noticiasModel.ReadAll();

            return LocalRedirect("~/Noticias");
        }   

    }
}