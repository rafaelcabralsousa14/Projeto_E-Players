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
        Noticias noticiaModel = new Noticias();

        public IActionResult Index(){
            ViewBag.Noticias = noticiaModel.ReadAll(); 
            return View();
        }

        /// <summary>
        /// Cadastra notícia no arquivo CSV
        /// </summary>
        /// <param name="form">Arquivo onde serão cadastradas as informações</param>
        /// <returns>Notícia cadastrada na Página Notícias</returns>
        public IActionResult Cadastrar(IFormCollection form){
            Noticias novaNoticia = new Noticias();
            novaNoticia.IdNoticia = Int32.Parse(form["IdNoticia"]);
            novaNoticia.Titulo = form["Titulo"];
            novaNoticia.Texto = form["Texto"];
            novaNoticia.Imagem = form["Imagem"];

            noticiaModel.Create(novaNoticia);

            ViewBag.Noticias = noticiaModel.ReadAll();

            return LocalRedirect("~/Notícias");
        }   

    }
}