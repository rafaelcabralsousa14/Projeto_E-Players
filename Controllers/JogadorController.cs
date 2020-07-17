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
    public class JogadorController : Controller
    {
        Jogador jogadorModel = new Jogador();

        public IActionResult Index(){
            ViewBag.Jogadores = jogadorModel.ReadAll(); 
            return View();
        }

        /// <summary>
        /// Cadastra Jogador no arquivo CSV
        /// </summary>
        /// <param name="forms">Arquivo onde serão cadastradas as informações</param>
        /// <returns>Jogador cadastrado na Página Cadastro</returns>
        public IActionResult Cadastrar(IFormCollection forms){
            Jogador novoJogador = new Jogador();
            novoJogador.IdJogador = Int32.Parse(forms["IdJogador"]);
            novoJogador.IdEquipe = Int32.Parse(forms["IdEquipe"]);
            novoJogador.Nome = forms["Nome"];
            novoJogador.Nickname = forms["Nickname"];
            novoJogador.Senha = forms["Senha"];
            
             // Upload Início
            var file = forms.Files[0];
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Jogadores");

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
                novoJogador.ImagemPerfil = file.FileName;
            }
            else
            {
                novoJogador.ImagemPerfil = "padrao.png";
            }
            // Upload Final
            
            jogadorModel.Create(novoJogador);

            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador");
        }   

        [Route("Jogador/{id}")]
        public IActionResult Excluir(int id){
            jogadorModel.Delete(id);
            ViewBag.Jogadores = jogadorModel.ReadAll();
            return LocalRedirect("~/Jogador");
        }

    }
}