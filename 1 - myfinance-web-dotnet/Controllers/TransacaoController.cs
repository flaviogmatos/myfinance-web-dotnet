using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly ITransacaoService _transacaoService;
        private readonly IPlanoContaService _planoContaService;

        public TransacaoController(ILogger<TransacaoController> logger, ITransacaoService transacaoService, 
               IPlanoContaService planoContaService)
        {
            _logger = logger;
            _transacaoService = transacaoService;
            _planoContaService = planoContaService;
        }

        [HttpGet]
        [Route ("Index")]
        public IActionResult Index()
        {
            var listaTransacoes = _transacaoService.ListarRegistros();
            List<TransacaoModel> listaTransacaoModel = new List<TransacaoModel>();

            foreach (var item in listaTransacoes)
            {
                var itemTransacao = new TransacaoModel()
                {
                    Id = item.Id,
                    Historico = item.Historico,
                    Data = item.Data,
                    Valor = item.Valor,
                    Tipo = item.PlanoConta.Tipo,
                    PlanoContaId = item.PlanoContaId
                };

                listaTransacaoModel.Add(itemTransacao);
            }

            ViewBag.ListaTransacao = listaTransacaoModel;

            return View();
        }

        [HttpGet]
        [Route("Cadastrar")]
        [Route("Cadastrar/{Id}")]
        public IActionResult Cadastrar(int? Id)
        {
            var ListaPlanoContas = new SelectList(_planoContaService.ListarRegistros(), "Id", "Descricao");

            var transacaoModel = new TransacaoModel()
            {
                Data = DateTime.Now,
                ListaPlanoContas = ListaPlanoContas
            };

            if (Id != null)
            {
                var transacao = _transacaoService.RetornarRegistro((int)Id);

                transacaoModel.Id = transacao.Id;
                transacaoModel.Historico = transacao.Historico;
                transacaoModel.Data = transacao.Data;
                transacaoModel.Valor = transacao.Valor;
                transacaoModel.PlanoContaId = transacao.PlanoContaId;
            }

            return View(transacaoModel);
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Route("Cadastrar/{Id}")]
        public IActionResult Cadastrar(TransacaoModel transacaoModel)
        {
            var transacao = new Transacao()
            {
                Id = transacaoModel.Id,
                Historico = transacaoModel.Historico,
                Data = transacaoModel.Data,
                Valor = transacaoModel.Valor,
                PlanoContaId = transacaoModel.PlanoContaId
            };

            _transacaoService.Cadastrar(transacao);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Excluir/{Id}")]
        public IActionResult Excluir(int? Id)
        { 
            _transacaoService.Excluir((int)Id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}