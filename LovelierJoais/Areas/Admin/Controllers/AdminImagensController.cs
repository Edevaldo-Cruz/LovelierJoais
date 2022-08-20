using LovelierJoais.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace LovelierJoais.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
                                        IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Metodo Upload de imagem
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            // Verifica se files está vazio
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            // Limita o quantidade de arquivo enviado
            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            // Calcula o total em bytes dos arquivos
            //using System.Linq
            long size = files.Sum(f => f.Length);

            // Armazena os nome do arquivo enviados
            var filePathsName = new List<string>();

            //Obtem o caminho onde vai ser salvo os arquivos
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);

            // Condiçoes para receber os arquivos
            foreach (var formFile in files)
            {
                //Verifica a extensão do arquivo
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif")
                    || formFile.FileName.Contains(".png"))
                {
                    // Monta o nome do arquivo
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    // Atribui o nome a variavel
                    filePathsName.Add(fileNameWithPath);

                    // Copia o arquivo para o servidor
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                }

            }

            ViewData["Resultado"] = $"{files.Count} arquivo foram enviados ao servidor, " +
                                    $"com tamanaho total de: {size} bytes";

            ViewBag.Arquivos = filePathsName;
            return View(ViewData);
        }

        public IActionResult GetImagens(string fname)
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath,
            _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProdutos = _myConfig.NomePastaImagensProdutos;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);

                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }

            return View("index");
        }

    }
}
