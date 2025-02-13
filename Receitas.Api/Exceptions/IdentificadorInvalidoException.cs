namespace Receitas.Api.Exceptions;

public class IdentificadorInvalidoException : Exception
{

	public IdentificadorInvalidoException()
	{	
		
	}
	
	public IdentificadorInvalidoException(string message) 
		: base (message)
	{
		
	}
	
	public IdentificadorInvalidoException(string message, Exception inner) 
		: base (message, inner)
	{
		
	}
}
