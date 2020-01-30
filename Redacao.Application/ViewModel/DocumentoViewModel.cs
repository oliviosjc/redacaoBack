using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Redacao.Application.ViewModel
{
    public class DocumentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public string File { get; set; }

        public string Size { get; set; }

        public string Folder { get; set; }
    }
}
