using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Redacao.Application.ViewModel
{
    public class RedacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public TipoRedacaoViewModel TipoRedacao { get; set; } 

        public Guid TipoRedacaoId { get; set; }

        public TemaRedacaoViewModel TemaRedacao { get; set; }

        public Guid TemaRedacaoId { get; set; }

        public StatusRedacaoViewModel StatusRedacao { get; set; }

        public Guid StatusRedacaoId { get; set; }

        public Guid DocumentoId { get; set; }

        public Guid UsuarioAlunoId { get; set; }

		public Guid UsuarioProfessorId { get; set; }

        public bool Ativo { get; set; }

		//public IFormFile Documento { get; set; }
    }
}
