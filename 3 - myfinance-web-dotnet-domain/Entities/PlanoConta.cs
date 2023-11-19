namespace myfinance_web_dotnet_domain.Entities;

public class PlanoConta
{
    public PlanoConta(string descricao, string tipo) 
    {
        this.Descricao = descricao;
        this.Tipo = tipo;
    }
    
    public int? Id {get; set;}
    public string Descricao {get; set;}
    public string Tipo {get; set;}
}
