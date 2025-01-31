using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Api.Entities
{
	public class Ingrediente
	{
		public int Id { get; set; }
		public required string Nome { get; set;}
	}
}