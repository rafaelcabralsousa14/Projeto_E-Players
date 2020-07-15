using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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
            
            // Upload Início
            var file = forms.Files[0];
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                novaNoticia.ImagemN   = file.FileName;
            }
            else
            {
                novaNoticia.ImagemN   = "padrao.png";
            }
            // Upload Final

            noticiasModel.Create(novaNoticia);

            ViewBag.Noticias = noticiasModel.ReadAll();

            return LocalRedirect("~/Noticias");
        }   

    }
}