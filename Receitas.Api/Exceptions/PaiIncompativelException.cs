namespace Receitas.Api.Exceptions;

public class PaiIncompativelException<TipoEntidadePai, TipoEntidadeFilho > : IdentificadorInvalidoException
{
	private static string _mensagemErro =
	 $" O identificador da entidade {typeof(TipoEntidadeFilho).Name} não pertence a entidade {typeof(TipoEntidadePai).Name} com o identificador informado";
	
	public PaiIncompativelException() : base(_mensagemErro)
	{
	}

    public PaiIncompativelException(Exception inner) : base(_mensagemErro, inner)
    {
    }
}
public class PaiIncompativelException : Exception
{
    public PaiIncompativelException(string message) 
		: base (message)
	{
		
	}
	
	public PaiIncompativelException(string message, Exception inner) 
		: base (message, inner)
	{
		
	}
}
