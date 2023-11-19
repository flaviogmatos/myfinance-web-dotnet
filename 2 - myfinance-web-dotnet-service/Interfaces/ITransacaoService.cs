using myfinance_web_dotnet_domain.Entities;

namespace myfinance_web_dotnet_service.Interfaces
{
    public interface ITransacaoService
    {
        void Cadastrar (Transacao transacao);
        void Excluir (int Id);
        List<Transacao> ListarRegistros();
        Transacao RetornarRegistro (int Id);
    }
}