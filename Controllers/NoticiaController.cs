using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eplayers.Models;
using Microsoft.AspNetCore.Http;

namespace Eplayers.Controllers
{
    public class NoticiaController : Controller
    {
        Noticia noticiaModel = new Noticia();

        public IActionResult Index(){
            ViewBag.Noticias = noticiaModel.ReadAll(); 
            return View();
        }

        /// <summary>
        /// Cadastra notícia no arquivo CSV
        /// </summary>
        /// <param name="forms">Arquivo onde serão cadastradas as informações</param>
        /// <returns>Notícia cadastrada na Página Notícias</returns>
        public IActionResult CadastrarNoticia(IFormCollection forms){
            Noticia novaNoticia = new Noticia();
            novaNoticia.IdNoticia = Int32.Parse(forms["IdNoticia"]);
            novaNoticia.Titulo = forms["Titulo"];
            novaNoticia.Texto = forms["Texto"];
            
             // Upload Início
            var fileN = forms.Files[0];
            var folderN = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(fileN != null)
            {
                if(!Directory.Exists(folderN)){
                    Directory.CreateDirectory(folderN);
                }

                var pathN = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folderN, fileN.FileName);
                using (var streamN = new FileStream(pathN, FileMode.Create))  
                {  
                    fileN.CopyTo(streamN);  
                }
                novaNoticia.ImagemN = fileN.FileName;
            }
            else
            {
                novaNoticia.ImagemN   = "padrao.png";
            }
            // Upload Final
            
            noticiaModel.Create(novaNoticia);

            ViewBag.Noticias = noticiaModel.ReadAll();

            return LocalRedirect("~/Noticia");
        }   

        [Route("Noticia/{id}")]
        public IActionResult Excluir(int id){
            noticiaModel.Delete(id);
            ViewBag.Noticias = noticiaModel.ReadAll();
            return LocalRedirect("~/Noticia");
        }

    }
}