namespace Receitas.Api.Exceptions;


public class IdentificadorInvalidoException<TipoEntidade> : IdentificadorInvalidoException
{
	private static string _mensagemErro = $"Identificador inválido para a entidade {typeof(TipoEntidade).Name}";
	
	public IdentificadorInvalidoException() : base(_mensagemErro)
	{
	}

    public IdentificadorInvalidoException(Exception inner) : base(_mensagemErro, inner)
    {
    }
}

public class IdentificadorInvalidoException : Exception
{

	public IdentificadorInvalidoException(string message) 
		: base (message)
	{
		
	}
	
	public IdentificadorInvalidoException(string message, Exception inner) 
		: base (message, inner)
	{
		
	}
}
